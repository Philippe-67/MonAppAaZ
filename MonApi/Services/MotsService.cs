// MonApi/Services/MotsService.cs

using MonApi.Models;
using MonApi.Repositories;
using MonApi.IServices;
using System.Collections.Generic;
using System.Linq;

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

        // --- MÉTHODE POUR L'INTERRO ---
        public async Task<List<InterroItemDto>> GetInterroItemsAsync(int count)
        {
            // 1. Récupérer tous les mots depuis le repository
            var allMots = await _repository.GetAllAsync();

            // 2. Les mélanger et en prendre le nombre demandé
            var randomMots = allMots
                .OrderBy(x => Guid.NewGuid())
                .Take(count);

            // 3. Les transformer en DTO pour le frontend
            var interroItems = randomMots.Select(mot => new InterroItemDto
            {
                WordToTranslate = mot.MotFr,
                CorrectAnswer = mot.MotEn
            }).ToList();

            return interroItems;
        }

        // --- MÉTHODE POUR LE QUIZ ---
        public async Task<List<QuizQuestionDto>> GetQuizQuestionsAsync(int count)
        {
            // 1. Récupérer tous les mots depuis le repository
            var allMots = await _repository.GetAllAsync();

            // 2. Les mélanger et en prendre le nombre demandé
            var randomMots = allMots
                .OrderBy(x => Guid.NewGuid())
                .Take(count);

            // 3. Les transformer en DTO pour le frontend
            var quizQuestions = randomMots.Select(mot => new QuizQuestionDto
            {
                WordToTranslate = mot.MotFr,
                CorrectAnswer = mot.MotEn,
                Options = GenerateQuizOptions(mot, allMots)
            }).ToList();

            return quizQuestions;
        }

        // --- MÉTHODE PRIVÉE POUR GÉNÉRER LES OPTIONS ---
        private List<string> GenerateQuizOptions(Mot mot, IEnumerable<Mot> allMots)
        {
            var random = new Random();
            var motsList = allMots.ToList();

            // Récupérer 3 distracteurs (mauvaises réponses)
            var distractors = motsList
                .Where(m => m.Id != mot.Id)
                .OrderBy(x => random.Next())
                .Take(3)
                .Select(m => m.MotEn)
                .ToList();

            // Créer la liste des options (bonne réponse + distracteurs)
            var options = new List<string> { mot.MotEn };
            options.AddRange(distractors);

            // Mélanger les options
            options = options.OrderBy(x => random.Next()).ToList();

            return options;
        }
    }
}
