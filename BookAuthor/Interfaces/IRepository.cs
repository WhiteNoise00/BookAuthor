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
        //Chapter ChapterGet(int id);
        //IEnumerable<Chapter> ChaptersGetAll();
        //void ChapterEditContent(Chapter chapter);



        /*Working with object "Character"*/
        void CharacterCreate(Character character);
        Character CharacterGet(int id);
        Task<List<Character>> CharactersGetAllAsync();
        void CharacterEdit(Character character);
        void CharacterDelete(Character character);


        /*Working with object "Scene"*/
        void SceneCreate(Scene scene);
        Scene SceneGet(int id);
        Task<List<Scene>> ScenesGetAllAsync();
        void SceneEdit(Scene scene);
        void SceneDelete(Scene scene);


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

