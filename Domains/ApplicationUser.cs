using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IdentityAuthenticationWebApp.Domains
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name ="First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;
    }
}
