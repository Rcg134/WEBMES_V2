using AutoMapper;
using WEBMES_V2.Models.DomainModels.Login;
using WEBMES_V2.Models.DTO;

namespace WEBMES_V2.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, User>().ReverseMap();
        }
    }
}
