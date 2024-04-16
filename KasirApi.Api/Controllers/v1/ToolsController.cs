using Flozacode.Extensions.StringExtension;
using KasirApi.Api.Commons;
using KasirApi.Core.Configs;
using KasirApi.Core.Models.Customs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace KasirApi.Api.Controllers.v1;

[ApiVersion("1.0")]
[Route("v1/[controller]")]
[ApiController]
[AllowAnonymous]
public class ToolsController : FlozaApiController
{
    private readonly JwtConfigs _jwtConfigs;

    public ToolsController(IOptions<JwtConfigs> configs)
    {
        _jwtConfigs = configs.Value;
    }
    
    [HttpPost("encrypt")]
    public IActionResult Encrypt([FromBody] TextRequest request)
    {
        var result = request.Text.Encrypt(_jwtConfigs.PasswordSecret);
        return ApiOK<string>(result);
    }

    [HttpPost("decrypt")]
    public IActionResult Decrypt([FromBody] TextRequest request)
    {
        var result = request.Text.Decrypt(_jwtConfigs.PasswordSecret);
        return ApiOK<string>(result);
    }
}