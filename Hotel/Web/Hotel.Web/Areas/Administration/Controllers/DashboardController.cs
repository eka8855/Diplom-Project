using Hotel.Services;
using Hotel.Web.ViewModels.Administration.Dashboard;

using Microsoft.AspNetCore.Mvc;

namespace Hotel.Web.Areas.Administration.Controllers
{
    public class DashboardController : BaseController
    {

        private IBookingsService bookingsService;
        private IRoomsService roomsService;

        public DashboardController(IBookingsService bookingsService, IRoomsService roomsService)
        {
            this.bookingsService = bookingsService;
            this.roomsService = roomsService;
        }

        public IActionResult Index()
        {
            var bookingsCount = this.bookingsService.Count();
            var roomsCount = this.roomsService.Count();

            return View(new IndexModel {BookingsCount = bookingsCount, RoomsCount = roomsCount });
        }
    }
}
