using Olympia_Library.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Services
{
    public interface IAuthorService
    {           //USED FOR TESTING PURPOSES, DO NOT DELETE
        void AddAuthor(AuthorModel author);
        void DeleteAuthor(AuthorModel author);
        List<string> ExtractAuthorNames();
        List<Author> GetAuthorByCondition(Expression<Func<Author, bool>> expression);
        void UpdateAuthor(AuthorModel author);
    }
}