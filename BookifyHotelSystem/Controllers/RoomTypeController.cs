using BookifyHotelSystem.DatabaseServices.Repositories;
using BookifyHotelSystem.DatabaseServices.Unit_Of_Work;
using BookifyHotelSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;

namespace BookifyHotelSystem.Controllers
{
    public class RoomTypeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public RoomTypeController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }


        public IActionResult Index()
        {
            var roomTypesList = unitOfWork.RoomTypeRepo.GetAll();
            return View(roomTypesList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RoomType roomType)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.RoomTypeRepo.Add(roomType);
                TempData["save"] = "Room Type Added successfully";
                return RedirectToAction("Index");
            }
            return View(roomType);
        }
       

        public IActionResult Edit(int id)
        {
            if (id != null)
            {
                var roomTypeDb = unitOfWork.RoomTypeRepo.GetById(id);
                return View(roomTypeDb);
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult Edit(RoomType roomType)
        {
            if(ModelState.IsValid)
            {
                unitOfWork.RoomTypeRepo.Update(roomType.Id, roomType);
                TempData["save"] = "Room Type Updated successfully";
                return RedirectToAction("Index");
            }
            return View(roomType);
        }

        public IActionResult Delete(int id)
        {
            if(id!=null)
            {
                var roomType = unitOfWork.RoomTypeRepo.GetById(id);
                return View(roomType);
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult Delete(RoomType roomType)
        {
            unitOfWork.RoomTypeRepo.Delete(roomType);
            TempData["save"] = "Room Type Deleted successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var rType = unitOfWork.RoomTypeRepo.GetById(id);
            return View(rType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(RoomType roomType)
        {
            return RedirectToAction("Index");
        }
    }
}
