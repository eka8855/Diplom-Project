using Hotel.Services;
using Hotel.Web.ViewModels.Administration.Rooms;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Hotel.Data.Models;
using Hotel.Web.ViewModels.Administration.Bookings;
using Hotel.Web.ViewModels.Rooms;
using System.Linq;
using System.Collections.Generic;
using System;
using Hotel.Web.ViewModels.Home;

namespace Hotel.Web.Areas.Administration.Controllers
{
	public class BookingsController : BaseController
	{
		private IBookingsService bookingsService;
		private IRoomsService roomsService;

		public BookingsController(IBookingsService bookingsService, IRoomsService roomsService)
		{
			this.bookingsService = bookingsService;
			this.roomsService = roomsService;
		}
		[Authorize]
		[HttpGet]
		public IActionResult Index()
		{
			var bookings = bookingsService.GetAll<BookingModel>();
			var days = 0;
			foreach(var booking in bookings)
			{
				days = (booking.CheckOut - booking.CheckIn).Days;
				booking.Price = booking.RoomsCount * (booking.Room.Price * days);
			}
			return View(bookings);
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Leave(int id)
		{
			var booking = bookingsService.GetById<HomeViewModel>(id);
			var room = roomsService.GetById<RoomInputModel>(booking.RoomId);
			room.NumberAvailable = room.NumberAvailable + booking.RoomsCount;
			await this.roomsService.UpdateAsync(booking.RoomId, room);
			await this.bookingsService.DeleteAsync(id);
			return this.RedirectToAction(nameof(this.Index));
		}


		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Confirm(int id)
		{
			var booking = bookingsService.GetById<HomeViewModel>(id);
			booking.Confirmed = true;
			await this.bookingsService.UpdateAsync(id, booking);
            var room = roomsService.GetById<RoomInputModel>(booking.RoomId);
			room.NumberAvailable = room.NumberAvailable - booking.RoomsCount;
            await this.roomsService.UpdateAsync(booking.RoomId, room);
            return this.RedirectToAction(nameof(this.Index));
		}
	}
}

