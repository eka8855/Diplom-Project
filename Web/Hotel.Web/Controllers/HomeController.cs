namespace Hotel.Web.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Services;
    using Hotel.Web.ViewModels;
    using Hotel.Web.ViewModels.Home;
    using Hotel.Web.ViewModels.Rooms;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private IBookingsService bookingsService;
        private IRoomsService roomsService;

        public HomeController(IBookingsService bookingsService, IRoomsService roomsService)
        {
            this.bookingsService = bookingsService;
            this.roomsService = roomsService;
        }

		[HttpGet]
		public IActionResult GetCount(int roomId)
		{
			var room = roomsService.GetById<RoomModel>(roomId);
			var availableRooms = room.NumberAvailable;
			return new JsonResult(availableRooms);
		}
		public IActionResult Index()
        {
            var rooms = this.roomsService.GetAll<RoomModel>().ToList();
            var options = rooms.Select(a =>
                                 new {
                                     Key = a.Id,
                                     Value = a.Name + " | " + a.Type,
                                 }).ToList().Select(x => new KeyValuePair<string, string>(x.Key.ToString(), x.Value));
            var model = new HomeViewModel { RoomsOptions = options, roomsModel = rooms };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> BookARoom(HomeViewModel model)
        {


            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            await this.bookingsService.CreateAsync(model);

            return this.RedirectToAction(nameof(this.Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
