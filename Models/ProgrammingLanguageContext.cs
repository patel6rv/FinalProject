using Microsoft.EntityFrameworkCore;
namespace FinalProject.Models;

public class ProgrammingLanguageContext : DbContext
{
    public ProgrammingLanguageContext(DbContextOptions<ProgrammingLanguage> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProgrammingLanguage>().HasData(
           new ProgrammingLanguage{ID = "1", Name="Java", Typed="Static", Execution="Compiled", Paradigm="Object Oriented"}
        );
    }

    public DbSet<ProgrammingLanguage>? ProgrammingLanguages { get; set;}
}