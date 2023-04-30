using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Web.ViewModels.Administration.Rooms;
using Hotel.Web.ViewModels.Home;

namespace Hotel.Services
{
    public interface IBookingsService
    {
        Task CreateAsync(HomeViewModel input);

        IEnumerable<T> GetAll<T>();

        Task DeleteAsync(int id);

		Task UpdateAsync(int id, HomeViewModel input);

		T GetById<T>(int id);

		int Count();
    }
}
