using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Organic_Shop_project.Constants;
using Organic_Shop_project.Models;

namespace Organic_Shop_project.Controllers
{
    public class TempController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TempController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Test()
        {
            foreach( var role in Enum.GetValues(typeof(Constants.UserRoles)))
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                   Name=role.ToString(),
                }
                    );
            }
            User admin = new()
            {

                FullName = "Leyla Mammadova",
                UserName = "Leyla",
                Email = "Leilamammad1996@gmail.com",
            };

            var result = await _userManager.CreateAsync(admin, "Admin0000");
            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    throw new Exception(error.Description);
                }
            }
            await _userManager.AddToRoleAsync(admin, UserRoles.SuperAdmin.ToString());
            return Ok();
        }
    }
}
