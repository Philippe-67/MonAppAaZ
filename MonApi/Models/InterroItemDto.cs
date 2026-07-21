namespace MonApi.Models
{
    public class InterroItemDto
    {
        public required string WordToTranslate { get; init; }
        public required string CorrectAnswer { get; init; }
    }
}