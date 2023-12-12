using System.ComponentModel.DataAnnotations;

namespace ShopBee.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? NomalizedName { get; set; }
    }
}
