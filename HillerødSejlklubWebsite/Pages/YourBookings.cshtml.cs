using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Hillerød_Sejlklub;
using Hillerød_Sejlklub.Repositories;
using Hillerød_Sejlklub.Interfaces;

namespace HillerødSejlklubWebsite.Pages
{
    public class YourBookingsModel : PageModel
    {
        IBookingRepository repo;

        public Dictionary<int, Booking> Bookings { get; private set; }

        [BindProperty]
        public string FilterCriteria { get; set; }

        public YourBookingsModel(IBookingRepository repository)
        {
            repo = repository;
        }

        public void OnGet()
        {
            Bookings = repo.BookingList;
        }

        public IActionResult OnGetDelete(int id)
        {
            var ev = repo.BookingList[id];
            if (ev != null)
            {
                repo.DeleteBooking(ev);
            }

            // Redirect tilbage til listen, så siden opdateres
            return RedirectToPage("YourBookings");
        }

    }
}
