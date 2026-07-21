using Xunit;
using Moq;
using MonApi.IServices;
using MonApi.Models;
using MonApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonApiTests
{
    public class InterroControllerTests
    {
        private readonly Mock<IMotsService> _motsServiceMock;
        private readonly MotsController _controller;

        public InterroControllerTests()
        {
            _motsServiceMock = new Mock<IMotsService>();
            _controller = new MotsController(_motsServiceMock.Object);
        }

        [Fact]
        public async Task GetInterro_ReturnsOk_WithListOfItems()
        {
            var fakeItems = new List<InterroItemDto>
            {
                new InterroItemDto { WordToTranslate = "Bonjour", CorrectAnswer = "Hello" },
                new InterroItemDto { WordToTranslate = "Merci",   CorrectAnswer = "Thank you" }
            };
            _motsServiceMock.Setup(s => s.GetInterroItemsAsync(It.IsAny<int>()))
                            .ReturnsAsync(fakeItems);

            var result = await _controller.GetInterro(2);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var items = Assert.IsAssignableFrom<IEnumerable<InterroItemDto>>(okResult.Value).ToList();
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public async Task GetInterro_ReturnsOk_WithEmptyList_WhenNoMotsAvailable()
        {
            _motsServiceMock.Setup(s => s.GetInterroItemsAsync(It.IsAny<int>()))
                            .ReturnsAsync(new List<InterroItemDto>());

            var result = await _controller.GetInterro(5);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var items = Assert.IsAssignableFrom<IEnumerable<InterroItemDto>>(okResult.Value).ToList();
            Assert.Empty(items);
        }

        [Fact]
        public async Task GetInterro_PassesCountParameterToService()
        {
            _motsServiceMock.Setup(s => s.GetInterroItemsAsync(10))
                            .ReturnsAsync(new List<InterroItemDto>())
                            .Verifiable();

            await _controller.GetInterro(10);

            _motsServiceMock.Verify(s => s.GetInterroItemsAsync(10), Times.Once);
        }

        [Fact]
        public async Task GetInterro_ItemsHaveCorrectShape()
        {
            var fakeItems = new List<InterroItemDto>
            {
                new InterroItemDto { WordToTranslate = "Chat", CorrectAnswer = "Cat" }
            };
            _motsServiceMock.Setup(s => s.GetInterroItemsAsync(It.IsAny<int>()))
                            .ReturnsAsync(fakeItems);

            var result = await _controller.GetInterro(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var items = Assert.IsAssignableFrom<IEnumerable<InterroItemDto>>(okResult.Value).ToList();
            Assert.Equal("Chat", items[0].WordToTranslate);
            Assert.Equal("Cat",  items[0].CorrectAnswer);
        }

        [Fact]
        public async Task GetInterro_PropagatesServiceException()
        {
            _motsServiceMock.Setup(s => s.GetInterroItemsAsync(It.IsAny<int>()))
                            .ThrowsAsync(new System.Exception("DB error"));

            await Assert.ThrowsAsync<System.Exception>(() => _controller.GetInterro(5));
        }
    }
}
