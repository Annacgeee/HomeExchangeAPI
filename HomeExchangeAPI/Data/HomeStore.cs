using HomeExchangeAPI.Models.Dto;

namespace HomeExchangeAPI.Data
{
    public static class HomeStore
    {
        public static List<HomeDTO> homeList = new List<HomeDTO> {
            new HomeDTO {Id = 1, Name="city", Sqft=100, Occupancy=2},
            new HomeDTO {Id = 2, Name="countryside",Sqft=200, Occupancy=4}
        };
    }
}