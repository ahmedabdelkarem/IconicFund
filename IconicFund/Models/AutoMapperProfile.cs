using AutoMapper;
//using IconicFund.Helpers.Shared.Dtos;
using IconicFund.Models;
using IconicFund.Models.Entities;

namespace IconicFund.Web.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Admin, AdminSessionUser>()
                .ForMember(user => user.Id, options => options.MapFrom(admin => admin.Id))
                .ForMember(user => user.NationalId, options => options.MapFrom(admin => admin.NationalId))
                .ForMember(user => user.FullName, options => options.MapFrom(admin => string.Concat(admin.FirstName, " ", admin.SecondName, " ", admin.ThirdName, " ", admin.LastName)));

           
            CreateMap<BasicSystemSetting, BasicSystemSettingViewModel>().ReverseMap();
            CreateMap<BasicSystemSettingViewModel, BasicSystemSetting>().ReverseMap();

        }
    }
}
