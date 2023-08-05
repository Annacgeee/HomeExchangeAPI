using AutoMapper;
using HomeExchangeAPI.Models;
using HomeExchangeAPI.Models.Dto;

namespace HomeExchangeAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Home, HomeDTO>().ReverseMap();
            CreateMap<Home, HomeCreateDTO>().ReverseMap();
            CreateMap<Home, HomeUpdateDTO>().ReverseMap();

            CreateMap<HomeNumber, HomeNumberDTO>().ReverseMap();
            CreateMap<HomeNumber, HomeNumberCreateDTO>().ReverseMap();
            CreateMap<HomeNumber, HomeNumberUpdateDTO>().ReverseMap();
        }
    }
}