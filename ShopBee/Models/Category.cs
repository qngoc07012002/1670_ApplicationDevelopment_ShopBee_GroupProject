using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ShopBee.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name must be 100 Characters ")]
        [DisplayName("Category Name")]
        public string? Name { get; set; }
        public int? Status { get; set; }
        public int IsDeleted { get; set; }
    }
}
