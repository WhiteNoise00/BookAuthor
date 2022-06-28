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
    public class SceneControllerTest
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        private List<Scene> GetTestScenes()
        {
            var chapters = new List<Scene>
            {
                new Scene { Id=1, Name="House", Description="Nice House", PhotoPath="3469183.collage_00239.jpg"},
                new Scene { Id=2, Name="Park", Description="Nice Park",  PhotoPath=""},
                new Scene { Id=3, Name="Flat", Description="Nice Flat",  PhotoPath=""}
            };
            return chapters;
        }

        private List<Scene> GetTestChangedScenes()
        {
            var chapters = new List<Scene>
            {
                new Scene { Id=1, Name="Nice House", Description="Nice House", PhotoPath="3469183.collage_00239.jpg"},
                new Scene { Id=2, Name="Park", Description="Nice Park",  PhotoPath="SomePicture.jpg"},
                new Scene { Id=3, Name="Flat", Description="Cool Flat",  PhotoPath="BigImage.jpg"}
            };
            return chapters;
        }

        [Fact]
        public async Task CreateScene()
        {
            // Arrange
            int testSceneId = 1;
            var mock = new Mock<IRepository>();
            Scene scene = new Scene
            {
                Id = testSceneId,
                Name = "House",
                Description = "Nice House",
                PhotoPath = "3469183.collage_00239.jpg"
            };
            SceneViewModel sc = new SceneViewModel
            {
                Id = testSceneId,
                Name = scene.Name,
                Description = scene.Description,
                ImagePath= "3469183.collage_00239.jpg"
            };
            var controller = new SceneController(mock.Object, webHostEnvironment);

            // Act
            var result = await controller.AddScene(sc);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ShowAllScenes", redirectToActionResult.ActionName);
            Assert.Equal(1, mock.Invocations.Count);
        }

        [Fact]
        public async Task GetAllSceness()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.ScenesGetAllAsync()).ReturnsAsync(GetTestScenes());
            var controller = new SceneController(mock.Object, webHostEnvironment);

            // Act
            var result = await controller.ShowAllScenes();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Scene>>(viewResult.Model);
            Assert.Equal(GetTestScenes().Count, model.Count());
        }

        [Fact]
        public void GetScene()
        {
            // Arrange
            int testSceneId = 1;
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.SceneGet(testSceneId))
                .Returns(GetTestScenes().FirstOrDefault(p => p.Id == testSceneId));
            var controller = new SceneController(mock.Object, webHostEnvironment);

            // Act
            var result = controller.SceneEditGet(testSceneId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<SceneViewModel>(viewResult.ViewData.Model);
            Assert.Equal(testSceneId, model.Id);
            Assert.Equal("House", model.Name);
            Assert.Equal("Nice House", model.Description);
            Assert.Equal("3469183.collage_00239.jpg", model.ImagePath);
        }

        [Fact]
        public void GetSceneDetails()
        {
            // Arrange
            int testSceneId = 1;
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.SceneGet(testSceneId))
                .Returns(GetTestScenes().FirstOrDefault(p => p.Id == testSceneId));
            var controller = new SceneController(mock.Object, webHostEnvironment);

            // Act
            var result = controller.ShowScene(testSceneId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<SceneViewModel>(viewResult.ViewData.Model);
            Assert.Equal(testSceneId, model.Id);
            Assert.Equal("House", model.Name);
            Assert.Equal("Nice House", model.Description);
            Assert.Equal("3469183.collage_00239.jpg", model.ImagePath);
        }

        [Fact]
        public async Task EditScene()
        {
             int testSceneId = 1;

             // Arrange
             SceneViewModel scene_view = new SceneViewModel
             {
                 Id = testSceneId,
                 Name = "New House",
                 Description = "Nice House",
                 ImagePath = "3469183.collage_00239.jpg"
             };

             var mock = new Mock<IRepository>();

             mock.Setup(repo => repo.SceneGet(testSceneId))
               .Returns(GetTestChangedScenes().FirstOrDefault(i => i.Id == testSceneId));

             var controller = new SceneController(mock.Object, webHostEnvironment);

             // Act
             var result = await controller.EditScene(scene_view);

             // Assert
             var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
             Assert.Equal("ShowAllScenes", redirectToActionResult.ActionName);           

        }


        [Fact]
        public void DeleteScene()
        {
            // Arrange
            int testSceneId = 1;
            var mock = new Mock<IRepository>();
            Scene scene= GetTestScenes().FirstOrDefault(i => i.Id == testSceneId);
            var controller = new SceneController(mock.Object, webHostEnvironment);

            // Act
            var result = controller.DeleteScene(testSceneId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ShowAllScenes", redirectToActionResult.ActionName);
        }
    }
}

