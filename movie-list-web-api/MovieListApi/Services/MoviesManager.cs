using Microsoft.EntityFrameworkCore;
using MovieListApi.Infrastructure.Database;
using MovieListApi.Infrastructure.Extensions;
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

    public async Task<(bool success, Movie? entity)> AddGenre(string movieStringId, string genreStringId)
    {
        var id = movieStringId.ToIntId();

        if (id is null)
            return (false, null);

        var movie = await _dbContext.Movies.Include(x => x.Genres).Where(x => x.Id == id).FirstOrDefaultAsync();

        if (movie is null)
            return (false, null);

        var genreId = genreStringId.ToIntId();

        if (genreId is null)
            return (false, movie);

        var genre = await _dbContext.MovieGenres.Where(x => x.Id == genreId).FirstOrDefaultAsync();

        if (genre is null)
            return (false, movie);

        if (movie.Genres.Contains(genre))
            return (false, movie);

        movie.Genres.Add(genre);

        await _dbContext.SaveChangesAsync();

        return (true, movie);
    }

    public async Task<(bool success, Movie? entity)> CreateMovieToWatch(MovieViewModel model)
    {
        var entity = ModelToEntity(model);

        await _dbContext.Movies.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return (true, entity);
    }

    public async Task<(bool success, Movie? entity)> CreateWatchedMovie(MovieViewModel model)
    {
        var entity = ModelToEntity(model);
        entity.Watched = true;

        await _dbContext.Movies.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return (true, entity);
    }

    public async Task<ICollection<Movie>> ListMoviesToWatch()
    {
        var entities = await _dbContext.Movies.Where(x => !x.Watched).ToListAsync();

        return entities;
    }

    public async Task<ICollection<Movie>> ListWatchedMovies()
    {
        var entities = await _dbContext.Movies.Where(x => x.Watched).ToListAsync();

        return entities;
    }

    public async Task<(bool success, Movie? entity)> SetMovieAsWatched(string stringId)
    {
        var id = stringId.ToIntId();

        if (id is null)
            return (false, null);

        var entity = await _dbContext.Movies.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (entity is null)
            return (false, null);

        if (entity.Watched)
            return (false, entity);

        entity.Watched = true;

        await _dbContext.SaveChangesAsync();

        return (true, entity);
    }

    private Movie ModelToEntity(MovieViewModel model)
    {
        var entity = new Movie
        {
            Title = model.Title,
            ReleaseDate = model.ReleaseDate,
            Country = model.Country,
        };

        return entity;
    }
}
