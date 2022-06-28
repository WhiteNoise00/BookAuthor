using BookAuthor.Data;
using BookAuthor.Interfaces;
using BookAuthor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAuthor.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly IRepository _repository;

        public EventController(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> ShowAllEvents()
        {
            var ch = await _repository.EventsGetAllAsync();
            if (ch != null)
            {
                return View(ch);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("Event/AddEvent")]
        [ActionName("AddEvent")]
        [HttpGet]
        public IActionResult CreateEventGet(int id)
        {
            var ch = _repository.EventGet(id);
            if (ch != null)
            { 
                return View(); 
            }
            else 
            { 
                return View(); 
            }
        }

        [Route("Event/AddEvent")]
        [ActionName("AddEvent")]
        [HttpPost]
        public IActionResult CreateEvent(Event ev)
        {
            if (ModelState.IsValid)
            {      
                _repository.EventCreate(ev);
                return RedirectToAction("ShowAllEvents");
            }
            else
            {
                return View();
            }
        }     

        [HttpGet]
        [Route("Event/EditEvent")]
        [ActionName("EditEvent")]
        public IActionResult EditEventGet(int id)
        {
            Event _event = _repository.EventGet(id);
            if (_event != null)
            { 
                return View(_event); 
            }
            else
            { 
                return NotFound(); 
            }
        }


        [HttpPost]
        [Route("Event/EditEvent")]
        [ActionName("EditEvent")]
        public IActionResult EditEvent(Event _event)
        {
            if (ModelState.IsValid)
            {
                _repository.EventEdit(_event);
                return RedirectToAction("ShowAllEvents");
            }
            else
            {
                return View(_event);
            }           
        }


        [Route("Event/DeleteEvent/{id?}")]
        [HttpGet]
        [ActionName("DeleteEvent")]
        public IActionResult EventConfirmDelete(int? id)
        {
            if (id != null)
            {
                Event ev = _repository.EventGet(id.Value);
                if (ev != null)
                return View(ev);
            }
            return NotFound();
        }


        [Route("Event/DeleteEvent/{id?}")]
        [HttpPost]
        [ActionName("DeleteEvent")]
        public IActionResult DeleteEvent(int? id)
        {
            if(id != null)
            {
                Event ev = _repository.EventGet(id.Value);
                if (ev != null)
                {
                    _repository.EventDelete(ev);
                    
                }
                return RedirectToAction("ShowAllEvents");
            }
            return NotFound();
        }
    }
}
