using System.ComponentModel.DataAnnotations;

namespace BabyStore.Models
{
    public class Address
    {
        [Required]
        [Display(Name = "Address")]
        public string AddressLine1 { get; set; }
        [Display(Name = "Address second line")]
        public string AddressLine2 { get; set; }
        [Required]
        public string Town { get; set; }
        [Required]
        public string County { get; set; }
        [Required]
        public string Postcode { get; set; }
    }
}