using Flozacode.Models.Paginations;
using KasirApi.Core.Models.Customs.Requests;
using KasirApi.Core.Models.Filters;
using KasirApi.Core.Models.Services;

namespace KasirApi.Core.Interfaces;

public interface IMemberService : IFlozaPagination<MemberViewDto, MemberAddDto, MemberUpdDto, MemberFilter>
{
    
}