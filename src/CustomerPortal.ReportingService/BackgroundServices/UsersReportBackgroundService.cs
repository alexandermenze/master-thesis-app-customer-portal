using CustomerPortal.Messages.Dtos;

namespace CustomerPortal.ReportingService.BackgroundServices;

public class UsersReportBackgroundService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        HttpClient httpClient = new HttpClient();

        var request = await httpClient.GetFromJsonAsync<List<UserResponseDto>>(
            "https://localhost:5001/api/usersreport"
        );

        if (request is null)
        {
            throw new Exception("Failed to retrieve users report.");
        }

        var userAmount = request.Count;

        var timeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");

        await File.AppendAllTextAsync(
            Environment.CurrentDirectory + "/UsersReport.txt",
            $"rtimestamp: \"{timeStamp}\",\"userCount\":{userAmount}\n"
        );
    }
}
