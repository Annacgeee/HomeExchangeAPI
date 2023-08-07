using System.ComponentModel.DataAnnotations;

namespace  HomeExchange_Web.Models.Dto
{
    public class HomeNumberUpdateDTO
    {
        [Required]
        public int HomeNo {get;set;}
        [Required]
        public int HomeID { get; set; }

        public string SpecialDetails {get;set;}
    }
}