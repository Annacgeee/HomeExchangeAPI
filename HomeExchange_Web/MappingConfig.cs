using AutoMapper;
using HomeExchange_Web.Models.Dto;

namespace HomeExchange_Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<HomeDTO, HomeCreateDTO>().ReverseMap();
            CreateMap<HomeDTO, HomeUpdateDTO>().ReverseMap();

            CreateMap<HomeNumberDTO, HomeNumberCreateDTO>().ReverseMap();
            CreateMap<HomeNumberDTO, HomeNumberUpdateDTO>().ReverseMap();
        }
    }
}