using HomeExchangeAPI.Models.Dto;

namespace HomeExchangeAPI.Data
{
    public static class HomeStore
    {
        public static List<HomeDTO> homeList = new List<HomeDTO> {
            new HomeDTO{Id=1, Name="city"},
            new HomeDTO {Id = 2, Name="countryside"}
        };
    }
}