using KasirApi.Core.Interfaces;
using KasirApi.Core.Models.Common;
using KasirApi.Core.Models.Services;

namespace KasirApi.Core.Helpers;

public class MemberHelper
{
    private readonly IMemberService _service;

    public MemberHelper(IMemberService service)
    {
        _service = service;
    }

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
        var now = DateTime.UtcNow;

        value.MemberNumber = Guid.NewGuid().ToString();
        value.JoinDate = now;
        value.CreatedBy = currentUser.Id;
        value.CreatedAt = now;
        value.UpdatedBy = currentUser.Id;
        value.UpdatedAt = now;

        return await _service.CreateAsync(value);
    }

    public async Task<int> UpdateAsync(MemberUpdDto value, CurrentUser currentUser)
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