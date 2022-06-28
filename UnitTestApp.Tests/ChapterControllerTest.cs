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
    public class ChapterControllerTest
    {
        private List<Chapter> GetTestChapters()
        {
            var chapters = new List<Chapter>
            {
                new Chapter { Id=1, Author="Author", Name="First", Description="1", Text="Hello World!"},
                new Chapter { Id=2, Author="Author", Name="Second", Description="1", Text="Some text"},
                new Chapter { Id=3, Author="Author 2", Name="Thirth", Description="2", Text="My biography"},
                new Chapter { Id=4, Author="Author 2", Name="Fourth", Description="2", Text="New history"}
            };
            return chapters;
        }

        [Fact]
        public void CreateChapter()
        {
            // Arrange
            int testChapterId = 1;
            var mock = new Mock<IRepository>();          
            Chapter chapter = GetTestChapters().FirstOrDefault(i => i.Id == testChapterId);            

            var controller = new ChapterController(mock.Object);

            // Act
            var result = controller.AddChapter(chapter);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ShowAllChapters", redirectToActionResult.ActionName);
            mock.Verify(r => r.ChapterCreate(chapter));
        }


        [Fact]
        public async Task GetChapter()
        {
            // Arrange
            int testChapterId = 1;
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.ChapterGetAsync(testChapterId))
                .ReturnsAsync(GetTestChapters().FirstOrDefault(p => p.Id == testChapterId));
            var controller = new ChapterController(mock.Object);

            // Act
            var result = await controller.ChapterEditGet(testChapterId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Chapter>(viewResult.ViewData.Model);
            Assert.Equal("First", model.Name);
            Assert.Equal("Author", model.Author);
            Assert.Equal("1", model.Description);
            Assert.Equal(testChapterId, model.Id);
        }

        [Fact]
        public async Task GetAllChapters()
        {          
            // Arrange
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.ChaptersGetAllAsync()).ReturnsAsync(GetTestChapters());
            var controller = new ChapterController(mock.Object);

            // Act
            var result = await controller.ShowAllChapters();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Chapter>>(viewResult.Model);
            Assert.Equal(GetTestChapters().Count, model.Count());

        }

        [Fact]
        public async Task ReadChapter()
        {
            // Arrange
            int testChapterId = 1;
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.ChapterGetAsync(testChapterId))
                .ReturnsAsync(GetTestChapters().FirstOrDefault(p => p.Id == testChapterId));
            var controller = new ChapterController(mock.Object);

            // Act
            var result = await controller.ReadChapter(testChapterId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Chapter>(viewResult.ViewData.Model);
            Assert.Equal("First", model.Name);
            Assert.Equal("Author", model.Author);
            Assert.Equal("1", model.Description);
            Assert.Equal(testChapterId, model.Id);
        }


        [Fact]
        public void EditChapter()
        {
            // Arrange
            int testChapterId = 1;
            Chapter new_chapter = new Chapter { 
                Id=  testChapterId,
                Name ="Chapter 2",
                Description="New Chapter",
                Author="New Author",
                Text = "New Text"
            };
            var mock = new Mock<IRepository>();

            var controller = new ChapterController(mock.Object);

            // Act
            var result = controller.EditChapter(new_chapter);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ShowAllChapters", redirectToActionResult.ActionName);
            mock.Verify(r => r.ChapterEdit(new_chapter));

        }

        [Fact]
        public async Task EditChapterContent()
        {
            // Arrange
            int testChapterId = 1;
            Chapter new_chapter = new Chapter
            {
                Id = testChapterId,
                Name = "Chapter 2",
                Description = "New Chapter",
                Author = "Author",
                Text = "Hello World!"
            };
            var mock = new Mock<IRepository>();

            var controller = new ChapterController(mock.Object);

            // Act
            var result = await controller.EditChapterContent(new_chapter);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ReadChapter", redirectToActionResult.ActionName);
            mock.Verify(r => r.ChapterEditContentAsync(new_chapter));
        }

        [Fact]
        public async Task DeleteChapter()
        {
            // Arrange
            int testChapterId = 1;
            var mock = new Mock<IRepository>();
            Chapter chapter = GetTestChapters().FirstOrDefault(i=>i.Id==testChapterId);
            var controller = new ChapterController(mock.Object);

            // Act
            var result = await controller.DeleteChapter(testChapterId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ShowAllChapters", redirectToActionResult.ActionName);
        }
    }
}
