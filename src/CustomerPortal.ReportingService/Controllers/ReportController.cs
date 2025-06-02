using Microsoft.AspNetCore.Mvc;

namespace CustomerPortal.ReportingService.Controllers;

[ApiController]
[Route("reports")]
public class ReportController : ControllerBase
{
    public Task<IActionResult> GetUsersReport()
    {
        throw new NotImplementedException();
    }
}
