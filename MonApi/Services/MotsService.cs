
using MonApi.Models;
using MonApi.Repositories;
using MonApi.IServices;
using System.Collections.Generic;


namespace MonApi.Services
{

public class MotsService : IMotsService
{
    private readonly IMotsRepository _repository;

    public MotsService(IMotsRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Mots>> GetAllMotsAsync() => await _repository.GetAllAsync();

    public async Task<Mots> GetMotByIdAsync(string id) => await _repository.GetByIdAsync(id);

    public async Task AddMotAsync(Mots mot) => await _repository.AddAsync(mot);

    public async Task UpdateMotAsync(Mots mot) => await _repository.UpdateAsync(mot);

    public async Task DeleteMotAsync(string id) => await _repository.DeleteAsync(id);
}

}