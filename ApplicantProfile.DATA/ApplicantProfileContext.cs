using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicantProfile.Model;


namespace ApplicantProfile.Data
{
    public class ApplicantProfileContext :DbContext
    {
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Institute> Institutes { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<StudyField> StudyFields { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }

        public ApplicantProfileContext(DbContextOptions options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //table
            modelBuilder.Entity<Applicant>().ToTable("Applicants");
            modelBuilder.Entity<Applicant>().Property(a => a.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Applicant>().Property(a => a.SecondName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Applicant>().Property(a => a.LastName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Applicant>().Property(a => a.BirthDate).IsRequired();
            modelBuilder.Entity<Applicant>().Property(a => a.AddedDate).IsRequired();
            modelBuilder.Entity<Applicant>().Property(a => a.ModifiedDate).IsRequired();

            modelBuilder.Entity<Gender>().ToTable("Genders");
            modelBuilder.Entity<Gender>().Property(g => g.Name).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Gender>().Property(g => g.AddedDate).IsRequired(); 
            modelBuilder.Entity<Gender>().Property(g => g.ModifiedDate).IsRequired();

            modelBuilder.Entity<Education>().ToTable("Educations");
            modelBuilder.Entity<Education>().Property(e => e.DateGraduated).IsRequired();
            modelBuilder.Entity<Education>().Property(e => e.AddedDate).IsRequired();
            modelBuilder.Entity<Education>().Property(e => e.ModifiedDate).IsRequired();

            modelBuilder.Entity<Experience>().ToTable("Experiences");
            modelBuilder.Entity<Experience>().Property(e => e.Position).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Experience>().Property(e => e.FromDate).IsRequired();
            modelBuilder.Entity<Experience>().Property(e => e.ToDate);
            modelBuilder.Entity<Experience>().Property(e => e.CurrentPos).IsRequired();
            modelBuilder.Entity<Experience>().Property(e => e.BankingExp).IsRequired();
            modelBuilder.Entity<Experience>().Property(e => e.Duties).IsRequired().HasMaxLength(250);
            modelBuilder.Entity<Experience>().Property(e => e.AddedDate).IsRequired();
            modelBuilder.Entity<Experience>().Property(e => e.ModifiedDate).IsRequired();
            modelBuilder.Entity<Experience>().Property(e => e.Company).IsRequired().HasMaxLength(200);

            modelBuilder.Entity<Institute>().ToTable("Institutes");
            modelBuilder.Entity<Institute>().Property(i => i.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Institute>().Property(g => g.AddedDate).IsRequired();
            modelBuilder.Entity<Institute>().Property(g => g.ModifiedDate).IsRequired();

            modelBuilder.Entity<JobTitle>().ToTable("JobTitles");
            modelBuilder.Entity<JobTitle>().Property(j => j.Title).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<JobTitle>().Property(j => j.ExpYears).IsRequired();
            modelBuilder.Entity<JobTitle>().Property(j => j.AddedDate).IsRequired();
            modelBuilder.Entity<JobTitle>().Property(j => j.ModifiedDate).IsRequired();

            modelBuilder.Entity<Location>().ToTable("Locations");
            modelBuilder.Entity<Location>().Property(l => l.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Location>().Property(g => g.AddedDate).IsRequired();
            modelBuilder.Entity<Location>().Property(g => g.ModifiedDate).IsRequired();

            modelBuilder.Entity<Qualification>().ToTable("Qualifications");
            modelBuilder.Entity<Qualification>().Property(q => q.Name).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Qualification>().Property(g => g.AddedDate).IsRequired();
            modelBuilder.Entity<Qualification>().Property(g => g.ModifiedDate).IsRequired();

            modelBuilder.Entity<StudyField>().ToTable("StudyFields");
            modelBuilder.Entity<StudyField>().Property(s => s.Field).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<StudyField>().Property(g => g.AddedDate).IsRequired();
            modelBuilder.Entity<StudyField>().Property(g => g.ModifiedDate).IsRequired();

            modelBuilder.Entity<Vacancy>().ToTable("Vacancies");
            modelBuilder.Entity<Vacancy>().Property(v => v.VDate).IsRequired();
            modelBuilder.Entity<Vacancy>().Property(v => v.Qty).IsRequired();
            modelBuilder.Entity<Vacancy>().Property(v => v.Active).IsRequired();
            modelBuilder.Entity<Vacancy>().Property(g => g.AddedDate).IsRequired();
            modelBuilder.Entity<Vacancy>().Property(g => g.ModifiedDate).IsRequired();

            //relationships
            modelBuilder.Entity<Applicant>()
                .HasOne(g => g.Gender)
                .WithMany(a => a.Applicants)
                .HasForeignKey(g => g.GenderId);
            modelBuilder.Entity<Applicant>()
                .HasOne(v => v.Vacancy)
                .WithMany(a => a.Applicants)
                .HasForeignKey(v => v.VacancyId);

            modelBuilder.Entity<Education>()
                .HasOne(a => a.Applicant)
                .WithMany(e => e.Educations)
                .HasForeignKey(a => a.ApplicantId);
            modelBuilder.Entity<Education>()
                .HasOne(i => i.Institute)
                .WithMany(e => e.Educations)
                .HasForeignKey(i => i.InstituteId);
            modelBuilder.Entity<Education>()
                .HasOne(s => s.StudyField)
                .WithMany(e => e.Educations)
                .HasForeignKey(s => s.StudyFieldId);
            modelBuilder.Entity<Education>()
                .HasOne(q => q.Qualification)
                .WithMany(e => e.Educations)
                .HasForeignKey(q => q.QualificationId);

            modelBuilder.Entity<Experience>()
                .HasOne(a => a.Applicant)
                .WithMany(e => e.Experiences)
                .HasForeignKey(a => a.ApplicantId);
            
            modelBuilder.Entity<JobTitle>()
                .HasOne(e => e.Qualification)
                .WithMany(j => j.JobTitles)
                .HasForeignKey(e => e.QualificationId);
            modelBuilder.Entity<JobTitle>()
               .HasOne(s => s.StudyField)
               .WithMany(j => j.JobTitles)
               .HasForeignKey(s => s.StudyFieldId);

            modelBuilder.Entity<Vacancy>()
                .HasOne(j => j.JobTitle)
                .WithMany(v => v.Vacancies)
                .HasForeignKey(j => j.JobTitleId);
            modelBuilder.Entity<Vacancy>()
                .HasOne(l => l.Location)
                .WithMany(v => v.Vacancies)
                .HasForeignKey(l => l.LocationId);
        }
    }
}
