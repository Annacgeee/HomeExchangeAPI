using static HomeExchange_Utility.SD;

namespace HomeExchange_Web.Models
{
    public class APIRequest
    {
        public ApiType ApiType {get;set;} = ApiType.GET;

        public string Url {get;set;}

        public object Data {get; set;}
    }
}