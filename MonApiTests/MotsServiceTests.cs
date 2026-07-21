using Xunit;
using Moq;
using MonApi.Repositories;
using MonApi.Services;
using MonApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonApiTests
{
    public class MotsServiceTests
    {
        private readonly Mock<IMotsRepository> _repoMock;
        private readonly MotsService _service;

        public MotsServiceTests()
        {
            _repoMock = new Mock<IMotsRepository>();
            _service = new MotsService(_repoMock.Object);
        }

        [Fact]
        public async Task GetAllMotsAsync_ReturnsAll()
        {
            var items = new List<Mot>
            {
                new Mot { Id = "1", MotFr = "un", MotEn = "one" },
                new Mot { Id = "2", MotFr = "deux", MotEn = "two" }
            };
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(items);

            var result = await _service.GetAllMotsAsync();

            Assert.Equal(2, ((IEnumerable<Mot>)result).AsList().Count);
        }

        [Fact]
        public async Task GetAllMotsAsync_ReturnsEmpty_WhenNoMots()
        {
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Mot>());

            var result = await _service.GetAllMotsAsync();

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetMotById_ReturnsNull_WhenNotFound()
        {
            _repoMock.Setup(r => r.GetByIdAsync("unknown")).ReturnsAsync((Mot)null);

            var result = await _service.GetMotByIdAsync("unknown");

            Assert.Null(result);
        }

        [Fact]
        public async Task ServiceMethods_PropagateRepositoryExceptions()
        {
            var ex = new System.Exception("DB error");
            _repoMock.Setup(r => r.GetAllAsync()).ThrowsAsync(ex);
            await Assert.ThrowsAsync<System.Exception>(() => _service.GetAllMotsAsync());

            _repoMock.Setup(r => r.GetByIdAsync("1")).ThrowsAsync(ex);
            await Assert.ThrowsAsync<System.Exception>(() => _service.GetMotByIdAsync("1"));

            var mot = new Mot { Id = "x", MotFr = "x", MotEn = "x" };
            _repoMock.Setup(r => r.AddAsync(mot)).ThrowsAsync(ex);
            await Assert.ThrowsAsync<System.Exception>(() => _service.AddMotAsync(mot));

            _repoMock.Setup(r => r.UpdateAsync(mot)).ThrowsAsync(ex);
            await Assert.ThrowsAsync<System.Exception>(() => _service.UpdateMotAsync(mot));

            _repoMock.Setup(r => r.DeleteAsync("x")).ThrowsAsync(ex);
            await Assert.ThrowsAsync<System.Exception>(() => _service.DeleteMotAsync("x"));
        }

        [Fact]
        public async Task AddMotAsync_CallsRepository()
        {
            var mot = new Mot { Id = "3", MotFr = "trois", MotEn = "three" };
            _repoMock.Setup(r => r.AddAsync(mot)).Returns(Task.CompletedTask).Verifiable();

            await _service.AddMotAsync(mot);

            _repoMock.Verify(r => r.AddAsync(mot), Times.Once);
        }

        [Fact]
        public async Task UpdateMotAsync_CallsRepository()
        {
            var mot = new Mot { Id = "4", MotFr = "quatre", MotEn = "four" };
            _repoMock.Setup(r => r.UpdateAsync(mot)).Returns(Task.CompletedTask).Verifiable();

            await _service.UpdateMotAsync(mot);

            _repoMock.Verify(r => r.UpdateAsync(mot), Times.Once);
        }

        [Fact]
        public async Task DeleteMotAsync_CallsRepository()
        {
            var id = "5";
            _repoMock.Setup(r => r.DeleteAsync(id)).Returns(Task.CompletedTask).Verifiable();

            await _service.DeleteMotAsync(id);

            _repoMock.Verify(r => r.DeleteAsync(id), Times.Once);
        }
    }

    static class EnumerableExtensions
    {
        public static System.Collections.Generic.List<T> AsList<T>(this System.Collections.Generic.IEnumerable<T> src)
        {
            return new System.Collections.Generic.List<T>(src);
        }
    }
}
