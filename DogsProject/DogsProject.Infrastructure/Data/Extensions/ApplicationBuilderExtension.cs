using DogsProject.Infrastructure.Data.Entities;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsProject.Infrastructure.Data.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
        {
            await using var serviceScope = app.ApplicationServices.CreateAsyncScope();

            var services = serviceScope.ServiceProvider;

            var data = services.GetRequiredService<ApplicationDbContext>();
            SeedBreeds(data);

            return app;
        }

        private async static void SeedBreeds(ApplicationDbContext data)
        {
            if (data.Breeds.Any())
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
    }
}
