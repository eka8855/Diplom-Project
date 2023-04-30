using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Common;
using Hotel.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Data.Seeding
{
    internal class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any())
            {
                return;
            }
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            await SeedAdmin(userManager, GlobalConstants.AdministratorRoleName);
        }
        private static async Task SeedAdmin(UserManager<ApplicationUser> userManager, string roleName)
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Admin@email.com",
                NormalizedUserName = "admin@email.com",
                Email = "Admin@email.com",
                NormalizedEmail = "admin@email.com",
            };

            await userManager.CreateAsync(user, "654321");
            await userManager.AddToRoleAsync(user, roleName);
        }
    }
}