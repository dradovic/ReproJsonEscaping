namespace ReproJsonEscaping
{
    public class Questionnaire
    {
        public int Id { get; set; }
        public List<QuestionnaireSection> Sections { get; set; } = new();
        public List<QuestionnaireAnswer> Answers { get; set; } = null!;
    }

    public class QuestionnaireSection
    {
        public List<QuestionnaireQuestion> Questions { get; set; } = new();
    }

    public class QuestionnaireQuestion
    {
        public List<TranslatedString> TranslatedQuestions { get; set; } = new();
    }

    public record TranslatedString(string Language, string Value);

    public class QuestionnaireAnswer
    {
        public Guid QuestionId { get; set; }
        public string? Comment { get; set; }
        public bool? BoolValue { get; set; }
        public int? IntValue { get; set; }
        public string? StringValue { get; set; }
    }
}
