using MonApi.Models;

namespace MonApi.IServices
{
public interface IMotsService
{
    Task<IEnumerable<Mots>> GetAllMotsAsync();
    Task<Mots> GetMotByIdAsync(string id);
    Task AddMotAsync(Mots mot);
    Task UpdateMotAsync(Mots mot);
    Task DeleteMotAsync(string id);
}
}
