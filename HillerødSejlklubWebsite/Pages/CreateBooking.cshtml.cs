using Hillerød_Sejlklub.Interfaces;
using Hillerød_Sejlklub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Hillerød_Sejlklub.Repositories;

namespace HillerødSejlklubWebsite.Pages
{
    public class CreateBookingModel : PageModel
    {
        IBookingRepository bookingRepo;
        IUserRepository userRepo;
        IBoatRepository boatRepo;

        [BindProperty]
        public string Boat { get; set; }
        [BindProperty]
        public string Destination { get; set; }
        [BindProperty]
        public DateTime Start { get; set; }
        [BindProperty]
        public DateTime Finish { get; set; }

        public List<Boat> Boats { get; set; }
        public CreateBookingModel(IBookingRepository repository, IUserRepository userRepository, IBoatRepository boatRepository)
        {
            bookingRepo = repository;
            userRepo = userRepository;
            boatRepo = boatRepository;
        }

        public void OnGet()
        {
            Boats = boatRepo.GetAll();
        }

        public IActionResult OnPost()
        {
            Boats = boatRepo.GetAll();

            var boat = boatRepo.GetBoatByName(Boat);
            if (boat == null)
            {
                ModelState.AddModelError("BoatName", "Invalid boat selected");
                return Page();
            }
            bookingRepo.AddBooking(userRepo.GetByName("Jan"), boatRepo.GetBoatByName(Boat), Destination, Start, Finish);
            return RedirectToPage("YourBookings");
        }
    }
}
