using BookStoreApi.Models;
using BookStoreApi.Services;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BooksService _bookService;

        public BooksController(BooksService bookService) =>
            _bookService = bookService;

        //[Authorize(Policy = "ElevatedRights")]
        [Route("all-books")]
        [HttpGet]
        public async Task<List<Book>> Get() =>
             await _bookService.GetAsync();
        
        [HttpGet("{author:length(1,10)}")]
        public async Task<ActionResult<Book>> Get(string author)
        {
            var book = await _bookService.GetAsync(author);

            if (book == null)
            {
                return NotFound();
            } else
            {
                Console.WriteLine(DateTime.Now);
                return book;
            }
        }

        [Route("create-book")]
        [HttpPost]
        public async Task<IActionResult> Post(Book newBook)
        {
            await _bookService.CreateAsync(newBook);

            return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Book updatedBook)
        {
            var book = await _bookService.GetAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            updatedBook.Id = book.Id;

            await _bookService.UpdateAsync(id, updatedBook);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _bookService.GetAsync(id);

            if(book == null) { return NotFound(); }

            await _bookService.RemoveAsync(id);

            return NoContent();
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest userLogin)
        {
            ErrorOr<dynamic> loginResponse = await _bookService.Login(userLogin.UserName, userLogin.Password);
            return Ok(loginResponse);
        }
    }
}
