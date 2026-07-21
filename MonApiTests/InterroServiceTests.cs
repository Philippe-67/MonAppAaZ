using Xunit;
using Moq;
using MonApi.Repositories;
using MonApi.Services;
using MonApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonApiTests
{
    public class InterroServiceTests
    {
        private readonly Mock<IMotsRepository> _repoMock;
        private readonly MotsService _service;

        public InterroServiceTests()
        {
            _repoMock = new Mock<IMotsRepository>();
            _service  = new MotsService(_repoMock.Object);
        }

        [Fact]
        public async Task GetInterroItemsAsync_MapsMotFrToWordToTranslate()
        {
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Mot>
            {
                new Mot { Id = "1", MotFr = "Maison", MotEn = "House" }
            });

            var items = await _service.GetInterroItemsAsync(1);

            Assert.Equal("Maison", items[0].WordToTranslate);
        }

        [Fact]
        public async Task GetInterroItemsAsync_MapsMotEnToCorrectAnswer()
        {
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Mot>
            {
                new Mot { Id = "1", MotFr = "Maison", MotEn = "House" }
            });

            var items = await _service.GetInterroItemsAsync(1);

            Assert.Equal("House", items[0].CorrectAnswer);
        }

        [Fact]
        public async Task GetInterroItemsAsync_ReturnsExactCount_WhenEnoughMotsAvailable()
        {
            var mots = Enumerable.Range(1, 10)
                .Select(i => new Mot { Id = i.ToString(), MotFr = $"fr{i}", MotEn = $"en{i}" })
                .ToList();
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(mots);

            var items = await _service.GetInterroItemsAsync(5);

            Assert.Equal(5, items.Count);
        }

        [Fact]
        public async Task GetInterroItemsAsync_ReturnsAllMots_WhenCountExceedsAvailable()
        {
            var mots = new List<Mot>
            {
                new Mot { Id = "1", MotFr = "Un",    MotEn = "One"   },
                new Mot { Id = "2", MotFr = "Deux",  MotEn = "Two"   },
                new Mot { Id = "3", MotFr = "Trois", MotEn = "Three" }
            };
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(mots);

            var items = await _service.GetInterroItemsAsync(10);

            Assert.Equal(3, items.Count);
        }

        [Fact]
        public async Task GetInterroItemsAsync_ReturnsEmpty_WhenNoMotsInRepository()
        {
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Mot>());

            var items = await _service.GetInterroItemsAsync(5);

            Assert.Empty(items);
        }

        [Fact]
        public async Task GetInterroItemsAsync_ReturnsEmpty_WhenCountIsZero()
        {
            var mots = new List<Mot>
            {
                new Mot { Id = "1", MotFr = "Un", MotEn = "One" }
            };
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(mots);

            var items = await _service.GetInterroItemsAsync(0);

            Assert.Empty(items);
        }

        [Fact]
        public async Task GetInterroItemsAsync_ReturnsDifferentOrderAcrossMultipleCalls()
        {
            var mots = Enumerable.Range(1, 20)
                .Select(i => new Mot { Id = i.ToString(), MotFr = $"fr{i}", MotEn = $"en{i}" })
                .ToList();
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(mots);

            var call1 = (await _service.GetInterroItemsAsync(10)).Select(x => x.WordToTranslate).ToList();
            var call2 = (await _service.GetInterroItemsAsync(10)).Select(x => x.WordToTranslate).ToList();

            bool sameOrder = call1.SequenceEqual(call2);
            Assert.False(sameOrder, "Deux appels successifs ne devraient pas retourner exactement le même ordre.");
        }

        [Fact]
        public async Task GetInterroItemsAsync_PropagatesRepositoryException()
        {
            _repoMock.Setup(r => r.GetAllAsync())
                     .ThrowsAsync(new System.Exception("DB error"));

            await Assert.ThrowsAsync<System.Exception>(() => _service.GetInterroItemsAsync(5));
        }

        [Fact]
        public async Task GetInterroItemsAsync_DoesNotReturnDuplicateWords()
        {
            var mots = Enumerable.Range(1, 10)
                .Select(i => new Mot { Id = i.ToString(), MotFr = $"fr{i}", MotEn = $"en{i}" })
                .ToList();
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(mots);

            var items = await _service.GetInterroItemsAsync(5);

            var distinctWords = items.Select(x => x.WordToTranslate).Distinct().ToList();
            Assert.Equal(items.Count, distinctWords.Count);
        }
    }
}
