using AutoMapper;
using KasirApi.Core.Models.Services;
using KasirApi.Repository.Entities;

namespace KasirApi.Api.Mappers
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<User, UserViewDto>()
                .ForMember(d => d.RoleName, conf => conf.MapFrom(e => e.Role.Name))
                .ReverseMap();
            CreateMap<UserAddDto, User>();
            CreateMap<UserUpdDto, User>();
            
            CreateMap<Member, MemberViewDto>()
                .ReverseMap();
            CreateMap<MemberAddDto, Member>();
            CreateMap<MemberUpdDto, Member>()
                .ForMember(d => d.MemberNumber, conf => conf.Ignore());
            
            CreateMap<Product, ProductViewDto>()
                .ReverseMap();
            CreateMap<ProductAddDto, Product>();
            CreateMap<ProductUpdDto, Product>()
                .ForMember(d => d.Code, conf => conf.Ignore());

            CreateMap<Transaction, TransactionViewDto>()
                .ForMember(
                    d => d.MemberName, 
                    conf => conf.MapFrom(e => e.MemberId == null ? "" : e.Member!.Name))
                .ReverseMap();
            CreateMap<Transaction, TransactionViewDetailDto>()
                .ForMember(
                    d => d.MemberName, 
                    conf => conf.MapFrom(e => e.MemberId == null ? "" : e.Member!.Name))
                .ForMember(d => d.Details, conf => conf.MapFrom(e => e.TransactionDetails))
                .ReverseMap();
            CreateMap<TransactionAddDto, Transaction>()
                .ForMember(d => d.TransactionDetails, conf => conf.MapFrom(e => e.Details));

            CreateMap<TransactionDetail, TransactionDetailViewDto>()
                .ForMember(d => d.ProductName, conf => conf.MapFrom(e => e.Product.Name))
                .ReverseMap();
            CreateMap<TransactionDetailAddDto, TransactionDetail>();
            CreateMap<TransactionDetailUpdDto, TransactionDetail>();
        }
    }
}
