using Microsoft.EntityFrameworkCore;
using MovieListApi.Infrastructure.Database;
using MovieListApi.Models.Entities;
using MovieListApi.Models.ViewModels;
using MovieListApi.Services;
using MovieListApi.Tests.Comparators;
using MovieListApi.Tests.Factories.Entities;
using MovieListApi.Tests.Factories.Infrastructure;
using MovieListApi.Tests.Factories.ViewModels;

namespace MovieListApi.Tests.Services;

public class MoviesManagerTest
{
    private readonly MoviesManager _manager;
    private readonly MoviesDbContext _dbContext;

    public MoviesManagerTest()
    {
        _dbContext = MockDbContextFactory.BuildInMemory<MoviesDbContext>();
        _manager = new MoviesManager(_dbContext);
    }

    [Fact]
    public async Task ShouldGetMoviesToWatch()
    {
        // Given
        var movieList = new List<Movie>().Build();
        await _dbContext.Movies.AddRangeAsync(movieList);

        var movie = new Movie().Build().Watched();
        await _dbContext.Movies.AddAsync(movie);

        await _dbContext.SaveChangesAsync();

        // When
        var result = await _manager.GetMoviesToWatch();

        // Then
        Assert.Equal(movieList.Count, result.Count);
        Assert.True(result.SequenceEqual(movieList));
    }

    [Fact]
    public async Task ShouldGetWatchedMovies()
    {
        // Given
        var movieList = new List<Movie>().Build().Watched();
        await _dbContext.Movies.AddRangeAsync(movieList);

        var movie = new Movie().Build();
        await _dbContext.Movies.AddAsync(movie);

        await _dbContext.SaveChangesAsync();

        // When
        var result = await _manager.GetWatchedMovies();

        // Then
        Assert.Equal(movieList.Count, result.Count);
        Assert.True(result.SequenceEqual(movieList));
    }

    [Fact]
    public async Task ShouldAddMovieToWatch()
    {
        // Given
        var viewModel = new MovieViewModel().Build();

        // When
        var (success, result) = await _manager.AddMovieToWatch(viewModel);
        var entities = await _dbContext.Movies.ToListAsync();

        // Then
        Assert.True(success);
        Assert.True(viewModel.IsEquivalent(result));
        Assert.Single(entities);
        Assert.Equal(entities.First(), result);
    }

    [Fact]
    public async Task ShouldAddWatchedMovie()
    {
        // Given
        var viewModel = new MovieViewModel().Build();

        // When
        var (success, result) = await _manager.AddWatchedMovie(viewModel);
        var entities = await _dbContext.Movies.ToListAsync();

        // Then
        Assert.True(success);
        Assert.True(viewModel.IsEquivalent(result));
        Assert.True(result?.Watched);
        Assert.Single(entities);
        Assert.Equal(entities.First(), result);
    }

    [Fact]
    public async Task SholdMarkMovieAsWatched()
    {
        // Given
        var movie = new Movie().Build();
        await _dbContext.Movies.AddAsync(movie);
        await _dbContext.SaveChangesAsync();

        // When
        var (success, result) = await _manager.MarkMovieAsWatched(stringId: movie.Id.ToString());
        await _dbContext.Entry(movie).ReloadAsync();

        // Then
        Assert.True(success);
        Assert.True(result?.Watched);
        Assert.True(movie?.Watched);
    }

    [Fact]
    public async Task SholdNotMarkMovieAsWatchedInvalidId()
    {
        // Given

        // When
        var (success, result) = await _manager.MarkMovieAsWatched(stringId: Guid.NewGuid().ToString());

        // Then
        Assert.False(success);
        Assert.Null(result);
    }

    [Fact]
    public async Task SholdNotMarkMovieAsWatchedNotFound()
    {
        // Given

        // When
        var (success, result) = await _manager.MarkMovieAsWatched(stringId: new Random().Next().ToString());

        // Then
        Assert.False(success);
        Assert.Null(result);
    }

    [Fact]
    public async Task SholdNotMarkMovieAsWatchedAlreadyWatched()
    {
        // Given
        var movie = new Movie().Build().Watched();
        await _dbContext.Movies.AddAsync(movie);
        await _dbContext.SaveChangesAsync();

        // When
        var (success, result) = await _manager.MarkMovieAsWatched(stringId: movie.Id.ToString());
        await _dbContext.Entry(movie).ReloadAsync();

        // Then
        Assert.False(success);
        Assert.NotNull(result);
        Assert.Equal(movie, result);
    }
}
