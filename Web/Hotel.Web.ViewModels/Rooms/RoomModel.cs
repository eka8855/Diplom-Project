using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Data.Models;
using Hotel.Services.Mapping;

using Microsoft.AspNetCore.Http;

namespace Hotel.Web.ViewModels.Rooms
{
    public class RoomModel : IMapFrom<Room>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Type { get; set; }

        public decimal Price { get; set; }

        public int NumberAvailable { get; set; }

		public string ImageName { get; set; }

	}
}
