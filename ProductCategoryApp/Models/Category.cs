using System.ComponentModel.DataAnnotations;

namespace ProductCategoryApp.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Category Name is required")]
        public string CategoryName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
