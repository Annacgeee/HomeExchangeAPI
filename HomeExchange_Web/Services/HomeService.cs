using HomeExchange_Utility;
using HomeExchange_Web.Models;
using HomeExchange_Web.Models.Dto;
using HomeExchange_Web.Services.IServices;

namespace HomeExchange_Web.Services
{
    public class HomeService : BaseService, IHomeService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string HomeUrl;

        public HomeService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            HomeUrl = configuration.GetValue<string>("ServiceUrls:HomeAPI");
        }

        public Task<T> CreateAsync<T>(HomeCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = HomeUrl + "/api/HomeExchangeAPI",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = HomeUrl + "/api/HomeExchangeAPI/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            Console.WriteLine("here her 42");
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = HomeUrl + "/api/HomeExchangeAPI",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id,string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = HomeUrl + "/api/HomeExchangeAPI/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(HomeUpdateDTO dto,string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = HomeUrl + "/api/HomeExchangeAPI/" + dto.Id,
                Token = token
            }) ;
        }
    }
}
