using Microsoft.EntityFrameworkCore;
using ReproJsonEscaping;

using (var db = new MyDbContext())
{
    db.Database.EnsureCreated();
    db.Questionnaires.ExecuteDelete();

    db.Questionnaires.AddRange([
        new Questionnaire { Sections = [new() { Questions = [new("en", @"Change of address?")] }], Answers = [ new() { Content = @"New York" }] },
        new Questionnaire { Sections = [new() { Questions = [new("de", @"Addressänderung?")] }], Answers = [ new() { Content = @"Zürich has one Umlaut" }] },
        new Questionnaire { Sections = [new() { Questions = [new("de", @"Addressänderung?\nWeiteres?")] }], Answers = [ new() { Content = @"Zürich\nCH has one escape char and an Umlaut" }] },
        new Questionnaire { Sections = [new() { Questions = [new("en", @"Where do you live?\nComments?")] }], Answers = [ new() { Content = @"New York\nUS has one escape char" }] },
    ]);
    db.SaveChanges();
}

using (var db = new MyDbContext())
{
    int i = 0;
    foreach (var questionnaire in db.Questionnaires)
    {
        Console.WriteLine($"{++i}: {questionnaire.Sections.Single().Questions.Single().Value}: {questionnaire.Answers.Single().Content}");
    }
    // prints:
    // 1: Change of address?: New York
    // 2: Addressänderung ?: Zürich has one Umlaut
    // 3: Addressänderung ?\nWeiteres ?: Zürich\nCH has one escape char and an Umlaut
    // 4: Where do you live?\nComments ?: New York\nUS has one escape char
}

Console.ReadLine();