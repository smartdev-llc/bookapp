using BookServices;
using Entities;
using Entities.Requests;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private IBookService iBookService;
        #region Mock Data
        private static List<Book> Books = new List<Book>()
        {
            new Book(){ Id = 1, BookName = "Book 1", LastPage = 5},
            new Book(){ Id = 2, BookName = "Book 2", LastPage = 10},
            new Book(){ Id = 3, BookName = "Book 3", LastPage = 15},
        };
        private static List<User> Users = new List<User>()
        {
            new User(){ Id = 1, UserName = "Alex"},
            new User(){ Id = 2, UserName = "Kevin"},
        };
        private static List<UserBook> UserBooks = new List<UserBook>()
        {
            new UserBook(){ Id = 1, BookId = 1, UserId = 1, ReadingStatus = ReadingState.Reading, LastRead = DateTime.Now.AddDays(-2)},
            new UserBook(){ Id = 2, BookId = 2, UserId = 1, ReadingStatus = ReadingState.Completed, LastRead = DateTime.Now.AddDays(-2)},
            new UserBook(){ Id = 3, BookId = 3, UserId = 1, ReadingStatus = ReadingState.Completed, LastRead = DateTime.Now.AddDays(-1)},
            new UserBook(){ Id = 4, BookId = 1, UserId = 2, ReadingStatus = ReadingState.Reading, LastRead = DateTime.Now.AddDays(-1)},
        };
        #endregion

        public BookController(IBookService iBookService)
        {
            this.iBookService = iBookService;
            this.iBookService.MockData(Books, Users, UserBooks);
        }

        [HttpGet, Route("books/completed/{userId}")]
        public ActionResult<List<Book>> GetCompletedBooks(int userId)
        {
            try
            {
                var result = iBookService.GetCompletedBooks(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, Route("books/reading")]
        public ActionResult AddBookReading([FromBody] BookReadingRequest model)
        {
            if(model == null) return BadRequest("model is required");
            try
            {
                var res = iBookService.AddBookReading(model);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("books/all")]
        public ActionResult<List<Book>> GetAllBooks(string searchStr = "", int? userId = null)
        {
            try
            {
                var result = iBookService.GetAllBooks(searchStr, userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

    }
}