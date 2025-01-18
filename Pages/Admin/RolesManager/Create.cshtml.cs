using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityAuthenticationWebApp.Pages.Admin.RolesManager
{
    public class CreateModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public CreateModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        [BindProperty]
        public string Name { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole { Name = Name };
                await _roleManager.CreateAsync(role);
                return RedirectToPage("/Admin/RolesManager/Index");
            }
            return Page();
        }

        public void OnGet()
        {
        }
    }
}
