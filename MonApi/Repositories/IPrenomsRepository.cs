using MonApi.Models;
using MonApi.Settings;


public interface IPrenomsRepository
{
    Task<IEnumerable<Prenoms>> GetAllAsync();
    Task<Prenoms> GetByIdAsync(string id);
    Task AddAsync(Prenoms prenom);
    Task UpdateAsync(Prenoms prenom);
    Task DeleteAsync(string id);
}
