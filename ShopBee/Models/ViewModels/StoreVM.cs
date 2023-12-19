using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShopBee.Models.ViewModels
{
    public class StoreVM
    {
        public Store Store { get; set; }

        public int NumberOfStores { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> MyUsers { get; set; }

    }
}