using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShopBee.Models.ViewModels
{
    public class BookVM
    {
        
       
        public Book Book { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> MyCategories { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> MyStores { get; set; }
        public int NumberOfBooks { get; set; }
    }
}
