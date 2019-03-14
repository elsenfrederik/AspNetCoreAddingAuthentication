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
        public ActionResult Register()
        {
            return View("Register");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("Register", registerViewModel);
        }
    }
}
