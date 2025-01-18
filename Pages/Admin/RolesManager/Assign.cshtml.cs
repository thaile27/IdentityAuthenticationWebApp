using IdentityAuthenticationWebApp.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace IdentityAuthenticationWebApp.Pages.Admin.RolesManager
{
    public class AssignModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AssignModel(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> applicationUser)
        {
            _roleManager = roleManager;
            _userManager = applicationUser;
        }
        public SelectList Roles { get; set; }
        public SelectList Users { get; set; }

        [Required]
        [Display(Name = "Role")]
        [BindProperty]
        public string SelectedRole { get; set; }
        [Required]
        [Display(Name = "User")]
        [BindProperty]
        public string SelectedUser { get; set; }

        public async Task OnGet()
        {
            await GetOptions();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(SelectedUser);
                if (user != null)
                {
                    await _userManager.AddToRoleAsync(user, SelectedRole);
                }
                return RedirectToPage("/Admin/RolesManager/Index");
            }
            await GetOptions();
            return Page();
        }

        public async Task GetOptions()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var users = await _userManager.Users.ToListAsync();
            Roles = new SelectList(roles, nameof(IdentityRole.Name));
            Users = new SelectList(users, nameof(ApplicationUser.UserName));
        }
    }
}
