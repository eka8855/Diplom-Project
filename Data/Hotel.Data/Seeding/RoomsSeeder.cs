using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Common;
using Hotel.Data.Models;
using Hotel.Services;
using Hotel.Web.ViewModels.Administration.Rooms;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Data.Seeding
{
    internal class RoomsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Rooms.Any())
            {
                return;
            }
            await dbContext.Rooms.AddAsync(new Room()
            {
                Name = "Единично легло",
                Price = 350M,
                NumberAvailable = 3,
                Type = "Superior",
                ImageName = "room1.jpg",
            });
            await dbContext.Rooms.AddAsync(new Room()
            {
                Name = "Двойно легло",
                Price = 440M,
                NumberAvailable = 6,
                Type = "Superior",
				ImageName = "room2.jpg",
			});
            await dbContext.Rooms.AddAsync(new Room()
            {
                Name = "Единично легло",
                Price = 300M,
                NumberAvailable = 2,
                Type = "Delux",
				ImageName = "room3.jpg",
			});
            await dbContext.Rooms.AddAsync(new Room()
            {
                Name = "Двойно легло",
                Price = 400M,
                NumberAvailable = 5,
                Type = "Delux",
				ImageName = "room1.jpg",
			});
            await dbContext.Rooms.AddAsync(new Room()
            {
                Name = "Единично легло",
                Price = 150M,
                NumberAvailable = 2,
                Type = "Budget",
				ImageName = "room5.jpg",
			});
            await dbContext.Rooms.AddAsync(new Room()
            {
                Name = "Двойно легло",
                Price = 250M,
                NumberAvailable = 4,
                Type = "Budget",
				ImageName = "room6.jpg",
			});
      
            await dbContext.SaveChangesAsync();
        }
      
    }
}
