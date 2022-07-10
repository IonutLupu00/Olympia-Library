using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Olympia_Library.Data;
using Olympia_Library.Models;
using Olympia_Library.Models.GenreModel;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Controllers;
using WebApplication.Models;
using WebApplication.Repositories;
using WebApplication.Services;

namespace OlympiaLibraryTests
{
    [TestClass]
    public class BookTests
    {
        AuthorService authorService;
        GenreService genreService;
        BorrowService borrowService;
        BranchService branchService;
        BookService bookService;
        IHostEnvironment hostEnvironment;
        Mock<IRepositoryWrapper> moqIRepositoryWrapper;

        //public BookTests(AuthorService authorService, GenreService genreService, BorrowService borrowService, BranchService branchService, BookService bookService, IHostEnvironment hostEnvironment, Mock<IRepositoryWrapper> moqIRepositoryWrapper)
        //{
        //    this.authorService = authorService;
        //    this.genreService = genreService;
        //    this.borrowService = borrowService;
        //    this.branchService = branchService;
        //    this.bookService = bookService;
        //    this.hostEnvironment = hostEnvironment;
        //    this.moqIRepositoryWrapper = moqIRepositoryWrapper;
        //}

        [TestInitialize]
        public void Initialize()
        {
            moqIRepositoryWrapper = new Mock<IRepositoryWrapper>() { CallBase = true };
            
            authorService = new AuthorService(moqIRepositoryWrapper.Object, null);
            bookService = new BookService(moqIRepositoryWrapper.Object, authorService, hostEnvironment, genreService);
            genreService = new GenreService(moqIRepositoryWrapper.Object, null);
            branchService = new BranchService(moqIRepositoryWrapper.Object, null);
            borrowService = new BorrowService(moqIRepositoryWrapper.Object, bookService, branchService);
        }



        
        [TestMethod]
        public void AddAuthor_and_GetAuthorByCondition_Test()  

        {       //to search for an author, an author must be added into the database
                //to check if an author has been succesfully added, you must search for it
                //i thought that it would be better to mix the two functionalities together

            List<Author> database = new List<Author>();

            moqIRepositoryWrapper.Setup(x => x.AuthorRepository.Create(It.IsAny<Author>()))
                .Callback<Author>(author => database.Add(author));

            moqIRepositoryWrapper.Setup(x => x.AuthorRepository.FindByCondition(It.IsAny<System.Linq.Expressions.Expression<System.Func<Author, bool>>>()))
                .Returns(database.AsQueryable());


            AuthorModel author = new AuthorModel();
            author.Name = "UnitTestAuthor";
            author.Birth_year = 1970;
            author.Deceased = false;
            authorService.AddAuthor(author);


            Author author_found = authorService.GetAuthorByCondition(b => b.Name == "UnitTestAuthor").FirstOrDefault();
            Assert.AreEqual(author.Name, author_found.Name);
            Assert.AreEqual(author.Birth_year, author_found.Birth_year);
            Assert.AreEqual(author.Deceased, author_found.Deceased);


        }



        [TestMethod]
        public void AddAuthor_and_RemoveAuthor_Test()
            //ads author in database, checks that there is an author added
            //removes the author, checks that the database is empty
        {
            List<Author> database = new List<Author>();

            moqIRepositoryWrapper.Setup(x => x.AuthorRepository.Create(It.IsAny<Author>()))
                .Callback<Author>(author => database.Add(author));

            moqIRepositoryWrapper.Setup(x => x.AuthorRepository.FindByCondition(It.IsAny<System.Linq.Expressions.Expression<System.Func<Author, bool>>>()))
          .Returns(database.AsQueryable());


            moqIRepositoryWrapper.Setup(x => x.AuthorRepository.Delete(It.IsAny<Author>()))
           .Callback<Author>(author => database.Remove(database.First()));

      

            AuthorModel author = new AuthorModel();
            author.Name = "UnitTestAuthor";
            author.Birth_year = 1970;
            author.Deceased = false;
            authorService.AddAuthor(author);
            Assert.IsTrue(database.Count() == 1);


            authorService.DeleteAuthor(author); 

            Assert.IsTrue(database.Count() == 0);

        }

        [TestMethod]//adds author in database, edits authors deceased status, finds author and checks
        //status
        public void AddAuthor_and_EditAuthor_Test()

        {
            List<Author> database = new List<Author>();

            moqIRepositoryWrapper.Setup(x => x.AuthorRepository.Create(It.IsAny<Author>()))
                .Callback<Author>(author => database.Add(author));

            moqIRepositoryWrapper.Setup(x => x.AuthorRepository.FindByCondition(It.IsAny<System.Linq.Expressions.Expression<System.Func<Author, bool>>>()))
          .Returns(database.AsQueryable());


            moqIRepositoryWrapper.Setup(x => x.AuthorRepository.Update(It.IsAny<Author>()));

            AuthorModel author = new AuthorModel();
            author.Name = "UnitTestAuthor";
            author.Birth_year = 1970;
            author.Deceased = false;
            authorService.AddAuthor(author);
            Assert.IsFalse(author.Deceased);

            AuthorModel author_updated = new AuthorModel();
            author_updated.Name = "UnitTestAuthor";
            author_updated.Deceased = true;
            authorService.UpdateAuthor(author_updated);

            Assert.IsTrue(authorService.GetAuthorByCondition(b=>b.Name=="UnitTestAuthor").First().Deceased);

        }


        
        [TestMethod]
        [HttpPost]
        public void TestAddGenre_TestEditGenre()
        {

            var numberOfGenresToCreate = 999;

            List<Genre> genres = new List<Genre>();

            moqIRepositoryWrapper.Setup(g => g.GenreRepository.Create(It.IsAny<Genre>()))
                .Callback<Genre>(genre => genres.Add(genre));

            moqIRepositoryWrapper.Setup(x => x.GenreRepository.FindByCondition(It.IsAny<System.Linq
                .Expressions.Expression<System.Func<Genre, bool>>>()))
                .Returns(genres.AsQueryable());
            moqIRepositoryWrapper.Setup(x => x.GenreRepository.Update(It.IsAny<Genre>()));

            

            for(int i = 0; i < numberOfGenresToCreate; i++)
            {
                NewGenreModel someGenre = new NewGenreModel
                {
                    Name = "Test Genre" + i.ToString(),
                    ImageUrl = "",
                    ImageFile = null,
                    NewName = ""
                };

                genreService.AddGenre(someGenre);
                genreService.Save();
                Assert.IsNotNull(genreService.FindGenreByCondition(g => g.Name == someGenre.Name));
            }
            
            

            for (int i = 0; i < numberOfGenresToCreate; i++)
            {
                NewGenreModel someGenreUpdate = new NewGenreModel
                {
                    Name = "",
                    ImageUrl = "",
                    ImageFile = null,
                    NewName = "Test Genre" + i.ToString() + (i + 2).ToString()
                };
                //testing if the genre name has been updated correctly
                genreService.UpdateGenre(someGenreUpdate);
                genreService.Save();
                
                Assert.IsNotNull(genreService.FindGenreByCondition(g => g.Name == someGenreUpdate.NewName).FirstOrDefault());

            }

        }

        //[TestMethod]
        //public void TestAddBook_TestUpdateBook()
        //{
        //    List<Book> books = new List<Book>();
        //    int numberToCreate = 100;
        //    moqIRepositoryWrapper.Setup(g => g.BookRepository.Create(It.IsAny<Book>()))
        //        .Callback<Book>(book => books.Add(book));

        //    moqIRepositoryWrapper.Setup(x => x.BookRepository.FindByCondition(It.IsAny<System.Linq.Expressions.Expression<System.Func<Book, bool>>>()))
        //        .Returns(books.AsQueryable());
        //    moqIRepositoryWrapper.Setup(x => x.BookRepository.Update(It.IsAny<Book>()));

        //    for(int i = 0; i < numberToCreate; i++)
        //    {
        //        var newBook = new BookModel
        //        {
        //            Title = "Book" + i.ToString(),
                    
        //        };

        //        bookService.AddBook(newBook);
        //        bookService.Save();
        //        Assert.IsNotNull(bookService.GetBooksByCondition(b => b.Title == newBook.Title).FirstOrDefault());
        //    }
        //}

        
    }
}
