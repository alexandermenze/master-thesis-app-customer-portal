using CustomerPortal.Messages.Dtos;

namespace CustomerPortal.ReportingService.BackgroundServices;

public class UsersReportBackgroundService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var userCount = await LoadUserCountFromApi();
        await StoreUserStatistics(DateTime.Now, userCount);
    }

    private async Task<int> LoadUserCountFromApi()
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:5000/api/users");
        var users =  await httpClient.GetFromJsonAsync<IEnumerable<UserResponseDto>>("");
        return users?.Count() ?? 0;
    }

    private async Task StoreUserStatistics(DateTime timestamp, int userCount)
    {
        var record = $"{timestamp:O};{userCount}";
        await File.AppendAllTextAsync(@"c:\user-stats.csv", record);
    }
}
