using System.Net;
using Microsoft.Extensions.Localization;

namespace  HomeExchange_Web.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode {get; set;}

        public bool IsSuccess {get;set;} = true;

        public List<string> ErrorMessages {get;set;}

        public object Result { get;set;}

    }
}