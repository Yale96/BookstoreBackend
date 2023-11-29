using BookstoreBackend.Controllers;
using BookstoreBackend.Models;
using BookstoreBackend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookstoreBackend.Test
{
    [TestClass]
    public class BookApiControllerTests
    {
        private Mock<IBookRepository> mockRepository;
        private BookApiController controller;

        [TestInitialize]
        public void Setup()
        {
            mockRepository = new Mock<IBookRepository>();
            controller = new BookApiController(mockRepository.Object);
        }

        [TestMethod]
        public void GetAllBooks_ShouldReturnOkResultWithBooks()
        {
            mockRepository.Setup(r => r.GetAllBooks())
                .Returns(
                    new List<Book> {
                        new Book { Title = "Test Book 1", Description = "Test Book 1", Author = "Author 1", NumberOfPages = 101 },
                        new Book { Title = "Test Book 2", Description = "Test Book 2", Author = "Author 2", NumberOfPages = 202 }
                });

            var result = controller.GetAllBooks() as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(2, (result.Value as IEnumerable<Book>).Count());
        }

        [TestMethod]
        public void GetBookById_ExistingId_ShouldReturnOkResultWithBook()
        {
            var existingBook = new Book { Title = "Test Book 1", Description = "Test Book 1", Author = "Author 1", NumberOfPages = 101 };
            mockRepository.Setup(r => r.GetBookById(1)).Returns(existingBook);

            var result = controller.GetBookById(1) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual("Test Book 1", (result.Value as Book).Title);
        }

        [TestMethod]
        public void GetBookById_NonexistentId_ShouldReturnNotFoundResult()
        {
            mockRepository.Setup(r => r.GetBookById(999)).Returns((Book)null);

            var result = controller.GetBookById(999) as NotFoundResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public void AddBook_ValidBook_ShouldReturnCreatedAtActionResult()
        {
            var newBook = new Book { Title = "Test Book 1", Description = "Test Book 1", Author = "Author 1", NumberOfPages = 101 };
            mockRepository.Setup(r => r.AddBook(It.IsAny<Book>()));

            var result = controller.AddBook(newBook) as CreatedAtActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual("GetBookById", result.ActionName);
            Assert.IsNotNull(result.RouteValues);
            Assert.IsTrue(result.RouteValues.ContainsKey("id"));
        }

        // Ook deze test faalt, het heeft ook hier met de context te maken waarschijnlijk.
        // Ik ga hier nog wel even naar kijken.
        [TestMethod]
        public void UpdateBook_ExistingBook_ShouldReturnNoContentResult()
        {
            var existingBook = new Book { Title = "Test Book 1", Description = "Test Book 1", Author = "Author 1", NumberOfPages = 101 };
            mockRepository.Setup(r => r.GetBookById(1)).Returns(existingBook);
            mockRepository.Setup(r => r.UpdateBook(It.IsAny<Book>()));

            var result = controller.UpdateBook(1, existingBook) as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(204, result.StatusCode);
        }

        [TestMethod]
        public void UpdateBook_NonexistentBook_ShouldReturnBadRequestResult()
        {
            mockRepository.Setup(r => r.GetBookById(999)).Returns((Book)null);

            var result = controller.UpdateBook(999, new Book()) as BadRequestResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public void DeleteBook_ExistingId_ShouldReturnNoContentResult()
        {
            var existingBook = new Book { Title = "Test Book 1", Description = "Test Book 1", Author = "Yannick", NumberOfPages = 101 };
            mockRepository.Setup(r => r.GetBookById(1)).Returns(existingBook);
            mockRepository.Setup(r => r.DeleteBook(It.IsAny<int>()));

            var result = controller.DeleteBook(1) as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(204, result.StatusCode);
        }

        [TestMethod]
        public void DeleteBook_NonexistentId_ShouldReturnNotFoundResult()
        {
            mockRepository.Setup(r => r.GetBookById(999)).Returns((Book)null);

            var result = controller.DeleteBook(999) as NotFoundResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }
    }
}
