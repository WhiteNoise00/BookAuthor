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
    public class EventControllerTest
    {
        private List<Event> GetTestEvents()
        {
            var events = new List<Event>
            {
                new Event { Id=1, Name="First", Description="1"},
                new Event { Id=2, Name="Seceond", Description="2"},
                new Event { Id=3, Name="Third", Description="3"}
            };
            return events;
        }

        [Fact]
        public void CreateEvent()
        {
            // Arrange
            int testEventId = 1;
            var mock = new Mock<IRepository>();          
            Event _event = GetTestEvents().FirstOrDefault(i => i.Id == testEventId);            

            var controller = new EventController(mock.Object);

            // Act
            var result = controller.CreateEvent(_event);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ShowAllEvents", redirectToActionResult.ActionName);
            mock.Verify(r => r.EventCreate(_event));
        }


        [Fact]
        public void GetEvent()
        {
            // Arrange
            int testEventId = 1;
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.EventGet(testEventId))
                .Returns(GetTestEvents().FirstOrDefault(p => p.Id == testEventId));
            var controller = new EventController(mock.Object);

            // Act
            var result = controller.EditEventGet(testEventId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Event>(viewResult.ViewData.Model);
            Assert.Equal("First", model.Name);
            Assert.Equal("1", model.Description);
            Assert.Equal(testEventId, model.Id);
        }

        [Fact]
        public async Task GetAllEvents()
        {          
            // Arrange
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.EventsGetAllAsync()).ReturnsAsync(GetTestEvents());
            var controller = new EventController(mock.Object);

            // Act
            var result = await controller.ShowAllEvents();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Event>>(viewResult.Model);
            Assert.Equal(GetTestEvents().Count, model.Count());

        }

        [Fact]
        public void EditEvent()
        {
            // Arrange
            int testEventrId = 1;
            Event new_event = new Event { 
                Id= testEventrId,
                Name="Event 2",
                Description="New Event"
            };
            var mock = new Mock<IRepository>();
            var controller = new EventController(mock.Object);

            // Act
            var result = controller.EditEvent(new_event);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ShowAllEvents", redirectToActionResult.ActionName);
            mock.Verify(r => r.EventEdit(new_event));

        }


        [Fact]
        public void DeleteEvent()
        {
            // Arrange
            int testEventId = 1;
            var mock = new Mock<IRepository>();
            Event ev = GetTestEvents().FirstOrDefault(i => i.Id == testEventId);
            var controller = new EventController(mock.Object);

            // Act
            var result = controller.DeleteEvent(testEventId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ShowAllEvents", redirectToActionResult.ActionName);
        }
    }
}
