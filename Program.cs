using Microsoft.EntityFrameworkCore;
using ReproJsonEscaping;

using (var db = new MyDbContext())
{
    db.Database.EnsureCreated();
    db.Questionnaires.ExecuteDelete();

    db.Questionnaires.AddRange([
        new Questionnaire { Sections = [new() { Questions = [new() { TranslatedQuestions = [new("en", @"Change of address?")] }] }], Answers = [ new() { Content = @"New York", Comment = null }] },
        new Questionnaire { Sections = [new() { Questions = [new() { TranslatedQuestions = [new("de", @"Addressänderung?")] }] }], Answers = [ new() { Content = @"Zürich", Comment = @"Zürich has one Umlaut" }] },
        new Questionnaire { Sections = [new() { Questions = [new() { TranslatedQuestions = [new("de", @"Addressänderung?\nWeiteres?")] }] }], Answers = [ new() { Content = @"Zürich", Comment = @"Zürich\nCH has one escape char and an Umlaut" }] },
        new Questionnaire { Sections = [new() { Questions = [new() { TranslatedQuestions = [new("en", @"Where do you live?\nComments?")] }] }], Answers = [ new() { Content = @"New York", Comment = @"New York\nUS has one escape char" }] },
    ]);
    db.SaveChanges();
}

using (var db = new MyDbContext())
{
    int i = 0;
    foreach (var questionnaire in db.Questionnaires)
    {
        var question = questionnaire.Sections.Single().Questions.Single().TranslatedQuestions.Single();
        var answer = questionnaire.Answers.Single();
        Console.WriteLine($"{++i}: {question.Value}: {answer.Content} ({answer.Comment})");
    }
    // prints:
    // 1: Change of address?: New York ()
    // 2: Addressänderung ?: Zürich(Zürich has one Umlaut)
    // 3: Addressänderung?\nWeiteres?: Zürich (Zürich\nCH has one escape char and an Umlaut)
    // 4: Where do you live?\nComments ?: New York(New York\nUS has one escape char)
}

Console.ReadLine();