using Microsoft.AspNetCore.Mvc;

namespace CustomerPortal.ReportingService.Controllers;

[ApiController]
[Route("reports")]
public class ReportController : ControllerBase
{
    public async Task<IActionResult> GetUsersReport()
    {
        var userStatisticsCsv = await GetUserStatisticsFromFile();
        var userStatisticsResponse = UserStatisticsAsResponse(userStatisticsCsv);
        return Ok(userStatisticsResponse);
    }

    private async Task<IEnumerable<string>> GetUserStatisticsFromFile()
    {
        var userStatisticLines = await System.IO.File.ReadAllLinesAsync(@"c:\user-stats.csv");
        return userStatisticLines;
    }
    
    private IEnumerable<UserStatisticsResponse> UserStatisticsAsResponse(IEnumerable<string> userStatisticsCsv)
    {
        return userStatisticsCsv.Select(u => new UserStatisticsResponse
        {
            DateTime = DateTime.Parse(u.Split(",")[0]),
            UserCount = int.Parse(u.Split(",")[1])
        });
    }
    
    private record UserStatisticsResponse
    {
        public DateTime DateTime { get; set; }
        public int UserCount { get; set; }
    }
}
