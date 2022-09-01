using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BabyStore.Models
{
    public partial class Product
    {
        public int ID { get; set; }

        // setting error messages if the user inputs do not meet the required standard
        [Required(ErrorMessage = "The product cannot be left blank")]
        //error message if the entered text is not between 3 to 50 characters
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Product name must be over 3 characters")]
        [RegularExpression(@"^[a-zA-Z0-9'-'\s]*$", ErrorMessage = "Please enter a product name made up of letters and numbers")]
        public string Name { get; set; }
        // setting error messages if the user inputs do not meet the required standard
        [Required(ErrorMessage = "The description cannot be left blank")]
        //error message if the entered text is not between 10 to 200 characters
        [StringLength(200, MinimumLength = 10, ErrorMessage = "The Product description cannot be less than 10 characters")]
        [RegularExpression(@"^[,;a-zA-Z0-9'-'\s]*$", ErrorMessage = "Please enter a product description made up of letters and numbers")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        // setting error messages if the user inputs do not meet the required standard
        [Required(ErrorMessage = "The price cannot be blank")]
        //error message if the entered text is not between 0.10 to 10000.00 characters
        [Range(0.10, 10000, ErrorMessage = "Please enter a price between R0.10 and R10000.00")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        [RegularExpression("[0-9]+(\\.[0-9][0-9]?)?", ErrorMessage = "The price must be a number up to two decimal places")]
        public decimal Price { get; set; }

        public int? CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<ProductImageMapping> ProductImageMappings { get; set; }
    }
}
