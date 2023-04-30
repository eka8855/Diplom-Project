using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Data.Models;
using Hotel.Services.Mapping;

using Microsoft.AspNetCore.Http;

namespace Hotel.Web.ViewModels.Administration.Rooms
{
    public class RoomInputModel : IMapFrom<Room>
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public decimal Price { get; set; }

        public int NumberAvailable { get; set; }

		public string ImageName { get; set; }

		public IFormFile Image { get; set; }

	}
}
