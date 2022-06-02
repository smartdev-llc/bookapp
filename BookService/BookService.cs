using Entities;
using Entities.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookServices
{
    public class BookService : IBookService
    {
        private static List<Book> Books { get; set; } = new List<Book>();
        private static List<User> Users { get; set; } = new List<User>();
        private static List<UserBook> UserBooks { get; set; } = new List<UserBook>();

        public List<UserBook> AddBookReading(BookReadingRequest model)
        {
            var currentBook = Books.FirstOrDefault(x => x.Id.Equals(model.BookId));
            if (currentBook == null)
                throw new Exception("Book is not found");

            if (UserBooks.Any(x => x.BookId == model.BookId && x.UserId == model.UserId))
                throw new Exception("Book is already added");

            var newReadingBook = new UserBook
            {
                Id = GetCurrentUserBookKey(),
                BookId = model.BookId,
                UserId = model.UserId,
                LastRead = DateTime.Now,
                ReadingStatus = model.CurrentPage < currentBook.LastPage ? ReadingState.Reading : ReadingState.Completed,
            };
            UserBooks.Add(newReadingBook);
            return UserBooks.Where(x => x.UserId == model.UserId).ToList();
        }

        public List<Book> GetAllBooks(string searchStr = "", int? userId = null)
        {
            var result = Books.AsEnumerable();
            if (!string.IsNullOrEmpty(searchStr))
                result = result.Where(x => x.BookName.ToLower().Contains(searchStr.ToLower()));
            if (userId != null)
            {
                result = from r in result
                         join ub in UserBooks.Where(x => x.UserId.Equals(userId)) on r.Id equals ub.Id
                         select r;
            }
            return result.ToList();
        }

        public List<Book> GetCompletedBooks(int userId)
        {
            if (!Users.Any(x => x.Id == userId))
                throw new Exception("userId is not existed");

            var result = from b in Books
                         join ub in UserBooks.Where(x => x.ReadingStatus.Equals(ReadingState.Completed) && x.UserId == userId) on b.Id equals ub.BookId
                         select b;
            return result.ToList();
        }

        public void MockData(List<Book> books, List<User> users, List<UserBook> userBooks)
        {
            Books = books;
            Users = users;
            UserBooks = userBooks;
        }


        /// <summary>
        /// Get current Index ID for mock
        /// </summary>
        /// <returns></returns>
        private int GetCurrentUserBookKey()
        {
            return UserBooks.Select(x => x.Id).Max() + 1;
        }

    }
}