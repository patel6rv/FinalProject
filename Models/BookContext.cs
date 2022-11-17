using Microsoft.EntityFrameworkCore;
namespace FinalProject.Models;

public class BookContext : DbContext
{
    public BookContext(DbContextOptions<BookContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasData(
            new Book {Id = 1, Title = "Harry Potter", Author="J.K. Rowling" , Description="Magic School", PublishYear = "1993"}
        );
    }

    public DbSet<Book>? Books { get; set;}
}