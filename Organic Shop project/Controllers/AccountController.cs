using Microsoft.AspNetCore.Mvc;
using Organic_Shop_project.ViewModels.Account;
using System.Runtime.CompilerServices;
using Organic_Shop_project.Models;
using Microsoft.AspNetCore.Identity;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using Organic_Shop_project.Attributes;

namespace Organic_Shop_project.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public UserManager<User> UserManager { get; }


        [OnlyAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginViewModel model)

        {
            if (!ModelState.IsValid) return View(model);
            User user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Password or Username is Invalid");
                return View(model);
            }
            SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Password or Username is Invalid");
                return View(model);
            }
            return RedirectToAction(nameof(Index), "home");
        }


        [OnlyAnonymous]
        public async Task<IActionResult> SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(AccountSignupViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            User newUser = new User
            {
                FullName = model.FullName,
                UserName = model.UserName,
                Email = model.Email
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            return RedirectToAction("Login");
        }


        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
    }

    internal class OnlyAnonymousAttribute : Attribute
    {
    }
}
