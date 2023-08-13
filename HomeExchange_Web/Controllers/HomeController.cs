using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HomeExchange_Web.Models;
using HomeExchange_Web.Services.IServices;
using AutoMapper;
using HomeExchange_Web.Models.Dto;
using Newtonsoft.Json;

namespace HomeExchange_Web.Controllers;

public class HomeController : Controller
{
    private readonly IHomeService _homeService;
        private readonly IMapper _mapper;

        public HomeController(IHomeService homeService, IMapper mapper)
        {
            _homeService = homeService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
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

