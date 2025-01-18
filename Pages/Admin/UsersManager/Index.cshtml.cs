using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IdentityAuthenticationWebApp.Domains;

namespace IdentityAuthenticationWebApp.Pages.Admin.UsersManager
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IndexModel(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public List<UserWithRolesViewModel> UsersWithRoles { get; set; }

        public async Task OnGetAsync()
        {
            // Initialize the list to store users and their roles
            UsersWithRoles = new List<UserWithRolesViewModel>();

            // Get all users
            var users = _userManager.Users.ToList();

            foreach (var user in users)
            {
                // Get the roles for the user
                var roles = await _userManager.GetRolesAsync(user);

                UsersWithRoles.Add(new UserWithRolesViewModel
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Roles = roles.ToList()
                });
            }
        }

        public class UserWithRolesViewModel
        {
            public string UserId { get; set; } = string.Empty;
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public List<string> Roles { get; set; } = [];
        }
    }
}
