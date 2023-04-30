using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Web.ViewModels.Administration.Rooms;

namespace Hotel.Services
{
    public interface IRoomsService
    {
        Task CreateAsync(RoomInputModel input);

        Task UpdateAsync(int id, RoomInputModel input);

        IEnumerable<T> GetAll<T>();

		T GetById<T>(int id);

		Task DeleteAsync(int id, string path);

        int Count();
    }
}
