using BookifyHotelSystem.DatabaseServices.Repositories;
using BookifyHotelSystem.DatabaseServices.Unit_Of_Work;
using BookifyHotelSystem.Models;
using BookifyHotelSystem.Utilities;
using BookifyHotelSystem.View_Model;
using Microsoft.AspNetCore.Mvc;

namespace BookifyHotelSystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public BookingController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        
        public IActionResult Checkout()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                ViewBag.UserName = User.Identity.Name;
            }
            else
            {
                ViewBag.UserName = "Anonymous";
            }
                ViewBag.OrderNumber = GetOrderNo();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(Booking booking)
        {
            List<RoomViewModel> roomsVM =
                HttpContext.Session.Get<List<RoomViewModel>>("rooms");

            if (roomsVM != null && roomsVM.Any())
            {
                decimal nights = (decimal)(booking.EndDate - booking.StartDate).TotalDays;
                if (nights < 1) nights = 1;  

                decimal total = 0;

                foreach (var r in roomsVM)
                {
                    RoomBooking roomBooking = new RoomBooking();
                    roomBooking.RoomId = r.Id;

                    booking.RoomBookings.Add(roomBooking);

                    total += (r.PricePerNight * nights);
                }
                booking.TotalPrice = (int)total;  
            }
            //booking.BookingNo = GetOrderNo();
            unitOfWork.BookingRepo.Add(booking);

            HttpContext.Session.Set("rooms", new List<RoomViewModel>());
            return View();
        }

        public int GetOrderNo()
        {
            int rowCount = unitOfWork.BookingRepo.GetAll().Count() + 1;
            return rowCount;
        }
    }
}
