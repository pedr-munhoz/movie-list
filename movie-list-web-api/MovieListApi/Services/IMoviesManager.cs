using MovieListApi.Models.Entities;
using MovieListApi.Models.ViewModels;

namespace MovieListApi.Services;

public interface IMoviesManager
{
    Task<ICollection<Movie>> GetMoviesToWatch();
    Task<ICollection<Movie>> GetWatchedMovies();
    Task<(bool success, Movie? entity)> AddMovieToWatch(MovieViewModel model);
    Task<(bool success, Movie? entity)> AddWatchedMovie(MovieViewModel model);
    Task<(bool success, Movie? entity)> MarkMovieAsWatched(string stringId);
}
