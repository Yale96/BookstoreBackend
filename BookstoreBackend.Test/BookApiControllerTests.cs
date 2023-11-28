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
            // Arrange
            mockRepository.Setup(r => r.GetAllBooks())
                .Returns(new List<Book> { new Book { Id = 1, Title = "Test Book 1" }, new Book { Id = 2, Title = "Test Book 2" } });

            // Act
            var result = controller.GetAllBooks() as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(2, (result.Value as IEnumerable<Book>).Count());
        }

        [TestMethod]
        public void GetBookById_ExistingId_ShouldReturnOkResultWithBook()
        {
            // Arrange
            var existingBook = new Book { Id = 1, Title = "Test Book" };
            mockRepository.Setup(r => r.GetBookById(1)).Returns(existingBook);

            // Act
            var result = controller.GetBookById(1) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual("Test Book", (result.Value as Book).Title);
        }

        [TestMethod]
        public void GetBookById_NonexistentId_ShouldReturnNotFoundResult()
        {
            // Arrange
            mockRepository.Setup(r => r.GetBookById(999)).Returns((Book)null);

            // Act
            var result = controller.GetBookById(999) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public void AddBook_ValidBook_ShouldReturnCreatedAtActionResult()
        {
            // Arrange
            var newBook = new Book { Title = "New Book" };
            mockRepository.Setup(r => r.AddBook(It.IsAny<Book>()));

            // Act
            var result = controller.AddBook(newBook) as CreatedAtActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual("GetBookById", result.ActionName);
            Assert.IsNotNull(result.RouteValues);
            Assert.IsTrue(result.RouteValues.ContainsKey("id"));
        }

        [TestMethod]
        public void UpdateBook_ExistingBook_ShouldReturnNoContentResult()
        {
            // Arrange
            var existingBook = new Book { Id = 1, Title = "Existing Book" };
            mockRepository.Setup(r => r.GetBookById(1)).Returns(existingBook);
            mockRepository.Setup(r => r.UpdateBook(It.IsAny<Book>()));

            // Act
            var result = controller.UpdateBook(1, existingBook) as NoContentResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(204, result.StatusCode);
        }

        [TestMethod]
        public void UpdateBook_NonexistentBook_ShouldReturnBadRequestResult()
        {
            // Arrange
            mockRepository.Setup(r => r.GetBookById(999)).Returns((Book)null);

            // Act
            var result = controller.UpdateBook(999, new Book()) as BadRequestResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public void DeleteBook_ExistingId_ShouldReturnNoContentResult()
        {
            // Arrange
            var existingBook = new Book { Id = 1, Title = "Existing Book" };
            mockRepository.Setup(r => r.GetBookById(1)).Returns(existingBook);
            mockRepository.Setup(r => r.DeleteBook(It.IsAny<int>()));

            // Act
            var result = controller.DeleteBook(1) as NoContentResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(204, result.StatusCode);
        }

        [TestMethod]
        public void DeleteBook_NonexistentId_ShouldReturnNotFoundResult()
        {
            // Arrange
            mockRepository.Setup(r => r.GetBookById(999)).Returns((Book)null);

            // Act
            var result = controller.DeleteBook(999) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }
    }
}
