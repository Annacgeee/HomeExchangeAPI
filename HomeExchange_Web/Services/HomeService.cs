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

        public Task<T> CreateAsync<T>(HomeCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = HomeUrl + "/api/v1/HomeExchangeAPI",
                // Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = HomeUrl + "/api/v1/HomeExchangeAPI/" + id,
                // Token = token
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = HomeUrl + "/api/v1/HomeExchangeAPI",
                // Token = token
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = HomeUrl + "/api/v1/HomeExchangeAPI/" + id,
                // Token = token
            });
        }

        public Task<T> UpdateAsync<T>(HomeUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = HomeUrl + "/api/v1/HomeExchangeAPI/" + dto.Id,
                // Token = token
            }) ;
        }
    }
}
