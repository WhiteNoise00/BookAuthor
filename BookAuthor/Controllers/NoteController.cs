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
    public class NoteController : Controller
    {
        private readonly IRepository _repository;
        public NoteController(IRepository repository)
        {
            _repository = repository;

        }
        public async Task<IActionResult> ShowAllNotes()
        {
            var notes = await _repository.NotesGetAllAsync();
            if (notes != null)
            {
                return View(notes);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("Note/AddNote")]
        [ActionName("AddNote")]
        public IActionResult AddNote()
        {
            return View();
        }

        [Route("Note/AddNote")]
        [ActionName("AddNote")]
        [HttpPost]
        public IActionResult AddNote(Note note)
        {
            if (ModelState.IsValid)
            {
                _repository.NoteCreate(note);
                return RedirectToAction("ShowAllNotes");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        [Route("Note/EditNote")]
        [ActionName("EditNote")]
        public IActionResult NoteEditGet(int id)
        {
            Note note = _repository.NoteGet(id);
            if (note != null)
            {
                return View(note);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("Note/EditNote")]
        [ActionName("EditNote")]
        public IActionResult EditNote(Note note)
        {
            if (ModelState.IsValid)
            {
                _repository.NoteEdit(note);
                return RedirectToAction("ReadNote", new { id=note.Id});
            }
            else
            {
                return View(note);
            }
        }

        [Route("Note/DeleteNote/{id?}")]
        [HttpGet]
        [ActionName("DeleteNote")]
        public IActionResult NoteConfirmDelete(int? id)
        {
            if (id != null)
            {
                Note note = _repository.NoteGet(id.Value);
                if (note != null)
                    return View(note);
            }
            return NotFound();
        }

        [Route("Note/DeleteNote/{id?}")]
        [HttpPost]
        [ActionName("DeleteNote")]
        public IActionResult DeleteNote(int? id)
        {
            if (id != null)
            {

                Note note = _repository.NoteGet(id.Value);
                if (note != null)
                {
                    _repository.NoteDelete(note);
                    
                }
                return RedirectToAction("ShowAllNotes");
            }
            return NotFound();
        }

        public IActionResult ReadNote(int id)
        {
            
            Note note = _repository.NoteGet(id);

            if (note != null)
            {
                return View(note);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
