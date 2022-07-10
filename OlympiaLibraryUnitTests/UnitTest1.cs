using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace OlympiaLibraryUnitTests
{
    [TestClass]
    public class BookTests
    {
        private readonly BookService _bookservice;
        public BookTests(BookService bookService)
        {
            _bookService = bookService;
        }

        [TestMethod]
        public void AddBook_Test()
        {
            Book book = new Book();
            book.Title = "UnitTest";
          

        }
    }
}
