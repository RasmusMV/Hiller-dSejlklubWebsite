using Hillerød_Sejlklub.Interfaces;
using Hillerød_Sejlklub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HillerødSejlklubWebsite.Pages
{
    public class EditBookingsModel : PageModel
    {
        IBookingRepository bookingRepo;
        IUserRepository userRepo;
        IBoatRepository boatRepo;

        [BindProperty]
        public int BookingId { get; set; }

        [BindProperty]
        public string BoatName { get; set; }

        [BindProperty]
        public string Destination { get; set; }

        [BindProperty]
        public DateTime Start { get; set; }

        [BindProperty]
        public DateTime Finish { get; set; }

        public List<Boat> Boats { get; set; }

        public EditBookingsModel(IBookingRepository repository, IUserRepository userRepository, IBoatRepository boatRepository)
        {
            bookingRepo = repository;
            userRepo = userRepository;
            boatRepo = boatRepository;
        }
        public IActionResult OnGet(int id)
        {
            var booking = bookingRepo.BookingList.GetValueOrDefault(id);
            if (booking == null)
                return RedirectToPage("YourBookings");

            BookingId = booking.Id;
            BoatName = booking.Boat?.Name;
            Destination = booking.Destination;
            Start = booking.DateStart;
            Finish = booking.DateFinish;

            Boats = boatRepo.GetAll();
            return Page();
        }

        public IActionResult OnPost()
        {
            Boats = boatRepo.GetAll();

            if (!ModelState.IsValid)
                return Page();

            var booking = bookingRepo.BookingList.GetValueOrDefault(BookingId);
            if (booking == null)
                return RedirectToPage("YourBookings");

            var boat = boatRepo.GetBoatByName(BoatName);
            if (boat == null)
            {
                ModelState.AddModelError("BoatName", "Invalid boat selected");
                return Page();
            }

            bookingRepo.UpdateBookingDestination(booking, Destination);
            bookingRepo.UpdateBookingBoat(booking, boat);
            bookingRepo.UpdateBookingDate(booking, Start, Finish);

            return RedirectToPage("YourBookings");
        }
    }
}
