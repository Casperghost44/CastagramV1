using CastagramV1.Models;
using CastagramV1.Repositories;
using CastagramV1.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CastagramV1.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DBContext _db;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, DBContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.Email);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);

                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Post");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please, try again!";
                return View(model);
            }

            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(model);
        }

        public IActionResult Register()
        {
            return View(new RegistrationViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newUser = new User()
                {
                    Email = model.Email,
                    Name = model.Name,
                    UserName = model.Name
                };
                var newUserResponse = await _userManager.CreateAsync(newUser, model.Password);

                if (newUserResponse.Succeeded)
                {
                    var signInResult = await _signInManager.PasswordSignInAsync(newUser, model.Password, false, false);

                    if (!signInResult.Succeeded)
                    {
                        TempData["Error"] = "Unable to sign in user";
                        return View(model);
                    }

                    return RedirectToAction("Index", "Post");
                }
                else
                {
                    foreach (var error in newUserResponse.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Post");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }



    }
}
