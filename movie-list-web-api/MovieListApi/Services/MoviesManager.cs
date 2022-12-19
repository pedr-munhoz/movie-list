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

    public async Task<(bool success, Movie? movie)> AddMovieToWatch(MovieViewModel model)
    {
        var entity = ModelToEntity(model);

        await _dbContext.Movies.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return (true, entity);
    }

    public async Task<(bool success, Movie? movie)> AddWatchedMovie(MovieViewModel model)
    {
        var entity = ModelToEntity(model);
        entity.Watched = true;

        await _dbContext.Movies.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return (true, entity);
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

    private Movie ModelToEntity(MovieViewModel model)
    {
        var entity = new Movie
        {
            Name = model.Name,
            ReleaseDate = model.ReleaseDate,
            Country = model.Country,
        };

        return entity;
    }
}
