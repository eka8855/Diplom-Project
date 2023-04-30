using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Data.Models;

using Hotel.Services.Mapping;

namespace Hotel.Web.ViewModels.Administration.Bookings
{
	public class BookingModel : IMapFrom<Booking>
	{
		public int Id { get; set; }
		public string CustomerName { get; set; }
		public string CustomerEmail { get; set; }
		public string CustomerPhone { get; set; }

		public DateTime CheckIn { get; set; }

		public DateTime CheckOut { get; set; }

		public int RoomsCount { get; set; }

		public bool Confirmed { get; set; }

		public string RoomName { get; set; }
		public string RoomType { get; set; }

		public decimal Price { get; set; }

		public Room Room { get; set; }
	}
}