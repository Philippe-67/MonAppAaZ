namespace MonApi.Models;

    public class QuizQuestionDto
    {
       public required string WordToTranslate { get; init; }
       public required List<string> Options { get; init; }
       public required string CorrectAnswer { get; init; }
    }
