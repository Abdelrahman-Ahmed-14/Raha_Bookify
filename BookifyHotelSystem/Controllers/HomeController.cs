using BookifyHotelSystem.DatabaseServices.Repositories;
using BookifyHotelSystem.DatabaseServices.Unit_Of_Work;
using BookifyHotelSystem.Models;
using BookifyHotelSystem.Utilities;
using BookifyHotelSystem.View_Model;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace BookifyHotelSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork _unitOfWork)
        {
            _logger = logger;
            unitOfWork = _unitOfWork;
        }



        public IActionResult Index(int? page)
        {
            return View(unitOfWork.RoomRepo.GetAll().ToPagedList(pageNumber: page ?? 1, pageSize: 3));
        }


        #region MyRegion
        public IActionResult Privacy()
        {
            return View();
        }
        #endregion

        public IActionResult Details(int id)
        {
            if (id <= 0)
                return NotFound();

            var room = unitOfWork.RoomRepo.FindByIdWithInclude(id);
            if (room == null)
                return NotFound();

            // Check if this room is already in the cart and get saved dates
            List<RoomViewModel> rooms = HttpContext.Session.Get<List<RoomViewModel>>("rooms");
            if (rooms != null)
            {
                var existingRoom = rooms.FirstOrDefault(r => r.Id == id);
                if (existingRoom != null)
                {
                    ViewBag.StartDate = existingRoom.StartDate?.ToString("yyyy-MM-dd");
                    ViewBag.EndDate = existingRoom.EndDate?.ToString("yyyy-MM-dd");
                }
            }

            return View(room);
        }


        [ActionName("Details")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RoomDetails(int id, DateTime? StartDate, DateTime? EndDate)
        {
            Room room = unitOfWork.RoomRepo.FindByIdWithInclude(id);

            List<RoomViewModel> rooms = HttpContext.Session.Get<List<RoomViewModel>>("rooms");

            if (rooms == null)
            {
                rooms = new List<RoomViewModel>();
            }

            // Check if room already exists in cart
            var existingRoom = rooms.FirstOrDefault(r => r.Id == id);

            if (existingRoom != null)
            {
                // Update existing room dates
                existingRoom.StartDate = StartDate ?? DateTime.Now;
                existingRoom.EndDate = EndDate ?? DateTime.Now.AddDays(1);
            }
            else
            {
                // Add new room to cart
                rooms.Add(new RoomViewModel()
                {
                    Id = room.Id,
                    Name = room.Name,
                    PricePerNight = room.PricePerNight,
                    IsAvailable = room.IsAvailable,
                    RoomTypeName = room.RoomType?.TypeName ?? "",
                    Image = room.Image,
                    StartDate = StartDate ?? DateTime.Now,
                    EndDate = EndDate ?? DateTime.Now.AddDays(1)
                });
            }

            HttpContext.Session.Set("rooms", rooms);
            return RedirectToAction("Cart");
        }


        public IActionResult Remove(int id)
        {
            List<RoomViewModel> rooms = HttpContext.Session.Get<List<RoomViewModel>>("rooms");

            if (rooms != null)
            {
                var room = rooms.FirstOrDefault(r => r.Id == id);
                if (room is not null)
                {
                    rooms.Remove(room);
                    HttpContext.Session.Set("rooms", rooms);
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Cart()
        {
            List<RoomViewModel> roomsVM =
                HttpContext.Session.Get<List<RoomViewModel>>("rooms");

            if (roomsVM == null)
                roomsVM = new List<RoomViewModel>();

            return View(roomsVM);
        }
    }
}
