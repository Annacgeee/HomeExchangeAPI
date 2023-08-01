using AutoMapper;
using HomeExchangeAPI.Models;
using HomeExchangeAPI.Models.Dto;

namespace HomeExchangeAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Home, HomeDTO>();
            CreateMap<HomeDTO, Home>();

            CreateMap<Home, HomeCreateDTO>().ReverseMap();
            CreateMap<Home, HomeUpdateDTO>().ReverseMap();
        }
    }
}