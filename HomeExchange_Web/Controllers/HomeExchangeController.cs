using AutoMapper;
using HomeExchange_Web.Models;
using HomeExchange_Web.Models.Dto;
using HomeExchange_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HomeExchange_Web.Controllers
{
    public class HomeExchangeController : Controller
    {

        private readonly IHomeService _homeService;
        private readonly IMapper _mapper;

        public HomeExchangeController(IHomeService homeService, IMapper mapper)
        {
            _homeService = homeService;
            _mapper = mapper;
        }
        public async Task<IActionResult> IndexHome()
        {
             List<HomeDTO> list = new();
            
            var response = await _homeService.GetAllAsync<APIResponse>();
           
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<HomeDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
    }
}