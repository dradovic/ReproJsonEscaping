using Microsoft.EntityFrameworkCore;
using ReproJsonEscaping;

using (var db = new MyDbContext())
{
    db.Database.EnsureCreated();
    db.Questionnaires.ExecuteDelete();

    db.Questionnaires.AddRange([
        new Questionnaire { Answers = [ new() { Content = @"New York", Comment = null }] },
        new Questionnaire { Answers = [ new() { Content = @"Zürich", Comment = @"Natürlich\nZürich." }] },
    ]);
    db.SaveChanges();
}

Print();
// prints as expected:
// 1: New York ()
// 2: Zürich (Natürlich\nZürich.)

using (var db = new MyDbContext())
{
    db.Questionnaires.First().Answers.Single().Comment = @"Natürlich\nZürich.";
    db.SaveChanges();
}

Print();
// prints NOT as expected:
// 1: New York (Nat\u00FCrlich\\nZ\u00FCrich.)
// 2: Zürich (Natürlich\nZürich.)

void Print()
{
    using (var db = new MyDbContext())
    {
        int i = 0;
        foreach (var questionnaire in db.Questionnaires)
        {
            var answer = questionnaire.Answers.Single();
            Console.WriteLine($"{++i}: {answer.Content} ({answer.Comment})");
        }
    }
}

Console.ReadLine();