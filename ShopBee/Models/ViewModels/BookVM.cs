using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShopBee.Models.ViewModels
{
    public class BookVM
    {
        public Store Store { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> MyStores { get; set; }
        public User User { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> MyUsers { get; set; }

        public Book Book { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> MyCategories { get; set; }
        
    }
}
