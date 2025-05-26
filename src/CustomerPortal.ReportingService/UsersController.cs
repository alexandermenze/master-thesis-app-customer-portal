using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace CustomerPortal.ReportingService;

[Route("reports")]
[Controller]
public class UsersController : ControllerBase
{
    private const string Filename = "UsersCountInfo.json";

    [HttpGet("user-count")]
    [OutboundDataflow("reporting-service", "reports-user-count")]
    public async Task<IActionResult> GetUsers()
    {
        var usersText = await Pull(
            "users-file-read",
            () => System.IO.File.ReadAllTextAsync(Filename)
        );

        var usersDataList = JsonSerializer.Deserialize<IEnumerable<UsersData>>(usersText);

        return Ok(usersDataList);
    }
}
