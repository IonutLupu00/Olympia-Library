using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Olympia_Library.Data;
using WebApplication.Models;
using WebApplication.Repositories;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Olympia_Library.Models.GenreModel;

namespace WebApplication.Services
{
    public class BookService : BaseService
    {

        private readonly AuthorService _authorService;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly GenreService _genreService;


        public BookService(IRepositoryWrapper repositoryWrapper, AuthorService authorService, IHostEnvironment hostEnvironment, GenreService genreService) : base(repositoryWrapper)
        {
            _authorService = authorService;
            _hostEnvironment = hostEnvironment;
            _genreService = genreService;
        }

        public IEnumerable<Book> GetAll()
        {
            return repositoryWrapper.BookRepository.FindByCondition(b => b.BookId != 0);
        }

        public void AddBook(BookModel book)
        {
            Book new_book = new Book();
            new_book.Title = book.Title;
            new_book.GenreId = _genreService.FindGenreByCondition(b => b.Name == book.Genre).FirstOrDefault().Id;
            new_book.AuthorId = _authorService.GetAuthorByCondition(b => b.Name == book.AuthorName).FirstOrDefault().AuthorId;
            new_book.ImageUrl = book.ImageUrl;
            repositoryWrapper.BookRepository.Create(new_book);

            new_book.ImageUrl = UpdateBookCover(book.CoverImage);

        }

        public void UpdateBook(BookModel book)
        {
            Book updated_book = GetBooksByCondition(b => b.Title == book.Title).First();

            if (book.AuthorName != null)
                updated_book.AuthorId = (_authorService
                    .GetAuthorByCondition(b => b.Name == book.AuthorName)
                    .FirstOrDefault()).AuthorId;

            if (book.Genre != null)
                updated_book.GenreId = repositoryWrapper.GenreRepository
                    .FindByCondition(g => g.Name == book.Genre)
                    .FirstOrDefault().Id;

            if(book.NewTitle != null)
            {
                updated_book.Title = book.NewTitle;
            }

            if (book.Genre != null)
            {
                updated_book.GenreId = _genreService.FindGenreByCondition(b => b.Name == book.Genre).First().Id;
            }

            if (book.CoverImage != null)
            {
                var deletedCoverPath = RelativeToAbsolutePath(updated_book.ImageUrl);
                //File.SetAttributes(deletedCoverPath, FileAttributes.Normal);
                if (File.Exists(deletedCoverPath) && !deletedCoverPath.Contains("defaultCover")) 
                {
                    
                    File.Delete(deletedCoverPath);
                }

                updated_book.ImageUrl = UpdateBookCover(book.CoverImage);

            }
            if(book.Description != null)
            {
                updated_book.Description = book.Description;
            }
            if(book.Price != 0)
            {
                updated_book.Price = book.Price;
            }
            
            

            repositoryWrapper.BookRepository.Update(updated_book);
        }

        public List<Book> GetBooksByCondition(Expression<Func<Book, bool>> expression)
        {
            return repositoryWrapper.BookRepository.FindByCondition(expression).ToList();
        }

        public void DeleteBook(BookModel book)
        {

            var deleted_book = GetBooksByCondition(b => b.Title == book.Title).First();
            if (deleted_book != null)
            {
                var deletedCoverPath = RelativeToAbsolutePath(deleted_book.ImageUrl);
                if (File.Exists(deletedCoverPath) && !deletedCoverPath.Contains("defaultCover"))
                {
                    File.Delete(deletedCoverPath);
                }

                repositoryWrapper.BookRepository.Delete(deleted_book);
            }
        }

        public List<string> ExtractBookTitles()
        {
            List<Book> books = GetBooksByCondition(b => b.BookId != -1);
            List<string> BookNames = new List<string>();
            foreach (var book in books)
            {
                BookNames.Add(book.Title);
            }
            return BookNames;
        }

        public BookModel BuildBookDetailModel(int id)
        {

            var book = GetBooksByCondition(b => b.BookId == id).FirstOrDefault();

            var bookModel = new BookModel
            {
                Title = book.Title,
                AuthorName = _authorService.GetAuthorByCondition(a => a.AuthorId == book.AuthorId).FirstOrDefault().Name,
                Genre = repositoryWrapper.GenreRepository.FindByCondition(g => g.Id == book.GenreId).FirstOrDefault().Name,
                ImageUrl = book.ImageUrl,
                Description = book.Description,
                Price = book.Price,
                BookId = book.BookId
            };
            return bookModel;
        }

        public string GetGenreName(int bookId)
        {
            var genreId = repositoryWrapper.BookRepository.FindByCondition(b => b.BookId == bookId).FirstOrDefault().GenreId;
            var genreName = repositoryWrapper.GenreRepository.FindByCondition(g => g.Id == genreId).FirstOrDefault().Name;

            return genreName;
        }

        public IEnumerable<Book> GetLatestAdditions(int number)
        {
            return GetAll().OrderByDescending(b => b.BookId).Take(number);
        }

        public Book GetRandomBook()
        {
            var bookList = GetAll().ToList();
            var random = new Random();
            List<int> bookIdList = bookList.Select(o => o.BookId).ToList();
            var randomIdIndex = random.Next(bookIdList.Count);
            return bookList[randomIdIndex];

        }

        [HttpPost]
        public string UpdateBookCover(IFormFile file)
        {

            if (file != null)
            {

                var uniqueFileName = GetUniqueFileName(file.FileName);

                var folderPath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot\\images", "bookCovers");
                var filePath = Path.Combine(folderPath, uniqueFileName);

                file.CopyTo(new FileStream(filePath, FileMode.Create));

                return  "/images/bookCovers/" + uniqueFileName;


            }

            else

                return  "/images/bookCovers/defaultCover.jpg";

        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

        public bool IsBookInDatabase(string bookName)
        {
            if (repositoryWrapper.BookRepository.FindByCondition(b => b.Title.ToLower() == bookName.ToLower()).Any())
            {
                return true;
            }
            else
                return false;
        }

        public bool IsBookInDatabase(int bookId)
        {
            if (repositoryWrapper.BookRepository.FindByCondition(b => b.BookId == bookId).Any())
            {
                return true;
            }
            else
                return false;
        }

        private string RelativeToAbsolutePath(string relativePath)
        {
            var pathTokens = relativePath.Split("/");
            string newRelativePath = "";
            foreach(var token in pathTokens)
            {
                
                newRelativePath += token;
                newRelativePath += "\\";
            }
            newRelativePath = _hostEnvironment.ContentRootPath + newRelativePath;


            newRelativePath = newRelativePath.Remove(newRelativePath.Length - 1);

            return newRelativePath;
        }

    }
}
