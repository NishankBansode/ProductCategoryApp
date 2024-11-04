using System.ComponentModel.DataAnnotations;

namespace ProductCategoryApp.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Product Name is required")]
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
