using HomeExchange_Utility;
using HomeExchange_Web.Models;
using HomeExchange_Web.Models.Dto;
using HomeExchange_Web.Services.IServices;

namespace HomeExchange_Web.Services
{
    public class HomeNumberService : BaseService, IHomeNumberService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string HomeUrl;

        public HomeNumberService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            HomeUrl = configuration.GetValue<string>("ServiceUrls:HomeAPI");
        }

        public Task<T> CreateAsync<T>(HomeNumberCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = HomeUrl + "/api/HomeNumberAPI",
                // Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = HomeUrl + "/api/HomeNumberAPI" + id,
                // Token = token
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            Console.WriteLine("here her 42");
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = HomeUrl + "/api/HomeNumberAPI",
                // Token = token
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = HomeUrl + "/api/HomeNumberAPI" + id,
                // Token = token
            });
        }

        public Task<T> UpdateAsync<T>(HomeNumberUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = HomeUrl + "/api/HomeNumberAPI" + dto.HomeNo,
                // Token = token
            }) ;
        }
    }
}
