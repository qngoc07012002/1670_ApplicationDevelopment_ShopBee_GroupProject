using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopBee.Models
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public User User { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
