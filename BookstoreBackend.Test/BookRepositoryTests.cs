using BookstoreBackend.Data;
using BookstoreBackend.Models;
using BookstoreBackend.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreBackend.Test
{
    [TestClass]
    public class BookRepositoryTests
    {
        private ApplicationDbContext _context;
        private BookRepository _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            // Set up a new in-memory database for each test
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new BookRepository(_context);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            SeedDatabase();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Dispose();
        }

        [TestMethod]
        public void GetAllBooks_ShouldReturnAllBooks()
        {
            var books = _repository.GetAllBooks();

            Assert.IsNotNull(books);
            Assert.AreEqual(2, books.Count());
        }

        [TestMethod]
        public void GetBookById_ExistingId_ShouldReturnBook()
        {
            int existingId = 1;

            var book = _repository.GetBookById(existingId);

            Assert.IsNotNull(book);
            Assert.AreEqual(existingId, book.Id);
        }

        [TestMethod]
        public void GetBookById_NonexistentId_ShouldReturnNull()
        {
            int nonexistentId = 999;

            var book = _repository.GetBookById(nonexistentId);

            Assert.IsNull(book);
        }

        [TestMethod]
        public void AddBook_ShouldAddBookToDatabase()
        {
            var newBook = new Book
            {
                Title = "New Test Book",
                Description = "Description for New Test Book",
                Author = "Test Author",
                NumberOfPages = 200
            };

            _repository.AddBook(newBook);

            var addedBook = _repository.GetBookById(newBook.Id);
            Assert.IsNotNull(addedBook);
            Assert.AreEqual(newBook.Title, addedBook.Title);
        }

        [TestMethod]
        public void UpdateBook_ShouldUpdateBookInDatabase()
        {
            int existingId = 1;
            var updatedBook = new Book
            {
                Id = existingId,
                Title = "Updated Test Book",
                Description = "Description for Updated Test Book",
                Author = "Updated Author",
                NumberOfPages = 250
            };

            _repository.UpdateBook(updatedBook);

            var bookAfterUpdate = _repository.GetBookById(existingId);
            Assert.IsNotNull(bookAfterUpdate);
            Assert.AreEqual(updatedBook.Title, bookAfterUpdate.Title);
            Assert.AreEqual(updatedBook.Author, bookAfterUpdate.Author);
        }

        [TestMethod]
        public void DeleteBook_ShouldDeleteBookFromDatabase()
        {
            int existingId = 1;

            _repository.DeleteBook(existingId);

            var deletedBook = _repository.GetBookById(existingId);
            Assert.IsNull(deletedBook);
        }

        private void SeedDatabase()
        {
            _context.Books.AddRange(new[]
            {
                new Book { Title = "Test Book 1", Description = "Test Book 1", Author = "Yannick", NumberOfPages = 404 },
                new Book { Title = "Test Book 2", Description = "Test Book 2", Author = "Yannick", NumberOfPages = 404 },
            });
            _context.SaveChanges();
        }
    }
}
