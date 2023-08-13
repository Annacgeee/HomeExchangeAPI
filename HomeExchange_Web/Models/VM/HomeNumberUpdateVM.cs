using HomeExchange_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeExchange_Web.Models.VM
{
    public class HomeNumberUpdateVM
    {
        public HomeNumberUpdateVM()
        {
            HomeNumber = new HomeNumberUpdateDTO();
        }
        public HomeNumberUpdateDTO HomeNumber { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> HomeList { get; set; }
    }
}