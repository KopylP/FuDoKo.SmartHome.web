using FuDoKo.SmartHome.web.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.Data
{
    public class DbSeeder
    {
        public static void Seed(ApplicationDbConrext dbConrext, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            if (!dbConrext.Users.Any())
            {
                CreateUsers(dbConrext, roleManager, userManager)
                    .GetAwaiter()
                    .GetResult();
            }
            if(!dbConrext.SensorTypes.Any())
            {
                CreateSensorTypes(conrext: dbConrext);
            }
        }
        private static async Task CreateUsers(ApplicationDbConrext dbConrext, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            string roleAdmin = "admin";
            string roleUser = "user";

            if (!await roleManager.RoleExistsAsync(roleUser))
            {
                await roleManager.CreateAsync(new IdentityRole(roleUser));
            }

            if (!await roleManager.RoleExistsAsync(roleAdmin))
            {
                await roleManager.CreateAsync(new IdentityRole(roleAdmin));
            }

            var now = DateTime.Now;

            var admin = new ApplicationUser
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "Admin",
                Email = "p.kopyl9@gmail.com",
                Name = "Admin",
                Surname = "Admin",
                DisplayName = "Admin Admin",
                CreatedTime = now,
                LastModifiedDate = now
            };
            if (await userManager.FindByNameAsync(admin.UserName) == null)
            {
                await userManager.CreateAsync(admin, "Pass4Admin");
                await userManager.AddToRoleAsync(admin, roleAdmin);
                await userManager.AddToRoleAsync(admin, roleUser);
                admin.EmailConfirmed = true;
                admin.LockoutEnabled = false;
            }
            dbConrext.SaveChanges();
        }

        private static void CreateSensorTypes(ApplicationDbConrext conrext)
        {
            conrext.SensorTypes.AddRange(
                    new SensorType { TypeName = "light" },
                    new SensorType { TypeName = "temperature" },
                    new SensorType { TypeName = "submersion" }
                );
            conrext.SaveChanges();
        }
    }
}
