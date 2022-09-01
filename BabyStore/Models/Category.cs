using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BabyStore.Models
{
    public class Category
    {
        //setting the ID from the categories 
        public int ID { get; set; }
        //setting an error message if the category name is left empty
        [Required(ErrorMessage = "The category name must be filled")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Please enter a category name between 3 and 50 characters in length")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "Please enter a category name beginning with a capital letter and made up of letters and spaces only")]
        [Display(Name = "Category")]
        //setting the Name
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
