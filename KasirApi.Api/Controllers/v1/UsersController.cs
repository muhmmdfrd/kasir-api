using KasirApi.Api.Commons;
using KasirApi.Core.Helpers;
using KasirApi.Core.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KasirApi.Api.Controllers.v1;

[ApiVersion("1.0")]
[Route("v{v:apiVersion}/[controller]")]
[ApiController]
[Authorize]
public class UsersController : FlozaApiController
{
    private readonly UserHelper _helper;

    public UsersController(UserHelper helper)
    {
        _helper = helper;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Get()
    {
        return ApiOK("Work!");
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] UserAddDto request)
    {
        var result = await _helper.CreateAsync(request, CurrentUser);
        return result == 0 ? ApiDataInvalid() : ApiOK();
    }
    
    [HttpPost("auth")]
    [AllowAnonymous]
    public async Task<IActionResult> Auth()
    {
        await Task.Run(() => Console.WriteLine("Hello World!"));
        return ApiOK("hello");
    }
}