using System.ComponentModel.DataAnnotations;

namespace HomeExchangeAPI.Models.Dto
{
    public class HomeNumberCreateDTO
    {
      
    [Required]
        public int HomeNo {get;set;}

        [Required]
        public int HomeID{get;set;}

        public string SpecialDetails {get;set;}
    }
}