using DataBase.Entities.Concretes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using NeoGym_Project.ViewModels;

namespace NeoGym_Project.Controllers.Account
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


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }
            User user = new User()
            {
                UserName=registerVM.Email,
                FirstName=registerVM.Name,
                LastName=registerVM.Surname,
                Email=registerVM.Email,
            };
            var result = await _userManager.CreateAsync(user, registerVM.Password);

            if(!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            return RedirectToAction("Login");
        }

        [HttpGet]

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            User user;
            if (loginVM.Email.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(loginVM.Email);
            }
            else
            {
                user = await _userManager.FindByNameAsync(loginVM.Email);
            }

            if(user == null)
            {
                ModelState.AddModelError("","Invalid Username Or Password!");
                return View();
            }

            var result=await _signInManager.CheckPasswordSignInAsync(user, loginVM.Password,true);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Please Try Again Later!");
                return View();
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid Username Or Password!");
                return View();
            }

            await _signInManager.SignInAsync(user, loginVM.RememberMe);

            return RedirectToAction("Index", "Home");
        }
        public async  Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
