using BookifyHotelSystem.DatabaseServices.Repositories;
using BookifyHotelSystem.DatabaseServices.Unit_Of_Work;
using BookifyHotelSystem.Models;
using BookifyHotelSystem.View_Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookifyHotelSystem.Controllers
{
    public class RoleController : Controller
    { 
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUnitOfWork unitOfWork;

        public RoleController(RoleManager<IdentityRole> _roleManager,
            IUnitOfWork _unitOfWork,
            UserManager<ApplicationUser> _userManager)
        {
            roleManager = _roleManager;
            unitOfWork = _unitOfWork;
            userManager = _userManager;
        }


        public IActionResult Index()
        {
            List<IdentityRole> roles = unitOfWork.RoleRepo.GetAll();
            ViewBag.Roles = roles;
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            IdentityRole newRole = new IdentityRole();
            newRole.Name = name;

            IdentityResult result = await roleManager.CreateAsync(newRole);

            if (result.Succeeded)
            {
                TempData["save"] = "Role Added successfully";
                return RedirectToAction("Index");
            }
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View();
        }


        public IActionResult Edit(string id)
        {
            if (id == null)
                return NotFound();
            var role = unitOfWork.RoleRepo.GetById(id);
            if (role == null)
                return NotFound();
            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, IdentityRole roleUpdated)
        {
            if (ModelState.IsValid)
            {
                var role = unitOfWork.RoleRepo.GetById(id);
                if (role == null)
                    return NotFound();

                var result = await unitOfWork.RoleRepo.Update(role.Id, roleUpdated);
                if (result.Succeeded)
                {
                    TempData["save"] = "Role Updated successfully";
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(roleUpdated);
        }


        public IActionResult Delete(string id)
        {
            if (id == null)
                return NotFound();
            var role = unitOfWork.RoleRepo.GetById(id);
            if (role == null)
                return NotFound();
            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, IdentityRole roleDeleted)
        {
            var role = unitOfWork.RoleRepo.GetById(id);
            if (role == null)
                return NotFound();
            var result = await unitOfWork.RoleRepo.Delete(role);
            if (result.Succeeded)
            {
                TempData["save"] = "Role Deleted successfully";
                return RedirectToAction("Index");
            }
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(roleDeleted);
        }


        public async Task<IActionResult> Assign()
        {
            ViewBag.UserId =
                new SelectList(unitOfWork.UserRepo.GetAll().Where(u => u.LockoutEnd < DateTime.Now || u.LockoutEnd == null).ToList(), "Id", "UserName");
            ViewBag.RoleId =
                new SelectList(unitOfWork.RoleRepo.GetAll(), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(RoleUserViewModel roleUserVm)
        {
            var role = unitOfWork.RoleRepo.GetById(roleUserVm.RoleId);
            var user = unitOfWork.UserRepo.GetById(roleUserVm.UserId);
            if (user == null || role == null)
                return NotFound();

            var isCheckedRoleAssign = await userManager.IsInRoleAsync(user, role.Name);
            if (isCheckedRoleAssign)
            {
                ViewBag.msg = "this user already assign this role";
                ViewBag.UserId =
                new SelectList(unitOfWork.UserRepo.GetAll().Where(u => u.LockoutEnd < DateTime.Now || u.LockoutEnd == null).ToList(), "Id", "UserName");
                ViewBag.RoleId =
                    new SelectList(unitOfWork.RoleRepo.GetAll(), "Id", "Name");
                return View();
            }


            var result = await userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded)
            {
                TempData["save"] = "User role assigned successfully";
                return RedirectToAction("Index");
            }
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            ViewBag.UserId =
                new SelectList(unitOfWork.UserRepo.GetAll().Where(u => u.LockoutEnd < DateTime.Now || u.LockoutEnd == null).ToList(), "Id", "UserName");
            ViewBag.RoleId =
                new SelectList(unitOfWork.RoleRepo.GetAll(), "Id", "Name");
            return View();
        }


        public ActionResult AssignUserRole()
        {
            ViewBag.UserRoles = unitOfWork.UserRepo.GetUsersWithRoles();

            return View();
        }
    }
}
