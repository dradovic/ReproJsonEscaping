namespace ReproJsonEscaping
{
    public class Questionnaire
    {
        public int Id { get; set; }
        public List<QuestionnaireAnswer> Answers { get; set; } = null!;
    }

    public class QuestionnaireAnswer
    {
        public string Content { get; set; } = null!;
    }
}
