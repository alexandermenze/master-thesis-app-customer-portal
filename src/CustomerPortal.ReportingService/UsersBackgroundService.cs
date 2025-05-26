using System.Text.Json;
using CustomerPortal.Messages.Dtos;

namespace CustomerPortal.ReportingService;

public class UsersBackgroundService(IHttpClientFactory httpClientFactory) : BackgroundService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("UserAuthService");

    [ThreatModelProcess("reporting-service")]
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        const string filename = "UsersCountInfo.json";

        while (true)
        {
            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);

            var users = await Pull(
                "get-users",
                () =>
                    _httpClient.GetFromJsonAsync<IEnumerable<UserResponseDto>>(
                        "users",
                        stoppingToken
                    )
            );

            await Push(
                "users-file-write",
                () =>
                {
                    var fileStream = File.Open(
                        filename,
                        FileMode.Append,
                        FileAccess.ReadWrite,
                        FileShare.ReadWrite
                    );

                    return JsonSerializer.SerializeAsync(
                        fileStream,
                        new UsersData(DateTime.Now, users?.Count() ?? 0),
                        JsonSerializerOptions.Default,
                        stoppingToken
                    );
                }
            );
        }
    }
}
