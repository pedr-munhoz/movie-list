using Microsoft.EntityFrameworkCore;
using MovieListApi.Infrastructure.Database;
using MovieListApi.Models.Entities;
using MovieListApi.Models.ViewModels;

namespace MovieListApi.Services;

public class MoviesManager : IMoviesManager
{
    private readonly MoviesDbContext _dbContext;

    public MoviesManager(MoviesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<(bool success, Movie? movie)> AddMovieToWatch(MovieViewModel model)
    {
        throw new NotImplementedException();
    }

    public Task<(bool success, Movie? movie)> AddWatchedMovie(MovieViewModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<Movie>> GetMoviesToWatch()
    {
        var entities = await _dbContext.Movies.Where(x => !x.Watched).ToListAsync();

        return entities;
    }

    public async Task<ICollection<Movie>> GetWatchedMovies()
    {
        var entities = await _dbContext.Movies.Where(x => x.Watched).ToListAsync();

        return entities;
    }
}
