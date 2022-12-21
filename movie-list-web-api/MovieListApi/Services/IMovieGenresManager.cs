using MovieListApi.Models.Entities;
using MovieListApi.Models.ViewModels;

namespace MovieListApi.Services;

public interface IMovieGenresManager
{
    Task<ICollection<MovieGenre>> ListMovieGenres();
    Task<(bool success, MovieGenre? entity)> CreateMovieGenre(MovieGenreViewModel model);
}
