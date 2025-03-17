using Microsoft.EntityFrameworkCore;
using ReproJsonEscaping;

using (var db = new MyDbContext())
{
    db.Database.EnsureCreated();
    db.Questionnaires.ExecuteDelete();

    db.Questionnaires.AddRange([
        new Questionnaire { Answers = [ new() { StringValue = @"New York", Comment = null }] },
        new Questionnaire { Answers = [ new() { StringValue = @"Zürich", Comment = @"Zürich has one Umlaut" }] },
        new Questionnaire { Answers = [ new() { StringValue = @"Zürich", Comment = @"Zürich\nCH has one escape char and an Umlaut" }] },
        new Questionnaire { Answers = [ new() { StringValue = @"New York", Comment = @"New York\nUS has one escape char" }] },
    ]);
    db.SaveChanges();
}

using (var db = new MyDbContext())
{
    int i = 0;
    foreach (var questionnaire in db.Questionnaires)
    {
        var answer = questionnaire.Answers.Single();
        Console.WriteLine($"{++i}: {answer.StringValue} ({answer.Comment})");
    }
    // prints:
    // 1: Change of address?: New York ()
    // 2: Addressänderung ?: Zürich(Zürich has one Umlaut)
    // 3: Addressänderung?\nWeiteres?: Zürich (Zürich\nCH has one escape char and an Umlaut)
    // 4: Where do you live?\nComments ?: New York(New York\nUS has one escape char)
}

using (var db = new MyDbContext())
{
    db.Questionnaires.First().Answers.Single().Comment = @"Natürlich.\nZürich.";
    db.SaveChanges();
}

using (var db = new MyDbContext())
{
    int i = 0;
    foreach (var questionnaire in db.Questionnaires)
    {
        var answer = questionnaire.Answers.Single();
        Console.WriteLine($"{++i}: {answer.StringValue} ({answer.Comment})");
    }
    // prints:
    // 1: New York (Nat\u00FCrlich.\\nZ\u00FCrich.)
    // 2: Zürich(Zürich has one Umlaut)
    // 3: Zürich(Zürich\nCH has one escape char and an Umlaut)
    // 4: New York(New York\nUS has one escape char)
}

Console.ReadLine();