using MovieListApi.Models.Entities;
using MovieListApi.Models.ViewModels;

namespace MovieListApi.Services;

public interface IMovieGenresManager
{
    Task<ICollection<MovieGenre>> GetMovieGenres();
    Task<(bool success, MovieGenre? entity)> AddMovieGenre(MovieGenreViewModel model);
}
