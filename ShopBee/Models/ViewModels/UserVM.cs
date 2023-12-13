using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShopBee.Models.ViewModels
{
    public class UserVM
    { 
        public User? User { get; set; }
        public UserRole? UserRole { get; set; }  
        public Role? Role { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> MyUsers { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> MyRoles { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem > MyUsersRoles { get; set; }

}
}
