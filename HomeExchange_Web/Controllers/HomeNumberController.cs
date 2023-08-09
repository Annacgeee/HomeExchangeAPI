using AutoMapper;
using HomeExchange_Web.Models;
using HomeExchange_Web.Models.Dto;
using HomeExchange_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HomeExchange_Web.Controllers
{
    public class HomeNumberController : Controller
    {

        private readonly IHomeNumberService _HomeNumberService;
        private readonly IHomeService _HomeService;
        private readonly IMapper _mapper;
        public HomeNumberController(IHomeNumberService HomeNumberService, IMapper mapper, IHomeService HomeService)
        {
            _HomeNumberService = HomeNumberService;
            _mapper = mapper;
            _HomeService = HomeService;
        }


        public async Task<IActionResult> IndexHomeNumber()
        {
            List<HomeNumberDTO> list = new();

            var response = await _HomeNumberService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<HomeNumberDTO>>(Convert.ToString(response.Result));
}
            return View(list);
        }

    }
}