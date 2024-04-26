using KasirApi.Core.Interfaces;

namespace KasirApi.Core.Helpers;

public class MemberHelper
{
    private readonly IMemberService _service;

    public async Task<List<MemberViewDto>> GetAsync()
    {
        return await _service.GetListAsync();
    }

    public async Task<MemberViewDto> FindAsync(long id)
    {
        return await _service.FindAsync(id);
    }

    public async Task<int> CreateAsync(MemberAddDto value, CurrentUser currentUser)
    {
        value.CreatedBy = currentUser.Id;
        value.CreatedAt = DateTime.UtcNow;
        value.UpdatedBy = currentUser.Id;
        value.UpdatedAt = DateTime.UtcNow;

        return await _service.CreateAsync(value);
    }

    public async Task<int> UpdateAsync(MemberUpdDto value, CurrentMember currentUser)
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