using AutoMapper;
using HomeExchange_Utility;
using HomeExchange_Web.Models;
using HomeExchange_Web.Models.Dto;
using HomeExchange_Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> IndexHomeExchange()
        {
             List<HomeDTO> list = new();
            
            var response = await _homeService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
           
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<HomeDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        [Authorize(Roles="admin")]
          public async Task<IActionResult> CreateHomeExchange()
        {
            
            return View();
        }
        [Authorize(Roles="admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
          public async Task<IActionResult> CreateHomeExchange(HomeCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _homeService.CreateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
           
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "home created successfully!";
                return RedirectToAction(nameof(IndexHomeExchange));
            }
            }
            TempData["error"] = "error encounter!";
            return View(model);
        }

        [Authorize(Roles="admin")]
          public async Task<IActionResult> UpdateHomeExchange(int homeId)
        {
            var response = await _homeService.GetAsync<APIResponse>(homeId,HttpContext.Session.GetString(SD.SessionToken));
           
            if (response != null && response.IsSuccess)
            {
                HomeDTO model = JsonConvert.DeserializeObject<HomeDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<HomeUpdateDTO>(model)); 
            }
            return NotFound();
        }

        [Authorize(Roles="admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
          public async Task<IActionResult> UpdateHomeExchange(HomeUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _homeService.UpdateAsync<APIResponse>(model,HttpContext.Session.GetString(SD.SessionToken));
           
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "home updated successfully!";
                return RedirectToAction(nameof(IndexHomeExchange));
            }
            }
            TempData["error"] = "error encounter!";
            return View(model);
        }

        [Authorize(Roles="admin")]
          public async Task<IActionResult> DeleteHomeExchange(int homeId)
        {
            var response = await _homeService.GetAsync<APIResponse>(homeId);
           
            if (response != null && response.IsSuccess)
            {
                HomeDTO model = JsonConvert.DeserializeObject<HomeDTO>(Convert.ToString(response.Result));
                return View(model); 
            }
            return NotFound();
        }

        [Authorize(Roles="admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
          public async Task<IActionResult> DeleteHomeExchange(HomeDTO model)
        {
            
            var response = await _homeService.DeleteAsync<APIResponse>(model.Id);
           
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "home deleted successfully!";
                return RedirectToAction(nameof(IndexHomeExchange));
            }
        TempData["error"] = "error encounter!";
            return View(model);
        }
    }
}