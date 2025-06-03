using Microsoft.AspNetCore.Mvc;

namespace CustomerPortal.ReportingService.Controllers;

[ApiController]
[Route("reports")]
public class ReportController : ControllerBase
{
    [HttpGet("userCount")]
    public Task<IActionResult> GetUsersReport()
    {
        var filePath = Environment.CurrentDirectory + "/UsersReport.txt";

        if (!Pull("UserCount_TimeStamp", () => System.IO.File.Exists(filePath)))
        {
            return Task.FromResult<IActionResult>(NotFound("Report file not found."));
        }

        var reportContent = Pull("UserCount_TimeStamp", () => System.IO.File.ReadAllText(filePath));
        return Task.FromResult<IActionResult>(Ok(reportContent));
    }
}
