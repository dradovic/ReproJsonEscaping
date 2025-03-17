namespace ReproJsonEscaping
{
    public class Questionnaire
    {
        public int Id { get; set; }
        public List<QuestionnaireAnswer> Answers { get; set; } = null!;
    }

    public class QuestionnaireAnswer
    {
        public Guid QuestionId { get; set; }
        public string? Comment { get; set; }
        public bool? BoolValue { get; set; }
        public int? IntValue { get; set; }
        public string? StringValue { get; set; }
    }
}
