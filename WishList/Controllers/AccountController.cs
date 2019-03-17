using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using WishList.Models;
using WishList.Models.AccountViewModels;

namespace WishList.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> um, SignInManager<ApplicationUser> sim)
        {
            _userManager = um;
            _signInManager = sim;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            return RedirectToAction("Index", "Item");
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var result =_userManager.CreateAsync(new ApplicationUser {Email = registerViewModel.Email, UserName = registerViewModel.Email},registerViewModel.Password);
                if (!result.Result.Succeeded)
                {
                    foreach (var error in result.Result.Errors)
                    {
                        ModelState.AddModelError("Password",error.Description);
                    }
                    return View("Register", registerViewModel);
                }

                return RedirectToAction("Index", "Home");
            }

            return View("Register", registerViewModel);
        }
    }
}
