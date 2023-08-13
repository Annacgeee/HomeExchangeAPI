using HomeExchange_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeExchange_Web.Models.VM
{
    public class HomeNumberCreateVM
    {
        public HomeNumberCreateVM()
        {
            HomeNumber = new HomeNumberCreateDTO();
        }
        public HomeNumberCreateDTO HomeNumber { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> HomeList { get; set; }
    }
}