using IdentityAuthenticationWebApp.Data;
using IdentityAuthenticationWebApp.Domains;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityAuthenticationWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            }).AddRoles<IdentityRole>()                                 // Add this line to enable role authorization
              .AddEntityFrameworkStores<ApplicationDbContext>();

            // Configuring the authorization settings for our app
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/identity/account/AccessDenied";
            });

            // Authorizing folders with the AuthorizeFolder method when using AddRazorPages() 
            //builder.Services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            //    options.AddPolicy("AdminOrStaff", policy => policy.RequireRole("Admin", "Staff"));
            //});

            //builder.Services.AddRazorPages(options =>
            //{
            //    options.Conventions.AuthorizeFolder("/Admin", "AdminOnly");
            //    options.Conventions.AuthorizeFolder("/Staff", "AdminOrStaff");
            //});

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapRazorPages();

            app.Run();
        }
    }
}
