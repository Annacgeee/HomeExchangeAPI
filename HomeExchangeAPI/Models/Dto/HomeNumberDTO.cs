using System.ComponentModel.DataAnnotations;

namespace HomeExchangeAPI.Models.Dto
{
    public class HomeNumberDTO
    {
        [Required]
        public int HomeNo {get;set;}
        [Required]
        public int HomeID { get; set; }

        public string SpecialDetails {get;set;}

        public HomeDTO Home  { get; set; }
    }
}