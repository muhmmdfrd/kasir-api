using Flozacode.Models.Paginations;
using KasirApi.Core.Models.Customs.Requests;
using KasirApi.Core.Models.Customs.Response;
using KasirApi.Core.Models.Filters;
using KasirApi.Core.Models.Services;
using KasirApi.Repository.Entities;

namespace KasirApi.Core.Interfaces;

public interface IMemberService : IFlozaPagination<MemberViewDto, MemberAddDto, MemberUpdDto, MemberFilter>
{
    public Task UpdatePointAsync(int id, int totalPayment, int creator);
    public Task<MemberValidateResponse> ValidateAsync(string memberNumber);
}