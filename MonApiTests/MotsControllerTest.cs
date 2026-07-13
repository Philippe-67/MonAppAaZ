using Xunit;
using Moq;
using MonApi.IServices;
using MonApi.Models;
using MonApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MonApiTests
{
    public class MotsControllerTests
    {
        private readonly Mock<IMotsService> _motsServiceMock;
        private readonly MotsController _controller;

        public MotsControllerTests()
        {
            _motsServiceMock = new Mock<IMotsService>();
            _controller = new MotsController(_motsServiceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOk_WithListOfMots()
        {
            var motsFakes = new List<Mots>
            {
                new Mots { Id = "507f1f77bcf86cd799439011", MotFr = "Bonjour", MotEn = "Hello" },
                new Mots { Id = "507f1f77bcf86cd799439012", MotFr = "Merci", MotEn = "Thank you" }
            };
            _motsServiceMock.Setup(s => s.GetAllMotsAsync()).ReturnsAsync(motsFakes);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var mots = Assert.IsAssignableFrom<IEnumerable<Mots>>(okResult.Value).ToList();
            Assert.Equal(2, mots.Count);
        }

        [Fact]
        public async Task GetById_ReturnsOk_WhenMotExists()
        {
            var motFake = new Mots { Id = "507f1f77bcf86cd799439011", MotFr = "Bonjour", MotEn = "Hello" };
            _motsServiceMock.Setup(s => s.GetMotByIdAsync("507f1f77bcf86cd799439011")).ReturnsAsync(motFake);

            var result = await _controller.GetById("507f1f77bcf86cd799439011");

            var okResult = Assert.IsType<OkObjectResult>(result);
            var mot = Assert.IsType<Mots>(okResult.Value);
            Assert.Equal("Bonjour", mot.MotFr);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenMotDoesNotExist()
        {
            _motsServiceMock.Setup(s => s.GetMotByIdAsync("999")).ReturnsAsync((Mots)null);

            var result = await _controller.GetById("999");

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsCreated_WhenMotIsValid()
        {
            var motFake = new Mots { Id = "507f1f77bcf86cd799439011", MotFr = "Bonjour", MotEn = "Hello" };
            _motsServiceMock.Setup(s => s.AddMotAsync(It.IsAny<Mots>())).Returns(Task.CompletedTask);

            var result = await _controller.Create(motFake);

            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenMotExists()
        {
            var motFake = new Mots { Id = "507f1f77bcf86cd799439011", MotFr = "Bonjour", MotEn = "Hello" };
            _motsServiceMock.Setup(s => s.GetMotByIdAsync(motFake.Id)).ReturnsAsync(motFake);
            _motsServiceMock.Setup(s => s.UpdateMotAsync(It.IsAny<Mots>())).Returns(Task.CompletedTask);

            var result = await _controller.Update(motFake.Id, motFake);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenMotDoesNotExist()
        {
            _motsServiceMock.Setup(s => s.GetMotByIdAsync("999")).ReturnsAsync((Mots)null);

            var result = await _controller.Update("999", new Mots());

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenMotExists()
        {
            var motFake = new Mots { Id = "507f1f77bcf86cd799439011", MotFr = "Bonjour", MotEn = "Hello" };
            _motsServiceMock.Setup(s => s.GetMotByIdAsync(motFake.Id)).ReturnsAsync(motFake);
            _motsServiceMock.Setup(s => s.DeleteMotAsync(motFake.Id)).Returns(Task.CompletedTask);

            var result = await _controller.Delete(motFake.Id);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenMotDoesNotExist()
        {
            _motsServiceMock.Setup(s => s.GetMotByIdAsync("999")).ReturnsAsync((Mots)null);

            var result = await _controller.Delete("999");

            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}

