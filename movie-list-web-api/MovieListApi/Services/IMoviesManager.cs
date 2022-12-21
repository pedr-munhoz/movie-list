using MovieListApi.Models.Entities;
using MovieListApi.Models.ViewModels;

namespace MovieListApi.Services;

public interface IMoviesManager
{
    Task<ICollection<Movie>> CreateMoviesToWatch();
    Task<ICollection<Movie>> ListWatchedMovies();
    Task<(bool success, Movie? entity)> CreateMovieToWatch(MovieViewModel model);
    Task<(bool success, Movie? entity)> CreateWatchedMovie(MovieViewModel model);
    Task<(bool success, Movie? entity)> SetMovieAsWatched(string stringId);
}
