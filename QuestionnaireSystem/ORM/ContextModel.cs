using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QuestionnaireSystem.ORM
{
    public partial class ContextModel : DbContext
    {
        public ContextModel()
            : base("name=ContextModel")
        {
        }

        public virtual DbSet<BasicAnswer> BasicAnswers { get; set; }
        public virtual DbSet<CommonQuest> CommonQuests { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Questionnaire> Questionnaires { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BasicAnswer>()
                .Property(e => e.Nickname)
                .IsUnicode(false);

            modelBuilder.Entity<BasicAnswer>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<BasicAnswer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<BasicAnswer>()
                .HasMany(e => e.Answers)
                .WithRequired(e => e.BasicAnswer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Questionnaire>()
                .HasMany(e => e.BasicAnswers)
                .WithRequired(e => e.Questionnaire)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Questionnaire>()
                .HasMany(e => e.Questions)
                .WithRequired(e => e.Questionnaire)
                .WillCascadeOnDelete(false);
        }
    }
}
