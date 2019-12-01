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

            if (!dbConrext.SensorTypes.Any())
            {
                CreateSensorTypes(context: dbConrext);
            }

            if (!dbConrext.DeviceTypes.Any())
            {
                CreateDeviceTypes(context: dbConrext);
            }

            if(!dbConrext.ConditionTypes.Any())
            {
                CreateConditionTypes(context: dbConrext);
            }

            if(!dbConrext.Measures.Any())
            {
                CreateMeasureTypes(context: dbConrext);
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

        private static void CreateSensorTypes(ApplicationDbConrext context)
        {
            context.SensorTypes.AddRange(
                    new SensorType { TypeName = "light" },
                    new SensorType { TypeName = "temperature" },
                    new SensorType { TypeName = "submersion" }
                );
            context.SaveChanges();
        }

        private static void CreateDeviceTypes(ApplicationDbConrext context)
        {
            context.DeviceTypes.AddRange(
                    new DeviceType { TypeName = "Switch" },
                    new DeviceType { TypeName = "Lamp" },
                    new DeviceType { TypeName = "LED lamp" },
                    new DeviceType { TypeName = "Virtual" }
                );
            context.SaveChanges();
        }

        private static void CreateConditionTypes(ApplicationDbConrext context)
        {
            context.ConditionTypes.AddRange(
                    new ConditionType { Type = ">" },
                    new ConditionType { Type = "<" },
                    new ConditionType { Type = "=" }
                );
        }

        private static void CreateMeasureTypes(ApplicationDbConrext context)
        {
            var deviceTypes = context.DeviceTypes;
            context.Measures.AddRange(
                    new Measure { MeasureName = "PWM", DeviceTypeId = deviceTypes.Where(p => p.TypeName == "LED lamp").FirstOrDefault().Id },
                    new Measure { MeasureName = "Switch", DeviceTypeId = deviceTypes.Where(p => p.TypeName == "Switch").FirstOrDefault().Id },
                    new Measure { MeasureName = "Lamp", DeviceTypeId = deviceTypes.Where(p => p.TypeName == "Lamp").FirstOrDefault().Id },
                    new Measure { MeasureName = "Virtual", DeviceTypeId = deviceTypes.Where(p => p.TypeName == "Virtual").FirstOrDefault().Id }
                );
            context.SaveChanges();
        }
    }
}
