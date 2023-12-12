using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopBee.Models
{
    public class Order
    {
        [Key] 
        public int Id { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int Quantity { get; set; }

        [DisplayName("Price $")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public string Method { get; set; }
        public string Status { get; set; }
        public DateOnly CreateDate { get; set; }
    }
}
