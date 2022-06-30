using BookAuthor.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAuthor.Interfaces
{
    public interface IRepository
    {
        /*Working with object "Chapter"*/
        void ChapterCreate(Chapter chapter);
        Task<Chapter> ChapterGetAsync(int id);
        Task<List<Chapter>> ChaptersGetAllAsync();
        Task ChapterEditContentAsync(Chapter chapter);
        void ChapterEdit(Chapter chapter);
        void ChapterDelete(Chapter chapter);


        /*Working with object "Character"*/
        void CharacterCreate(Character character);
        Character CharacterGet(int id);
        Task<List<Character>> CharactersGetAllAsync();
        void CharacterEdit(Character character);
        void CharacterDelete(Character character);


        /*Working with object "Location"*/
        void LocationCreate(Location location);
        Location LocationGet(int id);
        Task<List<Location>> LocationsGetAllAsync();
        void LocationEdit(Location location);
        void LocationDelete(Location location);


        /*Working with object "Event"*/
        void EventCreate(Event _event);
        Event EventGet(int id);
        Task<List<Event>> EventsGetAllAsync();
        void EventEdit(Event _event);
        void EventDelete(Event _event);


        /*Working with object "Note"*/
        void NoteCreate(Note note);
        Note NoteGet(int id);
        Task<List<Note>> NotesGetAllAsync();
        void NoteEdit(Note _event);
        void NoteDelete(Note _event);

    }
}

