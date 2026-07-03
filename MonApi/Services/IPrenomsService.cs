using MonApi.Models;

public interface IPrenomsService
{
    Task<IEnumerable<Prenoms>> GetAllPrenomsAsync();
    Task<Prenoms> GetPrenomByIdAsync(string id);
    Task AddPrenomAsync(Prenoms prenom);
    Task UpdatePrenomAsync(Prenoms prenom);
    Task DeletePrenomAsync(string id);
}
