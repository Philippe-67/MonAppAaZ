
// using MonApi.Models;
// using MonApi.Repositories;
// using MonApi.IServices;
// using System.Collections.Generic;

// namespace MonApi.Services
// {

// public class PrenomsService : IPrenomsService
// {
//     private readonly IPrenomsRepository _repository;

//     public PrenomsService(IPrenomsRepository repository)
//     {
//         _repository = repository;
//     }

//     public async Task<IEnumerable<Prenoms>> GetAllPrenomsAsync() => await _repository.GetAllAsync();

//     public async Task<Prenoms> GetPrenomByIdAsync(string id) => await _repository.GetByIdAsync(id);

//     public async Task AddPrenomAsync(Prenoms prenom) => await _repository.AddAsync(prenom);

//     public async Task UpdatePrenomAsync(Prenoms prenom) => await _repository.UpdateAsync(prenom);

//     public async Task DeletePrenomAsync(string id) => await _repository.DeleteAsync(id);
// }

// }