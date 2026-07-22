using MonApi.Models;

namespace MonApi.IServices
{
public interface IMotsService
{
    Task<IEnumerable<Mot>> GetAllMotsAsync();
    Task<Mot> GetMotByIdAsync(string id);
    Task AddMotAsync(Mot mot);
    Task UpdateMotAsync(Mot mot);
    Task DeleteMotAsync(string id);
    Task<List<InterroItemDto>> GetInterroItemsAsync(int count);
    Task<List<QuizQuestionDto>> GetQuizQuestionsAsync(int count);
}
}
