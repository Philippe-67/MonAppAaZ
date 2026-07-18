using MonApi.Models;
using MonApi.Settings;

namespace MonApi.Repositories
{
public interface IMotsRepository
{
    Task<IEnumerable<Mots>> GetAllAsync();
    Task<Mots> GetByIdAsync(string id);
    Task AddAsync(Mots mot);
    Task UpdateAsync(Mots mot);
    Task DeleteAsync(string id);
}
}
