using BookstoreBackend.Models;
using BookstoreBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreBackend.Controllers
{
    [ApiController]
    [Route("api/books")]
    //[Authorize("RequireAdminRole")]
    public class BookApiController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookApiController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _bookRepository.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _bookRepository.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest();
            }

            _bookRepository.AddBook(book);

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book book)
        {
            if (book == null || id != book.Id)
            {
                return BadRequest();
            }

            var existingBook = _bookRepository.GetBookById(id);

            if (existingBook == null)
            {
                return NotFound();
            }

            _bookRepository.UpdateBook(book);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var existingBook = _bookRepository.GetBookById(id);

            if (existingBook == null)
            {
                return NotFound();
            }

            _bookRepository.DeleteBook(id);

            return NoContent();
        }
    }
}
