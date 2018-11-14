using AutoMapper;
using EbookShop.Models;
using EbookShop.Services.Dtos;
namespace EbookShop.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<RegistrationDTO,AppUser>()
                .ForMember(au=> au.UserName,map => map.MapFrom(vm=>vm.Email));

            CreateMap<AppUser, DashboardDTO>(); 
           
        }
    }
}
