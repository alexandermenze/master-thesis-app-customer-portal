using System.Text.Json;
using CustomerPortal.Messages.Dtos;

namespace CustomerPortal.ReportingService;

public class UsersBackgroundService(IHttpClientFactory httpClientFactory) : BackgroundService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("UserAuthService");

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        const string filename = "UsersCountInfo.json";

        while (true)
        {
            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);

            var users = await _httpClient.GetFromJsonAsync<IEnumerable<UserResponseDto>>(
                "users",
                stoppingToken
            );

            var fileStream = File.Open(
                filename,
                FileMode.Append,
                FileAccess.ReadWrite,
                FileShare.ReadWrite
            );

            await JsonSerializer.SerializeAsync(
                fileStream,
                new UsersData(DateTime.Now, users?.Count() ?? 0),
                JsonSerializerOptions.Default,
                stoppingToken
            );
        }
    }
}
