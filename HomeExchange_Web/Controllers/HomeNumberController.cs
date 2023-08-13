using AutoMapper;
using HomeExchange_Web.Models;
using HomeExchange_Web.Models.Dto;
using HomeExchange_Web.Models.VM;
using HomeExchange_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IActionResult> CreateHomeNumber()
        {
            HomeNumberCreateVM homeNumberVM = new HomeNumberCreateVM();
            var response = await _HomeService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                homeNumberVM.HomeList = JsonConvert.DeserializeObject<List<HomeDTO>>(Convert.ToString(response.Result)).Select(i => new SelectListItem{
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
}
            return View(homeNumberVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
          public async Task<IActionResult> CreateHomeNumber(HomeNumberCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _HomeNumberService.CreateAsync<APIResponse>(model.HomeNumber);
           
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexHomeNumber));
            }
            else{
                if (response.ErrorMessages.Count>0)
                {
                    ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                }
            }
            }

                var resp = await _HomeService.GetAllAsync<APIResponse>();
            if (resp != null && resp.IsSuccess)
            {
                model.HomeList = JsonConvert.DeserializeObject<List<HomeDTO>>(Convert.ToString(resp.Result)).Select(i => new SelectListItem{
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
}


            return View(model);
        }

          public async Task<IActionResult> UpdateHomeNumber(int homeNo)
        {
            HomeNumberUpdateVM homeNumberVM = new HomeNumberUpdateVM();
            var response = await _HomeNumberService.GetAsync<APIResponse>(homeNo);
           
            if (response != null && response.IsSuccess)
            {
                HomeNumberDTO model = JsonConvert.DeserializeObject<HomeNumberDTO>(Convert.ToString(response.Result));
                homeNumberVM.HomeNumber = _mapper.Map<HomeNumberUpdateDTO>(model); 
            }

             response = await _HomeService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                homeNumberVM.HomeList = JsonConvert.DeserializeObject<List<HomeDTO>>(Convert.ToString(response.Result)).Select(i => new SelectListItem{
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                return View(homeNumberVM);
}
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
          public async Task<IActionResult> UpdateHomeNumber(HomeNumberUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _HomeNumberService.UpdateAsync<APIResponse>(model.HomeNumber);
           
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexHomeNumber));
            }
            else{
                if (response.ErrorMessages.Count>0)
                {
                    ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                }
            }
            }

                var resp = await _HomeService.GetAllAsync<APIResponse>();
            if (resp != null && resp.IsSuccess)
            {
                model.HomeList = JsonConvert.DeserializeObject<List<HomeDTO>>(Convert.ToString(resp.Result)).Select(i => new SelectListItem{
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
}


            return View(model);
        }

          public async Task<IActionResult> DeleteHomeNumber(int homeNo)
        {

            HomeNumberDeleteVM homeNumberVM = new();
            var response = await _HomeNumberService.GetAsync<APIResponse>(homeNo);
           
            if (response != null && response.IsSuccess)
            {
                HomeNumberDTO model = JsonConvert.DeserializeObject<HomeNumberDTO>(Convert.ToString(response.Result));
                homeNumberVM.HomeNumber = model; 
            }

             response = await _HomeService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                homeNumberVM.HomeList = JsonConvert.DeserializeObject<List<HomeDTO>>(Convert.ToString(response.Result)).Select(i => new SelectListItem{
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                return View(homeNumberVM);
}
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
          public async Task<IActionResult> DeleteHomeNumber(HomeNumberDeleteVM model)
        {
            
            var response = await _HomeNumberService.DeleteAsync<APIResponse>(model.HomeNumber.HomeNo);
           
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexHomeNumber));
            }
        
            return View(model);
        }

    }
}