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
        public int? UserID { get; set; }
        [ForeignKey("UserID")]
        [ValidateNever]
        public User? User { get; set; }

        [Required]
        public int? BookID { get; set; }
        [ForeignKey("BookID")]
        [ValidateNever]
        public Book? Book { get; set; }
        [Required]
        public int? StoreID { get; set; }
        [ForeignKey("StoreID")]
        [ValidateNever]
        public Store? Store { get; set; }

        [Required]
        public int Quantity { get; set; }
        public string? Status { get; set; }

    }
}
