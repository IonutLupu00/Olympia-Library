using Microsoft.AspNetCore.Http;
using Olympia_Library.Data;
using Olympia_Library.Models.GenreModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebApplication.Models;

namespace WebApplication.Services
{
    public interface IBookService
    {           //USED FOR TESTING PURPOSES, DO NOT DELETE
        void AddBook(BookModel book);
        void AddGenre(NewGenreModel model);
        BookModel BuildBookDetailModel(int id);
        void DeleteBook(BookModel book);
        List<string> ExtractBookTitles();
        IEnumerable<Book> GetAll();
        List<Book> GetBooksByCondition(Expression<Func<Book, bool>> expression);
        string GetGenreName(int bookId);
        IEnumerable<Book> GetLatestAdditions(int number);
        void UpdateBook(BookModel book);
        void UpdateBookCover(IFormFile file, int bookId);
        void UpdateGenreIcon(IFormFile file, int genreId);
    }
}