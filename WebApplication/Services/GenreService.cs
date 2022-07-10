using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Olympia_Library.Data;
using Olympia_Library.Models.GenreModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public class GenreService : BaseService
    {
        private readonly IHostEnvironment _hostEnvironment;

        public GenreService(IRepositoryWrapper repositoryWrapper, IHostEnvironment hostEnvironment) : base(repositoryWrapper) {
            _hostEnvironment = hostEnvironment;
        }

        public List<Genre> FindGenreByCondition(Expression<Func<Genre, bool>> expression)
        {
            return repositoryWrapper.GenreRepository.FindByCondition(expression).ToList();
        }

        public List<string> ExtractGenres()
        {
            List<Genre> genres = FindGenreByCondition(b => b.Id != -1);
            List<string> genres_names = new List<string>();
            foreach (var genre in genres)
            {
                genres_names.Add(genre.Name);
            }
            return genres_names;
        }

        [HttpPost]
        public void UpdateGenreIcon(IFormFile file, int genreId)
        {
            if (file != null)
            {

                var uniqueFileName = GetUniqueFileName(file.FileName);

                var folderPath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot\\images", "genreIcons");
                var oldIconRelativePath = repositoryWrapper.GenreRepository.FindByCondition(g => g.Id == genreId).FirstOrDefault().ImageUrl;
                var filePath = Path.Combine(folderPath, uniqueFileName);


                file.CopyTo(new FileStream(filePath, FileMode.Create));

                var relativePath = "/images/genreIcons/" + uniqueFileName;

                repositoryWrapper.GenreRepository
                            .FindByCondition(b => b.Id == genreId)
                            .FirstOrDefault()
                            .ImageUrl = relativePath;
            }

            else

            {
                repositoryWrapper.GenreRepository
                            .FindByCondition(genre => genre.Id == genreId)
                            .FirstOrDefault()
                            .ImageUrl = "/images/genreIcons/defaultIcon.png";
            }
        }

        [HttpPost]
        public void UpdateGenre(NewGenreModel model)
        {
            var updatedGenre = repositoryWrapper.GenreRepository.FindByCondition(g => g.Name == model.Name).FirstOrDefault();

            if (!string.IsNullOrEmpty(model.NewName))
                updatedGenre.Name = model.NewName;

            if (model.ImageFile != null)
                UpdateGenreIcon(model.ImageFile, updatedGenre.Id);
        }

        [HttpPost]
        public void AddGenre(NewGenreModel model)
        {

            if (FindGenreByCondition(genre => genre.Name == model.Name).Any())
            {
                UpdateGenreIcon(model.ImageFile, FindGenreByCondition(genre => genre.Name == model.Name).FirstOrDefault().Id);
            }
            else
            {

                var newGenre = new Genre
                {
                    Name = model.Name
                };

                repositoryWrapper.GenreRepository.Create(newGenre);

                Save();

                UpdateGenreIcon(model.ImageFile, newGenre.Id);
            }
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }




    }
}

