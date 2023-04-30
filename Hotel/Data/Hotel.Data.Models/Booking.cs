using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

using Hotel.Data.Common.Models;

namespace Hotel.Data.Models
{
    public class Booking : BaseDeletableModel<int>
    {
        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerPhone { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public int RoomsCount { get; set; }

        public bool Confirmed { get; set; }

        public int RoomId { get; set; }

        public virtual Room Room { get; set; }
    }
}
