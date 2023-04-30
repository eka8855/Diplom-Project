using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Data.Common.Repositories;
using Hotel.Data.Models;
using Hotel.Services.Mapping;
using Hotel.Web.ViewModels.Administration.Rooms;
using Hotel.Web.ViewModels.Home;

namespace Hotel.Services
{
    public class BookingsService : IBookingsService
    {
        private IDeletableEntityRepository<Booking> bookingsRepository;

        public BookingsService(IDeletableEntityRepository<Booking> bookingsRepository)
        {
            this.bookingsRepository = bookingsRepository;
        }
		public T GetById<T>(int id)
		{
			var booking = this.bookingsRepository
				.AllAsNoTracking()
				.Where(r => r.Id == id)
				.To<T>()
				.FirstOrDefault();

			return booking;
		}

		public async Task CreateAsync(HomeViewModel input)
        {
            var booking = new Booking
            {
                CustomerName = input.CustomerName,
                CustomerEmail = input.CustomerEmail,
                CustomerPhone = input.CustomerPhone,
                CheckIn = input.CheckIn,
                CheckOut = input.CheckOut,
                RoomId = input.RoomId,
                RoomsCount = input.RoomsCount,
                Confirmed = false,
            };

            await this.bookingsRepository.AddAsync(booking);
            await this.bookingsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var booking = this.bookingsRepository.All().FirstOrDefault(x => x.Id == id);
            this.bookingsRepository.HardDelete(booking);
            await this.bookingsRepository.SaveChangesAsync();
        }
        public IEnumerable<T> GetAll<T>()
        {
            var rooms = this.bookingsRepository
                .AllAsNoTracking()
                .To<T>()
                .ToList();

            return rooms;
        }

		public async Task UpdateAsync(int id, HomeViewModel input)
		{
			var booking = this.bookingsRepository.All().FirstOrDefault(x => x.Id == id);
            booking.CustomerName = input.CustomerName;
            booking.CustomerEmail = input.CustomerEmail;
			booking.CustomerPhone = input.CustomerPhone;
			booking.CheckIn = input.CheckIn;
			booking.CheckOut = input.CheckOut;
			booking.RoomsCount = input.RoomsCount;
			booking.Confirmed = input.Confirmed;
			await this.bookingsRepository.SaveChangesAsync();
		}

		public int Count() => this.bookingsRepository.AllAsNoTracking().Count();
    }
}