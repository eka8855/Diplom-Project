using System.IO;
using System;
using System.Threading.Tasks;

using Hotel.Services;
using Hotel.Web.ViewModels.Administration.Rooms;
using Hotel.Web.ViewModels.Rooms;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Hotel.Data.Migrations;

namespace Hotel.Web.Areas.Administration.Controllers
{
    public class RoomsController : BaseController
    {
        private IRoomsService roomsService;
		public IWebHostEnvironment webHostEnviroment { get; }
		public RoomsController(IWebHostEnvironment webHostEnviroment, IRoomsService roomsService)
        {
            this.roomsService = roomsService;
			this.webHostEnviroment = webHostEnviroment;
		}
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            var rooms = roomsService.GetAll<RoomModel>();
            return View(rooms);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async  Task<IActionResult> Create(RoomInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
			input.ImageName = this.UploadFile(input.Image);
			await roomsService.CreateAsync(input);

            return this.RedirectToAction(nameof(this.Index));
        }


		[Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var room = roomsService.GetById<RoomInputModel>(id);

            return View(room);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, RoomInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
			if (input.Image != null)
			{
				string path = this.webHostEnviroment.WebRootPath + "/images/" + input.ImageName;
				System.IO.File.Delete(path);
				input.ImageName = this.UploadFile(input.Image);
			}
			await roomsService.UpdateAsync(id, input);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            string path = this.webHostEnviroment.WebRootPath + "/images/";
            await this.roomsService.DeleteAsync(id, path);
                
                return this.RedirectToAction(nameof(this.Index));
        }
        private string UploadFile(IFormFile image)
        {
            string fileName = null;
            if (image != null)
            {
                string uploadDir = Path.Combine(this.webHostEnviroment.WebRootPath, "images");
                fileName = Guid.NewGuid().ToString() + "-" + image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }

            return fileName;
        }
    }
}
