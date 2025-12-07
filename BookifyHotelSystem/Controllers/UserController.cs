using BookifyHotelSystem.DatabaseServices.Repositories;
using BookifyHotelSystem.DatabaseServices.Unit_Of_Work;
using BookifyHotelSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookifyHotelSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IUnitOfWork unitOfWork;


        public UserController(UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> _signInManager,
            IUnitOfWork _unitOfWork)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            unitOfWork = _unitOfWork;
        }


        public IActionResult Index()
        {
            return View(unitOfWork.UserRepo.GetAll());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                if (user.PasswordHash != null)
                {
                    IdentityResult result = await userManager.CreateAsync(user, user.PasswordHash);
                    if (result.Succeeded)
                    {
                        TempData["save"] = "User Added successfully";
                        return RedirectToAction("Index");
                    }
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
                ModelState.AddModelError(string.Empty, "Invalid password");
            }
            return View(user);
        }

        public IActionResult Edit(string id)
        {
            var user = unitOfWork.UserRepo.GetById(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(string id, ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.UserRepo.Update(id, user);
                TempData["save"] = $"User Updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Details(string id)
        {
            var applicationUser = unitOfWork.UserRepo.GetById(id);
            return View(applicationUser);
        }

        public IActionResult Lockout(string id)
        {
            if (id == null)
                return NotFound();

            ApplicationUser user = unitOfWork.UserRepo.GetById(id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        public IActionResult Lockout(ApplicationUser user)
        {
            var userDb = unitOfWork.UserRepo.GetById(user.Id);
            if (userDb == null)
                return NotFound();

            int rowAffected = unitOfWork.UserRepo.Lockout(userDb);
            if (rowAffected > 0)
            {
                TempData["save"] = "User Locked Successfully";
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public IActionResult Active(string id)
        {
            if (id == null)
                return NotFound();
            var user = unitOfWork.UserRepo.GetById(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        public IActionResult Active(ApplicationUser user)
        {
            var userDb = unitOfWork.UserRepo.GetById(user.Id);
            if (userDb == null)
                return NotFound();

            int rowAffected = unitOfWork.UserRepo.Active(userDb);

            if (rowAffected > 0)
            {
                TempData["save"] = "User Active Successfully";
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public IActionResult Delete(string id)
        {
            if (id == null)
                return NotFound();
            var user = unitOfWork.UserRepo.GetById(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        public IActionResult Delete(ApplicationUser user)
        {
            var userDb = unitOfWork.UserRepo.GetById(user.Id);
            if (userDb == null)
                return NotFound();

            int rowAffected = unitOfWork.UserRepo.Delete(userDb);

            if (rowAffected > 0)
            {
                TempData["save"] = "User Deleted Successfully";
                return RedirectToAction("Index");
            }
            return View(user);
        }
    }
}
