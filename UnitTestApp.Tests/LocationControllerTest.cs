using System;
using System.Collections.Generic;
using System.Text;
using BookAuthor.Controllers;
using BookAuthor.Interfaces;
using BookAuthor.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq;
using Xunit;
using System.Threading.Tasks;

namespace UnitTestApp.Tests
{
    public class LocationControllerTest
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        private List<Location> GetTestLocations()
        {
            var locations = new List<Location>
            {
                new Location { Id=1, Name="House", Description="Nice House", PhotoPath="3469183.collage_00239.jpg"},
                new Location { Id=2, Name="Park", Description="Nice Park",  PhotoPath=""},
                new Location { Id=3, Name="Flat", Description="Nice Flat",  PhotoPath=""}
            };
            return locations;
        }

        private List<Location> GetTestChangedLocations()
        {
            var locations = new List<Location>
            {
                new Location { Id=1, Name="Nice House", Description="Nice House", PhotoPath="3469183.collage_00239.jpg"},
                new Location { Id=2, Name="Park", Description="Nice Park",  PhotoPath="SomePicture.jpg"},
                new Location { Id=3, Name="Flat", Description="Cool Flat",  PhotoPath="BigImage.jpg"}
            };
            return locations;
        }

        [Fact]
        public async Task CreateLocation()
        {
            // Arrange
            int testLocationId = 1;
            var mock = new Mock<IRepository>();
            Location location = new Location
            {
                Id = testLocationId,
                Name = "House",
                Description = "Nice House",
                PhotoPath = "3469183.collage_00239.jpg"
            };
            LocationViewModel lc = new LocationViewModel
            {
                Id = testLocationId,
                Name = location.Name,
                Description = location.Description,
                ImagePath= "3469183.collage_00239.jpg"
            };
            var controller = new LocationController(mock.Object, webHostEnvironment);

            // Act
            var result = await controller.AddLocation(lc);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ShowAllLocations", redirectToActionResult.ActionName);
            Assert.Equal(1, mock.Invocations.Count);
        }

        [Fact]
        public async Task GetAllLocations()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.LocationsGetAllAsync()).ReturnsAsync(GetTestLocations());
            var controller = new LocationController(mock.Object, webHostEnvironment);

            // Act
            var result = await controller.ShowAllLocations();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Location>>(viewResult.Model);
            Assert.Equal(GetTestLocations().Count, model.Count());
        }

        [Fact]
        public void GetLocation()
        {
            // Arrange
            int testLocationId = 1;
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.LocationGet(testLocationId))
                .Returns(GetTestLocations().FirstOrDefault(p => p.Id == testLocationId));
            var controller = new LocationController(mock.Object, webHostEnvironment);

            // Act
            var result = controller.LocationEditGet(testLocationId);
            
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<LocationViewModel>(viewResult.ViewData.Model);
            Assert.Equal(testLocationId, model.Id);
            Assert.Equal("House", model.Name);
            Assert.Equal("Nice House", model.Description);
            Assert.Equal("3469183.collage_00239.jpg", model.ImagePath);
        }

        [Fact]
        public void GetLocationDetails()
        {
            // Arrange
            int testLocationId = 1;
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.LocationGet(testLocationId))
                .Returns(GetTestLocations().FirstOrDefault(p => p.Id == testLocationId));
            var controller = new LocationController(mock.Object, webHostEnvironment);

            // Act
            var result = controller.ShowLocation(testLocationId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<LocationViewModel>(viewResult.ViewData.Model);
            Assert.Equal(testLocationId, model.Id);
            Assert.Equal("House", model.Name);
            Assert.Equal("Nice House", model.Description);
            Assert.Equal("3469183.collage_00239.jpg", model.ImagePath);
        }

        [Fact]
        public async Task EditLocation()
        {
             int testLocationId = 1;

             // Arrange
             LocationViewModel location_view = new LocationViewModel
             {
                 Id = testLocationId,
                 Name = "New House",
                 Description = "Nice House",
                 ImagePath = "3469183.collage_00239.jpg"
             };

             var mock = new Mock<IRepository>();

             mock.Setup(repo => repo.LocationGet(testLocationId))
               .Returns(GetTestChangedLocations().FirstOrDefault(i => i.Id == testLocationId));

             var controller = new LocationController(mock.Object, webHostEnvironment);

             // Act
             var result = await controller.EditLocation(location_view);

             // Assert
             var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
             Assert.Equal("ShowAllLocations", redirectToActionResult.ActionName);           

        }


        [Fact]
        public void DeleteLocation()
        {
            // Arrange
            int testLocationId = 1;
            var mock = new Mock<IRepository>();
            Location location= GetTestLocations().FirstOrDefault(i => i.Id == testLocationId);
            var controller = new LocationController(mock.Object, webHostEnvironment);

            // Act
            var result = controller.DeleteLocation(testLocationId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ShowAllLocations", redirectToActionResult.ActionName);
        }
    }
}

