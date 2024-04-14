using Flozacode.Extensions.StringExtension;
using KasirApi.Core.Configs;
using KasirApi.Core.Interfaces;
using KasirApi.Core.Models.Common;
using KasirApi.Core.Models.Customs.Requests;
using KasirApi.Core.Models.Services;
using Microsoft.Extensions.Options;

namespace KasirApi.Core.Helpers;

public class UserHelper
{
    private readonly IUserService _service;
    private readonly JwtConfigs _jwtConfigs;

    public UserHelper(IUserService service, IOptions<JwtConfigs> jwtConfigs)
    {
        _service = service;
        _jwtConfigs = jwtConfigs.Value;
    }
    
    public async Task<List<UserViewDto>> GetAsync()
    {
        return await _service.GetListAsync();
    }

    public async Task<int> CreateAsync(UserAddDto value, CurrentUser currentUser)
    {
        value.Password = value.Password.Encrypt(_jwtConfigs.PasswordSecret);
        value.CreatedBy = currentUser.Id;
        value.CreatedAt = DateTime.UtcNow;
        value.UpdatedBy = currentUser.Id;
        value.UpdatedAt = DateTime.UtcNow;

        return await _service.CreateAsync(value);
    }

    public async Task<UserAuthResponse> AuthAsync(AuthRequest request)
    {
        request.Password = request.Password.Encrypt(_jwtConfigs.PasswordSecret);
        return await _service.Auth(request);
    }
}