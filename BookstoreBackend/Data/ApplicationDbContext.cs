using BookstoreBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookstoreBackend.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Toevoegen test data
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Test Book 1", Description = "Test Book 1", Author = "Author 1", NumberOfPages = 101 },
                new Book { Id = 2, Title = "Test Book 2", Description = "Test Book 2", Author = "Author 2", NumberOfPages = 202 },
                new Book { Id = 3, Title = "Test Book 3", Description = "Test Book 3", Author = "Author 3", NumberOfPages = 303 },
                new Book { Id = 4, Title = "Test Book 4", Description = "Test Book 4", Author = "Author 4", NumberOfPages = 404 },
                new Book { Id = 5, Title = "Test Book 5", Description = "Test Book 5", Author = "Author 5", NumberOfPages = 505 }
            );
        }
    }
}
