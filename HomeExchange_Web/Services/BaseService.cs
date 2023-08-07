using System.Text;
using HomeExchange_Utility;
using HomeExchange_Web.Models;
using HomeExchange_Web.Services.IServices;
using Newtonsoft.Json;

namespace HomeExchange_Web.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse responseModel { get; set ; }
        public IHttpClientFactory HttpClient{ get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            this.responseModel = new APIResponse();
            this.HttpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
           try
           {
            Console.WriteLine("reached 25!!");
             var client = HttpClient.CreateClient("HomeExchangeAPI");
             HttpRequestMessage message = new();
             message.Headers.Add("Accept", "application/json");
             message.RequestUri = new Uri(apiRequest.Url);
             if (apiRequest.Data != null)
            {
                Console.WriteLine("reached 32!!");
                message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                Encoding.UTF8, "application/json");
            }
            Console.WriteLine("reached 35!!");
            switch (apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;

                }

                HttpResponseMessage apiResponse = null;

                // if (!string.IsNullOrEmpty(apiRequest.Token))
                // {
                //     client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.Token);
                // }
Console.WriteLine("reached 60 ");
                apiResponse = await client.SendAsync(message);
                Console.WriteLine("reached 62 ");
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                Console.WriteLine("reached 64 ");
               
                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                Console.WriteLine("reached 67 ");
                return APIResponse;
           }
           catch(Exception e)
            {
                var dto = new APIResponse
                {
                    ErrorMessages = new List<string> { Convert.ToString(e.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }

           
        }
    }
}