using System.ComponentModel.DataAnnotations;

namespace ShopBee.Models
{
    public class User
    {
        public enum GenderType
        {
            Male,
            Female
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public GenderType Gender { get; set; }


    }
}
