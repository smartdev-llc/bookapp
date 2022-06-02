using BookServices;
using Entities;
using System;
using System.Collections.Generic;
using Xunit;

namespace BookTest
{
    public class BookTest
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

        public BookTest()
        {
            this.iBookService = new BookService();
            this.iBookService.MockData(Books, Users, UserBooks);
        }

        [Fact]
        public void GetCompletedBooksTest_Error()
        {
            var message = "userId is not existed";
            var exception = Assert.Throws<Exception>(() => iBookService.GetCompletedBooks(100));
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void GetCompletedBooksTest_Success()
        {
            var expected = 2;
            Assert.Equal(expected, iBookService.GetCompletedBooks(1).Count);
        }

        [Fact]
        public void AddBookReading_Book_NotFound_Error()
        {
            var message = "Book is not found";
            var model = new Entities.Requests.BookReadingRequest()
            {
                BookId = 111,
                CurrentPage = 1,
                UserId = 1,
            };
            var exception = Assert.Throws<Exception>(() => iBookService.AddBookReading(model));
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void AddBookReading_Book_Added_Error()
        {
            var message = "Book is already added";
            var model = new Entities.Requests.BookReadingRequest()
            {
                BookId = 1,
                CurrentPage = 1,
                UserId = 1,
            };
            var exception = Assert.Throws<Exception>(() => iBookService.AddBookReading(model));
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void AddBookReading_Book_Added_Success()
        {
            var model = new Entities.Requests.BookReadingRequest()
            {
                BookId = 2,
                CurrentPage = 1,
                UserId = 2,
            };
            var expected = 2;
            Assert.Equal(expected, iBookService.AddBookReading(model).Count);
        }

        [Fact]
        public void GetAllBooks_NoSearch_Success()
        {
            var expected = 3;
            Assert.Equal(expected, iBookService.GetAllBooks().Count);
        }

        [Fact]
        public void GetAllBooks_Search_Success()
        {
            var expected = 1;
            Assert.Equal(expected, iBookService.GetAllBooks("book 1").Count);
        }

        [Fact]
        public void GetAllBooks_Search_User_Success()
        {
            var expected = 3;
            Assert.Equal(expected, iBookService.GetAllBooks(userId:1).Count);
        }
    }
}