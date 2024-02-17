using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Organic_Shop_project.Areas.Admin.ViewModels.Account;
using Organic_Shop_project.Constants;
using Organic_Shop_project.Controllers;
using Organic_Shop_project.Models;
using System.Runtime.Serialization;

namespace Organic_Shop_project.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

		[OnlyAnonymous]
		public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginVM model)
        {
            if (!ModelState.IsValid) return View(model);
            User user = await _userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Password or Username is Invalid");
                return View(model);
            }

            bool isSuperAdmin = await _userManager.IsInRoleAsync(user, UserRoles.SuperAdmin.ToString());
            if (isSuperAdmin)
            {
                ModelState.AddModelError(string.Empty, "Username or Password is incorrect");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Password or Username is Invalid");
                return View(model);
            }
            return RedirectToAction(nameof(Index), "dashboard");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }



    }; }
