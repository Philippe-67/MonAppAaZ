using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MonApi.Models;
using MonApi.Settings;

namespace MonApi.Repositories
{
    public class MotsRepository : IMotsRepository
    {
        private readonly IMongoCollection<Mot> _collection;

        public MotsRepository(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = database.GetCollection<Mot>("Mots");
        }

        public async Task<IEnumerable<Mot>> GetAllAsync() => await _collection.Find(_ => true).ToListAsync();

        public async Task<Mot> GetByIdAsync(string id) => await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();

        public async Task AddAsync(Mot mot) => await _collection.InsertOneAsync(mot);

        public async Task UpdateAsync(Mot mot) => await _collection.ReplaceOneAsync(p => p.Id == mot.Id, mot);

        public async Task DeleteAsync(string id) => await _collection.DeleteOneAsync(p => p.Id == id);
    }
}
    