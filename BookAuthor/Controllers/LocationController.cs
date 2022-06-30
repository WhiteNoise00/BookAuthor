using BookAuthor.Data;
using BookAuthor.Interfaces;
using BookAuthor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookAuthor.Controllers
{
    [Authorize]
    public class LocationController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IRepository _repository;
        public LocationController(IRepository repository, IWebHostEnvironment environment)
        {
            _repository = repository;
            webHostEnvironment = environment;
        }

        public async Task<IActionResult> ShowAllLocations()
        {
            var locations = await _repository.LocationsGetAllAsync();
            return View(locations);
        }


        [Route("Location/AddLocation")]
        [ActionName("AddLocation")]
        public IActionResult AddLocation()
        {
            return View();
        }


        [Route("Location/AddLocation")]
        [ActionName("AddLocation")]
        [HttpPost]
        public async Task<IActionResult> AddLocation(LocationViewModel model)
        {
            if (ModelState.IsValid)
            {
                Location location = new Location
                {
                Name = model.Name,
                Description = model.Description,
                };

                if (model.Image != null)
                {
                    string path = "/Photos/" + model.Image.FileName;
                    using (var fileStream = new FileStream(webHostEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await model.Image.CopyToAsync(fileStream);
                }
                location.PhotoPath = path;
                location.PhotoName = model.Image.FileName;
            }

                _repository.LocationCreate(location);
                return RedirectToAction("ShowAllLocations");
            }
            else { return View();
            }
        }


        [HttpGet]
        [Route("Location/EditLocation")]
        [ActionName("EditLocation")]
        public IActionResult LocationEditGet(int id)
        {
            var location = _repository.LocationGet(id);
            LocationViewModel scene = new LocationViewModel
            {
                Id = location.Id,
                Name = location.Name,
                Description = location.Description,
                ImagePath = location.PhotoPath
            };
            return View(scene);
        }


        [HttpGet]
        [Route("Location/EditLocation")]
        [ActionName("EditLocation")]
        public async Task<IActionResult> EditLocation(LocationViewModel model)
        {
            var location = _repository.LocationGet(model.Id);
            location.Name = model.Name;
            location.Description = model.Description;
          
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    string path = "/Photos/" + model.Image.FileName;
                    using (var fileStream = new FileStream(webHostEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(fileStream);
                    }
                    location.PhotoPath = path;
                    location.PhotoName = model.Image.FileName;
                }
                _repository.LocationEdit(location);
                return RedirectToAction("ShowAllLocations");
            }
            else
            {
                return View(location);
            }
        }

        public IActionResult ShowLocation(int id)
        {
            var location = _repository.LocationGet(id);
            LocationViewModel model = new LocationViewModel
            {
                Id = location.Id,
                Name = location.Name,
                Description = location.Description,
                ImagePath = location.PhotoPath
            };
            return View(model);
        }

        [Route("Location/DeleteLocation/{id?}")]
        [HttpGet]
        [ActionName("DeleteLocation")]
        public IActionResult LocationConfirmDelete(int? id)
        {
            if (id != null)
            {
                var location = _repository.LocationGet(id.Value);
                if (location != null)
                return View(location);
            }
            return NotFound();
        }

        [Route("Location/DeleteLocation/{id?}")]
        [HttpPost]
        [ActionName("DeleteLocation")]
        public IActionResult DeleteLocation(int? id)
        {
            if (id != null)
            {
                var location = _repository.LocationGet(id.Value);
                if (location != null)
                {
                    _repository.LocationDelete(location);
                   
                }
                return RedirectToAction("ShowAllLocations");
            }
            return NotFound();
        }
    }
}
