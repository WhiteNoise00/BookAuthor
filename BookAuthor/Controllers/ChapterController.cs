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
    public class ChapterController : Controller
    {
        private readonly IRepository _repository;
        public ChapterController(IRepository repository)
        {
            _repository = repository;

        }
        public async Task<IActionResult> ShowAllChapters()
        {
            var ch = await _repository.ChaptersGetAllAsync();
            if (ch != null)
            {
                return View(ch);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("Chapter/AddChapter")]
        [ActionName("AddChapter")]
        public IActionResult AddChapter()
        {
            return View();
        }

        [Route("Chapter/AddChapter")]
        [ActionName("AddChapter")]
        [HttpPost]
        public IActionResult AddChapter(Chapter ch)
        {
            if (ModelState.IsValid)
            {
                _repository.ChapterCreate(ch);
                return RedirectToAction("ShowAllChapters");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        [Route("Chapter/EditChapter")]
        [ActionName("EditChapter")]
        public async Task<IActionResult> ChapterEditGet(int id)
        {
            Chapter ch = await _repository.ChapterGetAsync(id);
            if (ch != null)
            {
                return View(ch);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("Chapter/EditChapter")]
        [ActionName("EditChapter")]
        public IActionResult EditChapter(Chapter ch)
        {
            if (ModelState.IsValid)
            {
                _repository.ChapterEdit(ch);
                return RedirectToAction("ShowAllChapters");
            }
            else
            {
                return View(ch);
            }
        }

        [Route("Chapter/DeleteChapter/{id?}")]
        [HttpGet]
        [ActionName("DeleteChapter")]
        public async Task<IActionResult> ConfirmDeleteChapter(int? id)
        {
            if (id != null)
            {
                Chapter ch = await _repository.ChapterGetAsync(id.Value);
                if (ch != null)
                return View(ch);
            }
            return NotFound();
        }

        [Route("Chapter/DeleteChapter/{id?}")]
        [HttpPost]
        [ActionName("DeleteChapter")]
        public async Task<IActionResult> DeleteChapter(int? id)
        {
            if (id != null)
            {
                
                Chapter ch = await _repository.ChapterGetAsync(id.Value);
                if (ch != null)
                {
                    _repository.ChapterDelete(ch);
                   
                }
                return RedirectToAction("ShowAllChapters");
            }
            return NotFound();
        }


        public async Task<IActionResult> ReadChapter(int id)
        {
            var ch = await _repository.ChapterGetAsync(id);

            if (ch != null)
            {
                return View(ch);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet]
        [Route("Chapter/EditChapterContent")]
        [ActionName("EditChapterContent")]
        public async Task<IActionResult> EditChapterContent(int id)
        {
            var ch = await _repository.ChapterGetAsync(id);

            if (ch != null)
            {
                return View(ch);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        [Route("Chapter/EditChapterContent")]
        [ActionName("EditChapterContent")]
        public async Task<IActionResult> EditChapterContent(Chapter ch)
        {
            await _repository.ChapterEditContentAsync(ch);
            return RedirectToAction("ReadChapter", new { id = ch.Id });
        }
    }
}
