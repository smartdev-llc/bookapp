using Entities;
using Entities.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookServices
{
    public interface IBookService
    {

        /// <summary>
        /// View a list of books they have completed reading
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<Book> GetCompletedBooks(int userId);

        /// <summary>
        /// Add a book to the list of books they have read
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<UserBook> AddBookReading(BookReadingRequest model);

        /// <summary>
        /// Search for a book by name We’re looking only for APIs, no Frontend is needed. Please make sure that the APIs are RESTful. Please make reasonable assumptions if anything is not clear.
        /// </summary>
        /// <param name="searchStr"></param>
        /// <returns></returns>
        List<Book> GetAllBooks(string searchStr = "", int? userId = null);

        void MockData(List<Book> books, List<User> users, List<UserBook> userBooks);
    }
}
