using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ShopBee.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Book Name")]
        public string Name { get; set; }
        [ValidateNever]

        
        public int StoreID { get; set; }
        [ForeignKey("StoreID")]
        [ValidateNever]
        public Store Store { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        [Required]
        [DisplayName("Price $")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ActualPrice { get; set; }

        [Required]
        [DisplayName("Discount Price $")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountPrice { get; set; }

        public int? Stock {  get; set; }

        [Required]
        [DisplayName("Author")]
        public string? Author { get; set; }

        [DisplayName("Description")]
        public string? Description { get; set; }

        [ValidateNever]
        public string? ImgUrl { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

       
        
    }
}
