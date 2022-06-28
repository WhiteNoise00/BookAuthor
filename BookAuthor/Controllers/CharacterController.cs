using BookAuthor.Data;
using BookAuthor.Interfaces;
using BookAuthor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    public class CharacterController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IRepository _repository;
        public CharacterController(IRepository repository, IWebHostEnvironment environment)
        {
            _repository = repository;
            webHostEnvironment = environment;
        }

        public async Task<IActionResult> ShowAllCharacters()
        {
            var ch = await _repository.CharactersGetAllAsync();
            return View(ch);
        }

        [Route("Character/AddCharacter")]
        [ActionName("AddCharacter")]
        public IActionResult AddCharacter()
        {
            return View();
        }

        [Route("Character/AddCharacter")]
        [ActionName("AddCharacter")]
        [HttpPost]
        public async Task<IActionResult> AddCharacter(CharacterViewModel model)
        {
            if (ModelState.IsValid)
            {                
                Character ch = new Character
                {
                    Age = model.Age,
                    Name = model.Name,
                    FullName=model.FullName,
                    Description = model.Description,
                    Gender = model.Gender,
                    History = model.History
                };
                if (model.Image != null)
                {
                    string path = "/Photos/" + model.Image.FileName;
                    using (var fileStream = new FileStream(webHostEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(fileStream);
                    }
                    ch.PhotoPath = path;
                    ch.PhotoName = model.Image.FileName;
                }

                _repository.CharacterCreate(ch);
                return RedirectToAction("ShowAllCharacters");
            }
            else { return View(); }
        }

        [HttpGet]
        [Route("Character/EditCharacter")]
        [ActionName("EditCharacter")]
        public IActionResult EditCharacterGet(int id)
        {
            Character ch = _repository.CharacterGet(id);
            CharacterViewModel character = new CharacterViewModel
            {
                Name = ch.Name,
                FullName = ch.FullName,
                Age = ch.Age,
                Gender=ch.Gender,
                Description = ch.Description,
                ImagePath=ch.PhotoPath,
                History=ch.History,
                Id=ch.Id
            };
            return View(character);
        }

        [HttpPost]
        [Route("Character/EditCharacter")]
        [ActionName("EditCharacter")]
        public async Task<IActionResult> EditCharacter(CharacterViewModel model)
        {           
            Character character = _repository.CharacterGet(model.Id);
            character.Name = model.Name;
            character.FullName = model.FullName;
            character.Age= model.Age;
            character.Gender = model.Gender;
            character.Description = model.Description;
           
            if (model.Image != null)
            { 
                string path = "/Photos/" + model.Image.FileName;
                using (var fileStream = new FileStream(webHostEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await model.Image.CopyToAsync(fileStream);
                }
                character.PhotoPath = path;
                character.PhotoName = model.Image.FileName;
            }
            if (ModelState.IsValid)
            {
                _repository.CharacterEdit(character);
                return RedirectToAction("ShowAllCharacters");
            }
            else
            {
                return View(character);
            }
        }

        public IActionResult ShowCharacter(int id)
        {
            Character ch = _repository.CharacterGet(id);

                CharacterViewModel model = new CharacterViewModel
                {
                    Name = ch.Name,
                    FullName = ch.FullName,
                    ImagePath = ch.PhotoPath,
                    Gender = ch.Gender,
                    Age = ch.Age,
                    Description = ch.Description,
                    History = ch.History,
                    Id = ch.Id
                };
                return View(model);
  
        }


        [Route("Character/DeleteCharacter/{id?}")]
        [HttpGet]
        [ActionName("DeleteCharacter")]
        public IActionResult CharacterConfirmDelete(int? id)
        {
            if (id != null)
            {
                Character ch = _repository.CharacterGet(id.Value);
                return View(ch);
            }
            return NotFound();
        }

        [Route("Character/DeleteCharacter/{id?}")]
        [HttpPost]
        [ActionName("DeleteCharacter")]
        public IActionResult DeleteCharacter(int? id)
        {
            if (id != null)
            {
                
                Character ch = _repository.CharacterGet(id.Value);
                if (ch != null)
                {
                    _repository.CharacterDelete(ch);
                    
                }
                return RedirectToAction("ShowAllCharacters");
            }
            return NotFound();
        }

    }
}
