using IdentityAuthenticationWebApp.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IdentityAuthenticationWebApp.Pages.Admin.UsersManager
{
    public class DeleteModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [Required]
        [Display(Name = "User")]
        [BindProperty]
        public string SelectedUser { get; set; }
        public SelectList Users { get; set; }
        public async Task OnGetAsync()
        {
            await GetOptions();
        }
        public async Task<IActionResult> OnPostAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Email is required.");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"User with email '{email}' not found.");
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return Content($"User with email '{email}' successfully deleted.");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            await GetOptions();
            return Page();
        }
        public async Task GetOptions()
        {
            var users = await _userManager.Users.ToListAsync();
            Users = new SelectList(users, nameof(ApplicationUser.UserName));
        }
    }
}
