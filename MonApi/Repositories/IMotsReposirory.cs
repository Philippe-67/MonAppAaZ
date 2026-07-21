using MonApi.Models;
using MonApi.Settings;

namespace MonApi.Repositories
{
public interface IMotsRepository
{
    Task<IEnumerable<Mot>> GetAllAsync();
    Task<Mot> GetByIdAsync(string id);
    Task AddAsync(Mot mot);
    Task UpdateAsync(Mot mot);
    Task DeleteAsync(string id);
}
}
