using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MonApi.Models;
using MonApi.Settings;

namespace MonApi.Repositories
{
    public class MotsRepository : IMotsRepository
    {
        private readonly IMongoCollection<Mots> _collection;

        public MotsRepository(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = database.GetCollection<Mots>("Mots");
        }

        public async Task<IEnumerable<Mots>> GetAllAsync() => await _collection.Find(_ => true).ToListAsync();

        public async Task<Mots> GetByIdAsync(string id) => await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();

        public async Task AddAsync(Mots mot) => await _collection.InsertOneAsync(mot);

        public async Task UpdateAsync(Mots mot) => await _collection.ReplaceOneAsync(p => p.Id == mot.Id, mot);

        public async Task DeleteAsync(string id) => await _collection.DeleteOneAsync(p => p.Id == id);
    }
}
    