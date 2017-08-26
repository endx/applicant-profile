using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ApplicantProfile.Data;

namespace ApplicantProfile.API.Migrations
{
    [DbContext(typeof(ApplicantProfileContext))]
    partial class ApplicantProfileContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApplicantProfile.Model.Applicant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int>("GenderId");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int>("VacancyId");

                    b.HasKey("Id");

                    b.HasIndex("GenderId");

                    b.HasIndex("VacancyId");

                    b.ToTable("Applicants");
                });

            modelBuilder.Entity("ApplicantProfile.Model.Education", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<int>("ApplicantId");

                    b.Property<DateTime>("DateGraduated");

                    b.Property<int>("InstituteId");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<int>("QualificationId");

                    b.Property<int>("StudyFieldId");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.HasIndex("InstituteId");

                    b.HasIndex("QualificationId");

                    b.HasIndex("StudyFieldId");

                    b.ToTable("Educations");
                });

            modelBuilder.Entity("ApplicantProfile.Model.Experience", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<int>("ApplicantId");

                    b.Property<bool>("BankingExp");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 200);

                    b.Property<bool>("CurrentPos");

                    b.Property<string>("Duties")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 250);

                    b.Property<DateTime>("FromDate");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime?>("ToDate");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.ToTable("Experiences");
                });

            modelBuilder.Entity("ApplicantProfile.Model.Gender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 10);

                    b.HasKey("Id");

                    b.ToTable("Genders");
                });

            modelBuilder.Entity("ApplicantProfile.Model.Institute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("Institutes");
                });

            modelBuilder.Entity("ApplicantProfile.Model.JobTitle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<int>("ExpYears");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<int>("QualificationId");

                    b.Property<int>("StudyFieldId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.HasIndex("QualificationId");

                    b.HasIndex("StudyFieldId");

                    b.ToTable("JobTitles");
                });

            modelBuilder.Entity("ApplicantProfile.Model.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("ApplicantProfile.Model.Qualification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.HasKey("Id");

                    b.ToTable("Qualifications");
                });

            modelBuilder.Entity("ApplicantProfile.Model.StudyField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<string>("Field")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime>("ModifiedDate");

                    b.HasKey("Id");

                    b.ToTable("StudyFields");
                });

            modelBuilder.Entity("ApplicantProfile.Model.Vacancy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<DateTime>("AddedDate");

                    b.Property<int>("JobTitleId");

                    b.Property<int>("LocationId");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<int>("Qty");

                    b.Property<DateTime>("VDate");

                    b.HasKey("Id");

                    b.HasIndex("JobTitleId");

                    b.HasIndex("LocationId");

                    b.ToTable("Vacancies");
                });

            modelBuilder.Entity("ApplicantProfile.Model.Applicant", b =>
                {
                    b.HasOne("ApplicantProfile.Model.Gender", "Gender")
                        .WithMany("Applicants")
                        .HasForeignKey("GenderId");

                    b.HasOne("ApplicantProfile.Model.Vacancy", "Vacancy")
                        .WithMany("Applicants")
                        .HasForeignKey("VacancyId");
                });

            modelBuilder.Entity("ApplicantProfile.Model.Education", b =>
                {
                    b.HasOne("ApplicantProfile.Model.Applicant", "Applicant")
                        .WithMany("Educations")
                        .HasForeignKey("ApplicantId");

                    b.HasOne("ApplicantProfile.Model.Institute", "Institute")
                        .WithMany("Educations")
                        .HasForeignKey("InstituteId");

                    b.HasOne("ApplicantProfile.Model.Qualification", "Qualification")
                        .WithMany("Educations")
                        .HasForeignKey("QualificationId");

                    b.HasOne("ApplicantProfile.Model.StudyField", "StudyField")
                        .WithMany("Educations")
                        .HasForeignKey("StudyFieldId");
                });

            modelBuilder.Entity("ApplicantProfile.Model.Experience", b =>
                {
                    b.HasOne("ApplicantProfile.Model.Applicant", "Applicant")
                        .WithMany("Experiences")
                        .HasForeignKey("ApplicantId");
                });

            modelBuilder.Entity("ApplicantProfile.Model.JobTitle", b =>
                {
                    b.HasOne("ApplicantProfile.Model.Qualification", "Qualification")
                        .WithMany("JobTitles")
                        .HasForeignKey("QualificationId");

                    b.HasOne("ApplicantProfile.Model.StudyField", "StudyField")
                        .WithMany("JobTitles")
                        .HasForeignKey("StudyFieldId");
                });

            modelBuilder.Entity("ApplicantProfile.Model.Vacancy", b =>
                {
                    b.HasOne("ApplicantProfile.Model.JobTitle", "JobTitle")
                        .WithMany("Vacancies")
                        .HasForeignKey("JobTitleId");

                    b.HasOne("ApplicantProfile.Model.Location", "Location")
                        .WithMany("Vacancies")
                        .HasForeignKey("LocationId");
                });
        }
    }
}
