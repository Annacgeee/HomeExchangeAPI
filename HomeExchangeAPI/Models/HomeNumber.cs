using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeExchangeAPI.Models
{

    public class HomeNumber
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HomeNo {get;set;}

        public string SpecialDetails {get;set;}

        [ForeignKey("Home")]
        public int HomeID { get; set; }

        public Home Home {get; set;} // navigation property
        public DateTime CreatedDate {get; set;}

        public DateTime UpdatedDate{get;set;}
    }

    
}