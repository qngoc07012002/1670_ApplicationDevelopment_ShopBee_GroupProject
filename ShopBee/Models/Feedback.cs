using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ShopBee.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public string BookId { get; set; }
        [ForeignKey("BookId")]
        [ValidateNever]
        public Book Book { get; set; }
        public string? Content { get; set; }
        public int? Rating { get; set; }
        public string? Response {  get; set; }
        public DateTime? CreateDate {  get; set; }
        

    }
}
