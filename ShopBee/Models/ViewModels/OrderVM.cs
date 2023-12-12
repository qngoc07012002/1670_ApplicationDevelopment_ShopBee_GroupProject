using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShopBee.Models.ViewModels
{
    public class OrderVM
    {
        public Order Order { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> MyBooks { get; set; }
        
        
    }
}
