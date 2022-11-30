using Microsoft.EntityFrameworkCore;
namespace FinalProject.Models;

public class FinalProjectContext : DbContext
{
    public FinalProjectContext(DbContextOptions<FinalProjectContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasData(
            new Book {Id = 1, Title = "Harry Potter", Author="J.K. Rowling" , Description="Magic School", PublishYear = 1993}
        );

        modelBuilder.Entity<CollegeStudent>().HasData(
            new CollegeStudent {Id = 1, FullName = "Walter White", Birthdate = new DateTime(1958, 9, 7), CollegeProgram = "IT", YearInProgram = "Freshman"}
        );

        modelBuilder.Entity<OlympicAthlete>().HasData(
            new OlympicAthlete {Id = 1, AthleteName = "Usain Bolt", Country = "Jamaica", Sport = "Track & Field", MedalsWon = 8}
        );

        modelBuilder.Entity<ProgrammingLanguage>().HasData(
           new ProgrammingLanguage{Id = 1, Name="Java", Typed="Static", Execution="Compiled", Paradigm="Object Oriented"}
        );
    }

    public DbSet<Book>? Books { get; set;}
    public DbSet<CollegeStudent>? CollegeStudents { get; set;}
    public DbSet<OlympicAthlete>? OlympicAthletes { get; set; }
    public DbSet<ProgrammingLanguage>? ProgrammingLanguages { get; set;}
}