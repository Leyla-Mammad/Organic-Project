using Microsoft.AspNetCore.Identity;
using Organic_Shop_project.Constants;
using Organic_Shop_project.Models;

namespace Organic_Shop_project.Helpers
{
    public static class DbInitializer
    {
        public async static Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {


            foreach (var role in Enum.GetValues(typeof(UserRoles)))
            { 
                if (!await roleManager.RoleExistsAsync(role.ToString()))
                {
                    await roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role.ToString(),
                    });

                }

            }

            User admin = new()
            {

                FullName = "Leyla Mammadova",
                UserName = "Leyla",
                Email = "Leilamammad1996@gmail.com",
            };
            if(await userManager.FindByNameAsync(admin.UserName)==null)
            {


                var result = await userManager.CreateAsync(admin, "Admin0000");
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        throw new Exception(error.Description);
                    }
                }
                await userManager.AddToRoleAsync(admin, UserRoles.SuperAdmin.ToString());
            }
        }

    }
}
