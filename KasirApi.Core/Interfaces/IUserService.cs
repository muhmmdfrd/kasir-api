using Flozacode.Models.Paginations;
using KasirApi.Core.Models.Customs.Requests;
using KasirApi.Core.Models.Filters;
using KasirApi.Core.Models.Services;

namespace KasirApi.Core.Interfaces;

public interface IUserService : IFlozaPagination<UserViewDto, UserAddDto, UserUpdDto, UserFilter>
{
    public Task<UserAuthResponse> Auth(AuthRequest request);
}