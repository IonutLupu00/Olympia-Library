using Olympia_Library.Models.BookModel;
using Olympia_Library.Models.HomeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Repositories;
using WebApplication.Services;

namespace Olympia_Library.Services
{
    public class HomeService : BaseService
    {
        private readonly BookService _bookService;

        public HomeService(IRepositoryWrapper repositoryWrapper, BookService bookService) : base(repositoryWrapper)
        {
            _bookService = bookService;
        }

        public HomeIndexModel BuildHomeIndexModel()
        {
            var books = _bookService.GetAll();

            var latestAdditions = _bookService.GetLatestAdditions(8)
                .Select(b => new BookListingModel {
                    Title = b.Title,
                    AuthorId = b.AuthorId,
                    ImageUrl = b.ImageUrl,
                    GenreId = b.GenreId,
                    BookId = b.BookId
                });

            var bookListing = books.Select(b => new BookListingModel
            {
                Title = b.Title,
                AuthorId = b.AuthorId,
                ImageUrl = b.ImageUrl,
                GenreId = b.GenreId,
                BookId = b.BookId
            });

            

            var genres = repositoryWrapper.GenreRepository.FindByCondition(g => !string.IsNullOrEmpty(g.Name));

            if (IsNullGenreImage())
            {
                foreach (var genre in genres)
                {
                    if (string.IsNullOrEmpty(genre.ImageUrl))
                    {
                        genre.ImageUrl = "/images/genreIcons/defaultIcon.png";
                    }
                }
                Save();
            }
            

            return new HomeIndexModel {
                BookListing = bookListing,
                Genres = genres,
                LatestAdditions = latestAdditions,
                SearchQuery = ""
            };

        }

        private bool IsNullGenreImage()
        {
            if (repositoryWrapper.GenreRepository.FindByCondition(g => g.ImageUrl == null).Any())
                return true;
            else return false;
        }
    }
}
