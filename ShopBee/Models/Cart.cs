using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopBee.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        int? UserID { get; set; }
        [ForeignKey("UserID")]
        [ValidateNever]
        public User? User { get; set; }

        [Required]
        int? BookID { get; set; }
        [ForeignKey("BookID")]
        [ValidateNever]
        public Book? Book { get; set; }

        [Required]
        int? Quantity { get; set; }
    }
}
