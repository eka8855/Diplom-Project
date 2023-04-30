using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Data.Models;
using Hotel.Services.Mapping;
using Hotel.Web.ViewModels.Rooms;

namespace Hotel.Web.ViewModels.Home
{
    public class HomeViewModel : IMapFrom<Booking>
    {
        [Required(ErrorMessage = "Името е задължително")]
        [MaxLength(20)]
        [MinLength(3)]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Имейлът е задължителен")]
        [EmailAddress(ErrorMessage = "Имейлът трябва да е валиден")]
        public string CustomerEmail { get; set; }

        [Required(ErrorMessage = "Телефонът е задължителен")]
        [MaxLength(13, ErrorMessage = "Телефонът трябва да е валиден")]
        [MinLength(10, ErrorMessage = "Телефонът трябва да е валиден")]
        public string CustomerPhone { get; set; }
        [Required(ErrorMessage = "Датата за настаняване е задължителна")]
        public DateTime CheckIn { get; set; }
        [Required(ErrorMessage = "Датата за oсвобождаване е задължителна")]
        public DateTime CheckOut { get; set; }
        [Range(1, 10, ErrorMessage = "Броят на стаите е задължителен")]
        public int RoomsCount { get; set; }
        public bool Confirmed { get; set; }
        [Range(1, 10, ErrorMessage = "Стаята е задължителна")]
        public int RoomId { get; set; }

        public Room Room { get; set; }

        public IEnumerable<KeyValuePair<string, string>> RoomsOptions { get; set; }

        public List<RoomModel> roomsModel;
    }
}
