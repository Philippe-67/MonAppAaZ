using Xunit;
using MonApi.Repositories;
using MonApi.Models;
using Microsoft.Extensions.Options;
using MonApi.Settings;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Linq;

namespace MonApiTests
{
    public class MotsRepositoryIntegrationTests
    {
        private readonly MotsRepository _repository;
        private readonly IMongoClient _client;
        private readonly string _dbName;

        public MotsRepositoryIntegrationTests()
        {
            // Use a unique test database to avoid collisions
            _dbName = "MonAppDBTests_" + System.Guid.NewGuid().ToString("N");
            var settings = Options.Create(new MongoDBSettings
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = _dbName,
                CollectionName = "Mots"
            });

            _client = new MongoClient(settings.Value.ConnectionString);
            _repository = new MotsRepository(settings);
        }

        [Fact]
        public async Task AddGetUpdateDelete_Works()
        {
            var mot = new Mot { MotFr = "testfr", MotEn = "testen" };

            // Add
            await _repository.AddAsync(mot);
            Assert.False(string.IsNullOrEmpty(mot.Id));

            // GetAll contains
            var all = (await _repository.GetAllAsync()).ToList();
            Assert.Contains(all, m => m.Id == mot.Id);

            // GetById
            var byId = await _repository.GetByIdAsync(mot.Id);
            Assert.NotNull(byId);
            Assert.Equal("testfr", byId.MotFr);

            // Update
            mot.MotEn = "updated";
            await _repository.UpdateAsync(mot);
            var updated = await _repository.GetByIdAsync(mot.Id);
            Assert.Equal("updated", updated.MotEn);

            // Delete
            await _repository.DeleteAsync(mot.Id);
            var afterDelete = await _repository.GetByIdAsync(mot.Id);
            Assert.Null(afterDelete);

            // cleanup: drop test database
            _client.DropDatabase(_dbName);
        }
    }
}
