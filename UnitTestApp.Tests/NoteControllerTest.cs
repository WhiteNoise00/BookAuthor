using BookAuthor.Controllers;
using System;
using System.Collections.Generic;
using BookAuthor.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using BookAuthor.Models;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTestApp.Tests
{
    public class NoteControllerTest
    {
        private List<Note> GetTestNotes()
        {
            var notes = new List<Note>
            {
                new Note { Id=1, Name="First", Text="1"},
                new Note { Id=2, Name="Second", Text="2"},
                new Note { Id=3, Name="Third", Text="3"}
            };
            return notes;
        }

        [Fact]
        public void CreateNote()
        {
            // Arrange
            int testNoteId = 1;
            var mock = new Mock<IRepository>();          
            Note note = GetTestNotes().FirstOrDefault(i => i.Id == testNoteId);            

            var controller = new NoteController(mock.Object);

            // Act
            var result = controller.AddNote(note);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ShowAllNotes", redirectToActionResult.ActionName);
            mock.Verify(r => r.NoteCreate(note));
        }


        [Fact]
        public void GetNote()
        {
            // Arrange
            int testNoteId = 1;
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.NoteGet(testNoteId))
                .Returns(GetTestNotes().FirstOrDefault(p => p.Id == testNoteId));
            var controller = new NoteController(mock.Object);

            // Act
            var result = controller.NoteEditGet(testNoteId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Note>(viewResult.ViewData.Model);
            Assert.Equal("First", model.Name);
            Assert.Equal("1", model.Text);
            Assert.Equal(testNoteId, model.Id);
        }

        [Fact]
        public async Task GetAllNotes()
        {          
            // Arrange
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.NotesGetAllAsync()).ReturnsAsync(GetTestNotes());
            var controller = new NoteController(mock.Object);

            // Act
            var result = await controller.ShowAllNotes();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Note>>(viewResult.Model);
            Assert.Equal(GetTestNotes().Count, model.Count());

        }

        [Fact]
        public void EditNote()
        {
            // Arrange
            int testNoteId = 1;
            Note new_note = new Note { 
                Id= testNoteId,
                Name="New Note",
                Text="Some Idea"
            };
            var mock = new Mock<IRepository>();

            var controller = new NoteController(mock.Object);

            // Act
            var result = controller.EditNote(new_note);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ReadNote", redirectToActionResult.ActionName);
            mock.Verify(r => r.NoteEdit(new_note));

        }


        [Fact]
        public void DeleteNote()
        {
            // Arrange
            int testNoteId = 1;
            var mock = new Mock<IRepository>();
            var note = GetTestNotes().FirstOrDefault(i=>i.Id==testNoteId);
            var controller = new NoteController(mock.Object);

            // Act
            var result = controller.DeleteNote(testNoteId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ShowAllNotes", redirectToActionResult.ActionName);
        }
    }
}
