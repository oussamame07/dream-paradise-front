using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DreamParadise.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DreamParadise.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MyContext _context;

        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }

        //*================ Home  view  action =============
        public IActionResult Index()
        {
            return View();
        }

        //*================ About  view  action =============
        public IActionResult About()
        {
            return View();
        }

        //*================ Rooms  view  action =============
        public IActionResult Rooms()
        {
            return View();
        }

        //*================ Book now view  action =============
        [SessionCheck]
        public IActionResult Booking()
        {
            return View();
        }

        //*================ Contact Us view  action =============
        public IActionResult ContactUs()
        {
            return View();
        }

[SessionCheck]
public IActionResult Reservations()
{
     // Get the total count of reservations
    int totalReservationsCount = _context.Reservations.Count();
    ViewBag.TotalReservationsCount = totalReservationsCount;

    // Get the user ID from the session
    int? userId = HttpContext.Session.GetInt32("UserId");

    // Check if user ID is available
    if (userId != null)
    {
        // Retrieve reservations for the logged-in user
        var userReservations = _context.Reservations
            .Include(r => r.UserWhoReserved)
            .Where(r => r.UserWhoReserved.UserId == userId) // Assuming the user ID is stored in the UserId property of Reservation
            .ToList();

        return View(userReservations);
    }
    else
    {
        // Handle case where user ID is not available (session expired or user not logged in)
        // You may redirect the user to the login page or display an error message
        return RedirectToAction("LogReg", "LogReg");
    }
}



//*================ Edit Reservation action =============
[HttpGet]
[SessionCheck]
public IActionResult EditReservation(int id)
{
    var reservation = _context.Reservations.FirstOrDefault(r => r.ReservationId == id);
    if (reservation == null)
    {
        return NotFound();
    }
    return View(reservation);
}

//*================ Update Reservation action =============
[HttpPost]
[SessionCheck]
public IActionResult UpdateReservation(Reservation updatedReservation)
{
    if (ModelState.IsValid)
    {
        _context.Reservations.Update(updatedReservation);
        _context.SaveChanges();
        return RedirectToAction("Reservations");
    }
    return View("EditReservation", updatedReservation);
}

//*================ Delete Reservation action =============
[SessionCheck]
public IActionResult DeleteReservation(int id)
{
    var reservation = _context.Reservations.FirstOrDefault(r => r.ReservationId == id);
    if (reservation != null)
    {
        _context.Reservations.Remove(reservation);
        _context.SaveChanges();
    }
    return RedirectToAction("Reservations");
}







[HttpPost("Reservations/new")]
public IActionResult CreateReservation(Reservation newReservation)
{
    // Retrieve the current user's ID from the session or authentication context
    int? currentUserId = HttpContext.Session.GetInt32("UserId");
    
    if (currentUserId == null)
    {
        // Handle the case where the user is not logged in or the session has expired
        return RedirectToAction("Login", "Account");
    }
    
    if (ModelState.IsValid)
    {
        // Set the UserWhoReserved property
        newReservation.UserWhoReserved = _context.Users.Find(currentUserId);

        // Save the reservation
        _context.Add(newReservation);
        _context.SaveChanges();

        return RedirectToAction("Reservations");
    }
    else
    {
        return View("Booking", newReservation);
    }
}










        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //*================ Session check attribute  =============
        public class SessionCheckAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                int? userId = context.HttpContext.Session.GetInt32("UserId");
                if (userId == null)
                {
                    context.Result = new RedirectToActionResult("LogReg", "LogReg", null);
                }
            }
        }
    }
}






    