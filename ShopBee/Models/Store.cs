using System.ComponentModel.DataAnnotations;

namespace ShopBee.Models
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreateDate { get; set; }

    }
}
