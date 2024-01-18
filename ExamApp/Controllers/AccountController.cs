using ExamApp.Context;
using ExamApp.Models;
using ExamApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExamApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = new AppUser()
            {
                Email = register.Email,
                Name = register.Name,
                Surname = register.Surname,
                UserName = register.Username
            };
            var result = await _userManager.CreateAsync(user , register.Password);
            if (!result.Succeeded) 
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    return View();
                }          
            }


            //await _userManager.AddToRoleAsync(user, UserRole.Admin.ToString());

            return RedirectToAction("Login");
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(login.EmailOrUsername);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(login.EmailOrUsername);
                if(user is null)
                {
                    ModelState.AddModelError("", "Username-Email or Password is incorrect");
                    return View();
                }
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password , false);

            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "Username-Email or Password is incorrect");
                return View();
            }

            return RedirectToAction(nameof(Index), "Home");           
        }
    }
}
