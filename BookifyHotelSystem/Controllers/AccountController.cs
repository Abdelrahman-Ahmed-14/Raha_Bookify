using BookifyHotelSystem.Models;
using BookifyHotelSystem.View_Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookifyHotelSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginVm)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser applicationUser = await userManager.FindByNameAsync(loginVm.UserName);

                if (applicationUser is not null)
                {
                    bool isFound = await userManager.CheckPasswordAsync(applicationUser, loginVm.Password);

                    if (isFound)
                    { 
                        if(applicationUser.LockoutEnd !=null) 
                        {
                            return View("LockedPage");
                        }

                        await signInManager.SignInAsync(applicationUser, loginVm.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid user name or password");

            }
            return View(loginVm);
        }

        public IActionResult Register()
        {
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegiterViewModel registerVm)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser()
                {
                    UserName = registerVm.UserName,
                    Email = registerVm.Email,
                    PasswordHash = registerVm.Password,
                    FullName = registerVm.FullName,
                };

                IdentityResult result = await userManager.CreateAsync(applicationUser,registerVm.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(applicationUser, false);
                
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(registerVm);
        }


        public IActionResult Logout()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
