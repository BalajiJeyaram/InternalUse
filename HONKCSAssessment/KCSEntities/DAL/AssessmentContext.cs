namespace KCSEntities.DAL
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public partial class AssessmentContext : DbContext
    {
        public AssessmentContext()
            : base("name=AssessmentContext")
        {
            //Database.SetInitializer<AssessmentContext>(new CreateDatabaseIfNotExists<AssessmentContext>());
        }

        public virtual DbSet<Assessment> Assessments { get; set; }
        public virtual DbSet<assessmentanswer> assessmentanswers { get; set; }
        public virtual DbSet<ExamM> ExamMs { get; set; }
        public virtual DbSet<ExamQ> ExamQs { get; set; }
        public virtual DbSet<userprofile> userprofiles { get; set; }
        public virtual DbSet<studentprofile> studentprofile { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<assessmentanswer>()
            //    .Property(e => e.answertext)
            //    .IsUnicode(false);

            //modelBuilder.Entity<ExamM>()
            //    .Property(e => e.ExamTitle)
            //    .IsUnicode(false);

            //modelBuilder.Entity<ExamQ>()
            //    .Property(e => e.item)
            //    .IsUnicode(false);

            //modelBuilder.Entity<userprofile>()
            //    .Property(e => e.UserName)
            //    .IsUnicode(false);

            //modelBuilder.Entity<userprofile>()
            //    .Property(e => e.ActiveUser)
            //    .IsFixedLength()
            //    .IsUnicode(false);

            //modelBuilder.Entity<testtable>()
            //    .Property(e => e.ActiveUser)
            //    .IsFixedLength()
            //    .IsUnicode(false);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
