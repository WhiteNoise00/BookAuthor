using BookAuthor.Controllers;
using BookAuthor.Interfaces;
using BookAuthor.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTestApp.Tests
{
    public class CharacterControllerTest
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        private List<Character> GetTestCharacters()
        {
            var characters = new List<Character>
            {
                new Character { Id=1, Name="Anonymous", FullName="Unknown", Description="Anonymous", Age="Unknown", Gender="Unknown", History="Unknown", PhotoPath="3469183.collage_00239.jpg"},
                new Character { Id=2, Name="First Person", FullName="Unknown", Description="First Person", Age="Unknown", Gender="Unknown", History="Unknown", PhotoPath="Alexandra Zharkova.jpg"}
            };
            return characters;
        }

        private List<Character> GetTestChangedCharacters()
        {
            var characters = new List<Character>
            {
                new Character { Id=1, Name="Zero Person", FullName="Unknown", Description="Anonymous", Age="Unknown", Gender="Unknown", History="Unknown", PhotoPath="3469183.collage_00239.jpg"},
                new Character { Id=2, Name="First Person", FullName="Unknown", Description="First Person", Age="Unknown", Gender="Unknown", History="Unknown", PhotoPath="Alexandra Zharkova.jpg"}
            };
            return characters;
        }

        [Fact]
        public async Task CreateCharacter()
        {
            // Arrange
            int testCharacterId = 1;
            var mock = new Mock<IRepository>();

            mock.Setup(repo => repo.CharacterGet(testCharacterId))
                .Returns(GetTestCharacters().FirstOrDefault(p => p.Id == testCharacterId));
            CharacterViewModel ch = new CharacterViewModel
            { 
                Id = 1, 
                Name = "Anonymous", 
                FullName = "Unknown", 
                Description = "anonymous", 
                Age = "Unknown", 
                Gender = "Unknown",
                History = "Unknown",
                ImagePath = "3469183.collage_00239.jpg"
            };
            var controller = new CharacterController(mock.Object, webHostEnvironment);

            // Act
            var result =  await controller.AddCharacter(ch);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("ShowAllCharacters", redirectToActionResult.ActionName);
            Assert.Equal(1, mock.Invocations.Count);
        }


        [Fact]
        public async Task GetAllCharacters()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.CharactersGetAllAsync()).ReturnsAsync(GetTestCharacters());
            var controller = new CharacterController(mock.Object, webHostEnvironment);

            // Act
            var result = await controller.ShowAllCharacters();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Character>>(viewResult.Model);
            Assert.Equal(GetTestCharacters().Count, model.Count());
        }

        [Fact]
        public void GetCharacter()
        {
            // Arrange
            int testCharacterId = 1;
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.CharacterGet(testCharacterId))
                .Returns(GetTestCharacters().FirstOrDefault(p => p.Id == testCharacterId));
            var controller = new CharacterController(mock.Object, webHostEnvironment);

            // Act
            var result = controller.EditCharacterGet(testCharacterId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<CharacterViewModel>(viewResult.ViewData.Model);
            Assert.Equal(testCharacterId, model.Id);
            Assert.Equal("Anonymous", model.Name);
            Assert.Equal("Unknown", model.FullName);
            Assert.Equal("Anonymous", model.Description);            
            Assert.Equal("Unknown", model.Age);
            Assert.Equal("Unknown", model.Gender);
            Assert.Equal("Unknown", model.History);
            Assert.Equal("3469183.collage_00239.jpg", model.ImagePath);
        }

        [Fact]
        public void GetCharacterDetails()
        {
            // Arrange
            int testCharacterId = 1;
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.CharacterGet(testCharacterId))
                .Returns(GetTestCharacters().FirstOrDefault(p => p.Id == testCharacterId));
            var controller = new CharacterController(mock.Object, webHostEnvironment);

            // Act
            var result = controller.ShowCharacter(testCharacterId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<CharacterViewModel>(viewResult.ViewData.Model);
            Assert.Equal(testCharacterId, model.Id);
            Assert.Equal("Anonymous", model.Name);
            Assert.Equal("Unknown", model.FullName);
            Assert.Equal("Unknown", model.Age);
            Assert.Equal("Unknown", model.Gender);
            Assert.Equal("Unknown", model.History);
            Assert.Equal("Anonymous", model.Description);
            Assert.Equal("3469183.collage_00239.jpg", model.ImagePath);
        }

        [Fact]
        public async Task EditCharacter()
        {
            // Arrange
            int testCharacterId = 1;
            var mock = new Mock<IRepository>();

            CharacterViewModel character_view = new CharacterViewModel
            {
                Id = testCharacterId,
                Name = "Zero Person",
                FullName = "Unknown",
                Description = "Anonymous",
                Age = "Unknown",
                Gender = "Unknown",
                History = "Unknown",
                ImagePath = "3469183.collage_00239.jpg"
            };
            mock.Setup(repo => repo.CharacterGet(testCharacterId))
                .Returns(GetTestChangedCharacters().FirstOrDefault(i => i.Id == testCharacterId));
            var controller = new CharacterController(mock.Object, webHostEnvironment);

            // Act
            var result = await controller.EditCharacter(character_view);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ShowAllCharacters", redirectToActionResult.ActionName);
        }

        [Fact]
        public void DeleteCharacter()
        {
            // Arrange
            int testCharacterId = 1;
            var mock = new Mock<IRepository>();
            Character chapter = GetTestCharacters().FirstOrDefault(i => i.Id == testCharacterId);
            var controller = new CharacterController(mock.Object, webHostEnvironment);

            // Act
            var result = controller.DeleteCharacter(testCharacterId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ShowAllCharacters", redirectToActionResult.ActionName);
        }
    }
}

