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
    public class SceneController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IRepository _repository;
        public SceneController(IRepository repository, IWebHostEnvironment environment)
        {
            _repository = repository;
            webHostEnvironment = environment;
        }

        public async Task<IActionResult> ShowAllScenes()
        {
            var scenes = await _repository.ScenesGetAllAsync();
            return View(scenes);
        }


        [Route("Scene/AddScene")]
        [ActionName("AddScene")]
        public IActionResult AddScene()
        {
            return View();
        }


        [Route("Scene/AddScene")]
        [ActionName("AddScene")]
        [HttpPost]
        public async Task<IActionResult> AddScene(SceneViewModel model)
        {
            if (ModelState.IsValid)
            {
                Scene sc = new Scene
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
                sc.PhotoPath = path;
                sc.PhotoName = model.Image.FileName;
            }

                _repository.SceneCreate(sc);
                return RedirectToAction("ShowAllScenes");
            }
            else { return View();
            }
        }


        [HttpGet]
        [Route("Scene/EditScene")]
        [ActionName("EditScene")]
        public IActionResult SceneEditGet(int id)
        {
            var sc = _repository.SceneGet(id);
            SceneViewModel scene = new SceneViewModel
            {
                Id = sc.Id,
                Name = sc.Name,
                Description = sc.Description,
                ImagePath = sc.PhotoPath
            };
            return View(scene);
        }


        [HttpPost]
        [Route("Scene/EditScene")]
        [ActionName("EditScene")]
        public async Task<IActionResult> EditScene(SceneViewModel model)
        {
            var scene = _repository.SceneGet(model.Id);
            scene.Name = model.Name;
            scene.Description = model.Description;
          
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    string path = "/Photos/" + model.Image.FileName;
                    using (var fileStream = new FileStream(webHostEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(fileStream);
                    }
                    scene.PhotoPath = path;
                    scene.PhotoName = model.Image.FileName;
                }
                _repository.SceneEdit(scene);
                return RedirectToAction("ShowAllScenes");
            }
            else
            {
                return View(scene);
            }
        }

        public IActionResult ShowScene(int id)
        {
            var sc = _repository.SceneGet(id);
            SceneViewModel model = new SceneViewModel
            {
                Id = sc.Id,
                Name = sc.Name,
                Description = sc.Description,
                ImagePath = sc.PhotoPath
            };
            return View(model);
        }

        [Route("Scene/DeleteScene/{id?}")]
        [HttpGet]
        [ActionName("DeleteScene")]
        public IActionResult SceneConfirmDelete(int? id)
        {
            if (id != null)
            {
                var sc = _repository.SceneGet(id.Value);
                if (sc != null)
                return View(sc);
            }
            return NotFound();
        }

        [Route("Scene/DeleteScene/{id?}")]
        [HttpPost]
        [ActionName("DeleteScene")]
        public IActionResult DeleteScene(int? id)
        {
            if (id != null)
            {
                var sc = _repository.SceneGet(id.Value);
                if (sc != null)
                {
                    _repository.SceneDelete(sc);
                   
                }
                return RedirectToAction("ShowAllScenes");
            }
            return NotFound();
        }
    }
}
