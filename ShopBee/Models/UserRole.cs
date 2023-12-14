using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopBee.Models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }


        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public User User { get; set; }

        public int? RoleId { get; set; }
        [ForeignKey("RoleId")]
        [ValidateNever] 
        public Role Role { get; set; }
        

    }
}
