using KasirApi.Core.Interfaces;
using KasirApi.Core.Models.Common;
using KasirApi.Core.Models.Services;

namespace KasirApi.Core.Helpers;

public class ProductHelper
{
    private readonly IProductService _service;

    public async Task<List<ProductViewDto>> GetAsync()
    {
        return await _service.GetListAsync();
    }

    public async Task<ProductViewDto> FindAsync(long id)
    {
        return await _service.FindAsync(id);
    }

    public async Task<int> CreateAsync(ProductAddDto value, CurrentUser currentUser)
    {
        value.CreatedBy = currentUser.Id;
        value.CreatedAt = DateTime.UtcNow;
        value.UpdatedBy = currentUser.Id;
        value.UpdatedAt = DateTime.UtcNow;

        return await _service.CreateAsync(value);
    }

    public async Task<int> UpdateAsync(ProductUpdDto value, CurrentUser currentUser)
    {
        value.UpdatedBy = currentUser.Id;
        value.UpdatedAt = DateTime.UtcNow;

        return await _service.UpdateAsync(value);
    }

    public async Task<int> DeleteAsync(long id)
    {
        return await _service.DeleteAsync(id);
    }
}