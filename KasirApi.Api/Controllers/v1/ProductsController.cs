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

public class ProductsController : FlozaApiController
{
    private readonly ProductHelper _helper;

    public ProductsController(ProductHelper helper)
    {
        _helper = helper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _helper.GetAsync();
        return ApiOK(result);
    }
    
    [HttpGet("{id:long}")]
    public async Task<IActionResult> Find([FromRoute] long id)
    {
        var result = await _helper.FindAsync(id);
        return ApiOK(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductAddDto request)
    {
        var result = await _helper.CreateAsync(request, CurrentUser);
        return result == 0 ? ApiDataInvalid() : ApiOK();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ProductUpdDto request)
    {
        var result = await _helper.UpdateAsync(request, CurrentUser);
        return result == 0 ? ApiDataInvalid() : ApiOK();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete([FromRoute] long id)
    {
        var result = await _helper.DeleteAsync(id);
        return result == 0 ? ApiDataInvalid() : ApiOK();
    }
}