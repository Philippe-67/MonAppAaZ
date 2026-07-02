using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MonApi.Models;
using MonApi.Settings;

namespace MonApi.Services;

public class PrenomService
{
    private readonly IMongoCollection<Prenom> _collection;

    public PrenomService(IOptions<MongoDBSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _collection = database.GetCollection<Prenom>(settings.Value.CollectionName);
    }

    public async Task<List<Prenom>> GetAsync() =>
        await _collection.Find(_ => true).ToListAsync();

    public async Task<Prenom?> GetByIdAsync(string id) =>
        await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Prenom prenom) =>
        await _collection.InsertOneAsync(prenom);

    public async Task UpdateAsync(string id, Prenom prenom) =>
        await _collection.ReplaceOneAsync(p => p.Id == id, prenom);

    public async Task DeleteAsync(string id) =>
        await _collection.DeleteOneAsync(p => p.Id == id);
}
