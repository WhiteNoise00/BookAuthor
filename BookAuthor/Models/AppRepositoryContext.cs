using BookAuthor.Data;
using BookAuthor.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAuthor.Models
{
    public class AppRepositoryContext: IRepository
    {
        private readonly ApplicationDbContext db;

        public AppRepositoryContext(ApplicationDbContext context)
        {
            db = context;
        }

        /*Working with object "Chapter"*/
        public void ChapterCreate(Chapter chapter)
        {
            db.Chapters.Add(chapter);
            db.SaveChanges();
        }

        public async Task<Chapter> ChapterGetAsync(int id)
        {
            return await db.Chapters.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Chapter>> ChaptersGetAllAsync()
        {
            return await db.Chapters.OrderBy(c => c.Title).ToListAsync();
        }

        public void ChapterEdit(Chapter chapter)
        {
            Chapter ch= db.Chapters.FirstOrDefault(e => e.Id == chapter.Id);
            ch.Title = chapter.Title;
            ch.Content = chapter.Content;
            ch.Author = chapter.Author;
            ch.Description = chapter.Description;
            db.Chapters.Update(ch);
            db.SaveChanges();       
        }

        public async Task ChapterEditContentAsync(Chapter chapter)
        {
            Chapter ch = await db.Chapters.FirstOrDefaultAsync(e => e.Id == chapter.Id);
            ch.Content = chapter.Content;
            db.Chapters.Update(ch);
            await db.SaveChangesAsync();
        }

        public void ChapterDelete(Chapter chapter) 
        {
            db.Chapters.Remove(chapter);
            db.SaveChanges();
        }


        /*Working with object "Character"*/

        public void CharacterCreate(Character character) 
        {
            db.Characters.Add(character);
            db.SaveChanges();
        }

        public async Task<List<Character>> CharactersGetAllAsync()
        {
            return await db.Characters.OrderBy(c => c.Name).ToListAsync();
        }

        public Character CharacterGet(int id)
        {
            return db.Characters.FirstOrDefault(i=>i.Id==id);
        }

        public void CharacterEdit(Character character) 
        {
            db.Characters.Update(character);
            db.SaveChanges();
        }

        public void CharacterDelete(Character character)
        {
            db.Characters.Remove(character);
            db.SaveChanges();
        }


        /*Working with object "Location"*/
        public async Task<List<Location>> LocationsGetAllAsync()
        {
            return await db.Locations.OrderBy(c => c.Name).ToListAsync();
        }

        public Location LocationGet(int id)
        {
            return db.Locations.FirstOrDefault(i=>i.Id==id);
        }

        public void LocationEdit(Location location)
        {
            db.Locations.Update(location);
            db.SaveChanges();
        }

        public void LocationCreate(Location location)
        {
            db.Locations.Add(location);
            db.SaveChanges();
        }
        public void LocationDelete(Location location)
        {
            db.Locations.Remove(location);
            db.SaveChanges();
        }


        /*Working with object "Event"*/
        public void EventCreate(Event _event)
        {
            db.Events.Add(_event);
            db.SaveChanges();
        }
        public async Task<List<Event>> EventsGetAllAsync() 
        {
            return await db.Events.OrderBy(c => c.Name).ToListAsync();
        }
        public Event EventGet(int id)
        {
            return db.Events.FirstOrDefault(i => i.Id == id);
        }
        public void EventEdit(Event _event) 
        {
            db.Events.Update(_event);
            db.SaveChanges();
        }
        public void EventDelete(Event _event) 
        {
            db.Events.Remove(_event);
            db.SaveChanges();
        }


        /*Working with object "Note"*/
        public void NoteCreate(Note note)
        {
            db.Notes.Add(note);
            db.SaveChanges();
        }

        public async Task<List<Note>> NotesGetAllAsync()
        {
            return await db.Notes.OrderBy(c => c.Name).ToListAsync();
        }

        public Note NoteGet(int id)
        {
            return db.Notes.FirstOrDefault(i => i.Id == id);
        }

        public void NoteEdit(Note note)
        {
            db.Notes.Update(note);
            db.SaveChanges();
        }

        public void NoteDelete(Note note)
        {
            db.Notes.Remove(note);
            db.SaveChanges();
        }

    }
}
