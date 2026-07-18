// using MongoDB.Driver;
// using Microsoft.Extensions.Options;
// using MonApi.Models;
// using MonApi.Settings;
// using MonApi.Repositories;
// using MonApi.IServices; 
// using MonApi.Services;

// public class PrenomsRepository : IPrenomsRepository
// {
//     private readonly IMongoCollection<Prenoms> _collection;

//     public PrenomsRepository(IOptions<MongoDBSettings> settings)
//     {
//         var client = new MongoClient(settings.Value.ConnectionString);
//         var database = client.GetDatabase(settings.Value.DatabaseName);
//         _collection = database.GetCollection<Prenoms>("Prenoms");
//     }

//     public async Task<IEnumerable<Prenoms>> GetAllAsync() => await _collection.Find(_ => true).ToListAsync();

//     public async Task<Prenoms> GetByIdAsync(string id) => await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();

//     public async Task AddAsync(Prenoms prenom) => await _collection.InsertOneAsync(prenom);

//     public async Task UpdateAsync(Prenoms prenom) => await _collection.ReplaceOneAsync(p => p.Id == prenom.Id, prenom);

//     public async Task DeleteAsync(string id) => await _collection.DeleteOneAsync(p => p.Id == id);
// }
