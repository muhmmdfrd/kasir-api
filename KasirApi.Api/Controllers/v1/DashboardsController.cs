using KasirApi.Api.Commons;
using KasirApi.Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KasirApi.Api.Controllers.v1;

[ApiVersion("1.0")]
[Route("v1/[controller]")]
[ApiController]
[Authorize]
public class DashboardsController : FlozaApiController
{
    private readonly DashboardHelper _helper;

    public DashboardsController(DashboardHelper helper)
    {
        _helper = helper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _helper.GetSummaryAsync();
        return ApiOK(result);
    }
}