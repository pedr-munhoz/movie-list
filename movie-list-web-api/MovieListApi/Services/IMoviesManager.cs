using MovieListApi.Models.Entities;
using MovieListApi.Models.ViewModels;

namespace MovieListApi.Services;

public interface IMoviesManager
{
    Task<ICollection<Movie>> GetMoviesToWatch();
    Task<ICollection<Movie>> GetWatchedMovies();
    Task<(bool success, Movie? movie)> AddMovieToWatch(MovieViewModel model);
    Task<(bool success, Movie? movie)> AddWatchedMovie(MovieViewModel model);
    Task<(bool success, Movie? movie)> MarkMovieAsWatched(string stringId);
}
