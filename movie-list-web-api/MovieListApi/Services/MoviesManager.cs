using Microsoft.EntityFrameworkCore;
using MovieListApi.Infrastructure.Database;
using MovieListApi.Models.Entities;

namespace MovieListApi.Services;

public class MoviesManager : IMoviesManager
{
    private readonly MoviesDbContext _dbContext;

    public MoviesManager(MoviesDbContext dbContext)
    {
        _dbContext = dbContext;
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
