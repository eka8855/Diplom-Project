using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Data.Common.Models;

using Microsoft.AspNetCore.Http;

namespace Hotel.Data.Models
{
    public class Room : BaseDeletableModel<int>
    {
        public Room()
        {
            this.Bookings = new HashSet<Booking>();
        }

        public string Name { get; set; }

        public string Type { get; set; }

        public decimal Price { get; set; }

        public int NumberAvailable { get; set; }
   
		public string ImageName { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
