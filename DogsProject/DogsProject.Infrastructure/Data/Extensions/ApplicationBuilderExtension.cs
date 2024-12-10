using DogsProject.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DogsProject.Infrastructure.Data.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;
            await RoleSeeder(services);
            await SeedAdministrator(services);

            var data = services.GetRequiredService<ApplicationDbContext>();
            await SeedBreeds(data);

            return app;
        }

        private static async Task SeedBreeds(ApplicationDbContext data)
        {
            if (await data.Breeds.AnyAsync())
            {
                return;
            }

            await data.Breeds.AddRangeAsync(new[]
            {
                new Breed {Name = "Husky"},
                new Breed {Name = "Pincher"},
                new Breed {Name = "Cocer spaniol"},
                new Breed {Name = "Dachshund"},
                new Breed {Name = "Doberman"}
            });

            await data.SaveChangesAsync();
        }

        private static async Task RoleSeeder(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Administrator", "Client"};

            IdentityResult roleResult;

            foreach (var role in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);

                if (!roleExists)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private static async Task SeedAdministrator(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (await userManager.FindByNameAsync("admin") == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "admin",
                    FirstName = "admin",
                    LastName = "admin",
                    Email = "admin@admin.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "Admin123456");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Administrator");
                }
            }
            
        }
    }
}