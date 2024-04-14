using AutoMapper;
using WEBMES_V2.Models.DomainModels.Login;
using WEBMES_V2.Models.DomainModels.PlasmaMagazine;
using WEBMES_V2.Models.DTO.LoginUserDTO;
using WEBMES_V2.Models.DTO.PlasmaMagazineDTO;

namespace WEBMES_V2.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<TrnLotMagazine, TrnLotMagazineDTO>().ReverseMap();
            CreateMap<TrnMagazineDetail, TrnMagazineDetailDTO>().ReverseMap();
            CreateMap<TrnMagazineDetail, Insert_TrnMagazineDTO>().ReverseMap();
            CreateMap<TrnMagazineDetail, TrnMagazineDetailDTO>().ReverseMap();
            CreateMap<TrnMagazineDetailsHistory, TrnMagazineDetailDTO>().ReverseMap();
            CreateMap<MsStationMagazine, MsStationMagazineDTO>().ReverseMap();
        }
    }
}
