using KasirApi.Api.Commons;
using KasirApi.Core.Helpers;
using KasirApi.Core.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KasirApi.Api.Controllers.v1;

[ApiVersion("1.0")]
[Route("v1/[controller]")]
[ApiController]
[Authorize]
public class TransactionsController : FlozaApiController
{
    private readonly TransactionHelper _helper;

    public TransactionsController(TransactionHelper helper)
    {
        _helper = helper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _helper.GetListAsync();
        return ApiOK(result);
    }
    
    [HttpGet("{id:long}")]
    public async Task<IActionResult> Find([FromRoute] int id)
    {
        var result = await _helper.FindAsync(id);
        return ApiOK(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TransactionAddDto request)
    {
        var result = await _helper.CreateAsync(request, CurrentUser);
        return result == 0 ? ApiDataInvalid() : ApiOK();
    }
}