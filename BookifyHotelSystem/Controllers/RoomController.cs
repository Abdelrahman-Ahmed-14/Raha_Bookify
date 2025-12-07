using BookifyHotelSystem.DatabaseServices.Repositories;
using BookifyHotelSystem.DatabaseServices.Unit_Of_Work;
using BookifyHotelSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookifyHotelSystem.Controllers
{
    public class RoomController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment iwhe;

        public RoomController(IUnitOfWork _unitOfWork, IWebHostEnvironment _iwhe)
        {
            unitOfWork = _unitOfWork;
            iwhe = _iwhe;
        }


        public IActionResult Index()
        {
            var rooms = unitOfWork.RoomRepo.GetAllWithInclude();
            return View(rooms);
        }

        public IActionResult Create()
        {
            var roomTypeList = unitOfWork.RoomTypeRepo.GetAll().ToList();
            SelectList roomItems = new SelectList(roomTypeList, "Id", "TypeName");

            ViewBag.RoomTypes = roomItems;

            return View();
        }

        [HttpPost]
        public IActionResult Create(Room room)
        {
            if (ModelState.IsValid)
            {
                if (room.ImageFile != null)
                {
                    string uploadPath = Path.Combine(iwhe.WebRootPath, "Images");
                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);

                    string fileName = Path.GetFileName(room.ImageFile.FileName);
                    string filePath = Path.Combine(uploadPath, fileName);

                    using var stream = new FileStream(filePath, FileMode.Create);

                    room.ImageFile.CopyTo(stream);

                    room.Image = "/Images/" + fileName;
                }
                else
                {
                    room.Image = "/Images/notFound.png";
                }
                unitOfWork.RoomRepo.Add(room);
                TempData["save"] = "Room Added successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            if (id != null)
            {
                var roomTypeList = unitOfWork.RoomTypeRepo.GetAll().ToList();
                SelectList roomItems = new SelectList(roomTypeList, "Id", "TypeName");
                ViewBag.RoomTypes = roomItems;
                var room = unitOfWork.RoomRepo.GetById(id);
                return View(room);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Room room)
        {
            if (ModelState.IsValid)
            {
                if (room.ImageFile != null)
                {
                    string uploadPath = Path.Combine(iwhe.WebRootPath, "Images");
                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);

                    string fileName = Path.GetFileName(room.ImageFile.FileName);
                    string filePath = Path.Combine(uploadPath, fileName);

                    using var stream = new FileStream(filePath, FileMode.Create);

                    room.ImageFile.CopyTo(stream);

                    room.Image = "/Images/" + fileName;
                }
                else
                {
                    room.Image = "/Images/notFound.png";
                }

                unitOfWork.RoomRepo.Update(room.Id, room);
                TempData["save"] = "Room updated successfully";
                return RedirectToAction("Index");
            }
            var roomTypeList = unitOfWork.RoomTypeRepo.GetAll().ToList();
            SelectList roomItems = new SelectList(roomTypeList, "Id", "TypeName");
            ViewBag.RoomTypes = roomItems;
            return View(room);
        }

        public IActionResult Details(int id)
        {
            var rTypes = unitOfWork.RoomTypeRepo.GetAll();
            SelectList list = new SelectList(rTypes, "Id", "TypeName");
            return View(unitOfWork.RoomRepo.GetById(id));
        }

        public IActionResult Delete(int id)
        {
            var rTypes = unitOfWork.RoomTypeRepo.GetAll();
            SelectList list = new SelectList(rTypes, "Id", "TypeName");
            return View(unitOfWork.RoomRepo.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Room room)
        {
            unitOfWork.RoomRepo.Delete(room);
            TempData["save"] = "room deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
