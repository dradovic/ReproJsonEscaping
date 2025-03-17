﻿using Microsoft.EntityFrameworkCore;

namespace ReproJsonEscaping
{
    internal class MyDbContext : DbContext
    {
        public DbSet<Questionnaire> Questionnaires { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Questionnaire>()
                .OwnsMany(x => x.Sections, x =>
                {
                    x.OwnsMany(s => s.Questions, s => s.OwnsMany(q => q.TranslatedQuestions));
                    x.ToJson();
                });

            builder.Entity<Questionnaire>()
                .OwnsMany(x => x.Answers, x => x.ToJson());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Data Source=.;Initial Catalog=ReproJsonEscaping;Integrated Security=True;Encrypt=False");
    }
}
