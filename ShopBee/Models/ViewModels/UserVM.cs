using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShopBee.Models.ViewModels
{
    public class UserVM
    { 
    public User User { get; set; }
    [ValidateNever]
    public IEnumerable<SelectListItem> MyUsers { get; set; }
}
}
