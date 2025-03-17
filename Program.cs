using Microsoft.EntityFrameworkCore;
using ReproJsonEscaping;

using (var db = new MyDbContext())
{
    db.Database.EnsureCreated();
    db.Questionnaires.ExecuteDelete();

    db.Questionnaires.AddRange([
        new Questionnaire { Answers = [ new() { Content = @"New York" }] },
        new Questionnaire { Answers = [ new() { Content = @"Zürich has one Umlaut" }] },
        new Questionnaire { Answers = [ new() { Content = @"Zürich\nCH has one escape char and an Umlaut" }] },
        new Questionnaire { Answers = [ new() { Content = @"New York\nUS has one escape char" }] },
    ]);
    db.SaveChanges();
}

using (var db = new MyDbContext())
{
    int i = 0;
    foreach (var questionnaire in db.Questionnaires)
    {
        Console.WriteLine($"{++i}: {questionnaire.Answers.Single().Content}");
    }
    // prints:
    // 1: New York
    // 2: Zürich has one Umlaut
    // 3: Zürich\nCH has one escape char and an Umlaut
    // 4: New York\nUS has one escape char
}

Console.ReadLine();