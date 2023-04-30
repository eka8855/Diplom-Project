using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using AutoMapper.Execution;

using Hotel.Data.Common.Repositories;
using Hotel.Data.Models;
using Hotel.Services.Mapping;
using Hotel.Web.ViewModels.Administration.Rooms;

namespace Hotel.Services
{
    public class RoomsService : IRoomsService
    {
        private IDeletableEntityRepository<Room> roomsRepository;
        private IDeletableEntityRepository<Booking> bookingsRepository;

        public RoomsService(IDeletableEntityRepository<Room> roomsRepository, IDeletableEntityRepository<Booking> bookingsRepository)
        {
            this.roomsRepository = roomsRepository;
            this.bookingsRepository = bookingsRepository;
        }
		public T GetById<T>(int id)
		{
			var room = this.roomsRepository
				.AllAsNoTracking()
				.Where(r => r.Id == id)
				.To<T>()
				.FirstOrDefault();

			return room;
		}
		public async Task CreateAsync(RoomInputModel input)
        {
            var room = new Room
            {
                Name = input.Name,
                Type = input.Type,
				NumberAvailable = input.NumberAvailable,
                Price = input.Price,
                ImageName = input.ImageName,
            };

            await this.roomsRepository.AddAsync(room);
            await this.roomsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, string path)
        {
            var room = this.roomsRepository.All().FirstOrDefault(x => x.Id == id);
            path = path + room.ImageName;
            System.IO.File.Delete(path);
            if (this.bookingsRepository.All().Where(x => x.RoomId == room.Id).Any())
            {
                foreach (var booking in this.bookingsRepository.All().Where(x => x.RoomId == room.Id))
                {
                    this.bookingsRepository.HardDelete(booking);
                }
            }
            
            this.roomsRepository.HardDelete(room);
            await this.roomsRepository.SaveChangesAsync();
        }
        public IEnumerable<T> GetAll<T>()
        {
            var rooms = this.roomsRepository
                .AllAsNoTracking()
                .To<T>()
                .ToList();

            return rooms;
        }

        public int Count() => this.roomsRepository.AllAsNoTracking().Count();
        public async Task UpdateAsync(int id, RoomInputModel input)
        {
            var room = this.roomsRepository.All().FirstOrDefault(x => x.Id == id);
            room.Name = input.Name;
            room.Type = input.Type;
            room.NumberAvailable = input.NumberAvailable;
            room.Price = input.Price;

            if (!string.IsNullOrEmpty(input.ImageName))
            {
                room.ImageName = input.ImageName;
            }
            await this.roomsRepository.SaveChangesAsync();
        }
    }
}
