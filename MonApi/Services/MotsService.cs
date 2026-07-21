// MonApi/Services/MotsService.cs

using MonApi.Models;
using MonApi.Repositories;
using MonApi.IServices;
using System.Collections.Generic;
using System.Linq; // <-- Cet using pourrait être nécessaire pour .OrderBy et .Select

namespace MonApi.Services
{

public class MotsService : IMotsService
{
    private readonly IMotsRepository _repository;

    public MotsService(IMotsRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Mot>> GetAllMotsAsync() => await _repository.GetAllAsync();

    public async Task<Mot> GetMotByIdAsync(string id) => await _repository.GetByIdAsync(id);

    public async Task AddMotAsync(Mot mot) => await _repository.AddAsync(mot);

    public async Task UpdateMotAsync(Mot mot) => await _repository.UpdateAsync(mot);

    public async Task DeleteMotAsync(string id) => await _repository.DeleteAsync(id);

    // NOUVELLE MÉTHODE AJOUTÉE ICI
    public async Task<List<InterroItemDto>> GetInterroItemsAsync(int count)
    {
        // 1. Récupérer tous les mots depuis le repository
        var allMots = await _repository.GetAllAsync();

        // 2. Les mélanger et en prendre le nombre demandé
        var randomMots = allMots
            .OrderBy(x => Guid.NewGuid()) // Astuce pour mélanger une liste
            .Take(count);

        // 3. Les transformer en DTO pour le frontend
        var interroItems = randomMots.Select(mot => new InterroItemDto
        {
            WordToTranslate = mot.MotFr,
            CorrectAnswer = mot.MotEn
        }).ToList();

        return interroItems;
    }
}

}
