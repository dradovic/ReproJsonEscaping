namespace ReproJsonEscaping
{
    public class Questionnaire
    {
        public int Id { get; set; }
        public List<TranslatedString> Questions { get; set; } = null!;
        public List<QuestionnaireAnswer> Answers { get; set; } = null!;
    }

    public record TranslatedString(string Language, string Value);

    public class QuestionnaireAnswer
    {
        public string Content { get; set; } = null!;
    }
}
