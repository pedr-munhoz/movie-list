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
    private readonly IMoviesManager _manager;
    private readonly MoviesDbContext _dbContext;

    public MoviesManagerTest()
    {
        _dbContext = MockDbContextFactory.BuildInMemory<MoviesDbContext>();
        _manager = new MoviesManager(_dbContext);
    }

    [Fact]
    public async Task ListMoviesToWatch_WhenCalled_ReturnsMoviesToWatchFromDb()
    {
        // Given
        var entities = new List<Movie>().Build();
        await _dbContext.Movies.AddRangeAsync(entities);

        var entity = new Movie().Build().Watched();
        await _dbContext.Movies.AddAsync(entity);

        await _dbContext.SaveChangesAsync();

        // When
        var result = await _manager.ListMoviesToWatch();

        // Then
        Assert.Equal(entities.Count, result.Count);
        Assert.True(result.SequenceEqual(entities));
    }

    [Fact]
    public async Task ListWatchedMovies_WhenCalled_ReturnsWatchedMoviesFromDb()
    {
        // Given
        var entities = new List<Movie>().Build().Watched();
        await _dbContext.Movies.AddRangeAsync(entities);

        var movie = new Movie().Build();
        await _dbContext.Movies.AddAsync(movie);

        await _dbContext.SaveChangesAsync();

        // When
        var result = await _manager.ListWatchedMovies();

        // Then
        Assert.Equal(entities.Count, result.Count);
        Assert.True(result.SequenceEqual(entities));
    }

    [Fact]
    public async Task CreateMovieToWatch_WhenCalled_AddsMovieToDbAndReturnsIt()
    {
        // Given
        var viewModel = new MovieViewModel().Build();

        // When
        var (success, result) = await _manager.CreateMovieToWatch(viewModel);
        var entities = await _dbContext.Movies.ToListAsync();

        // Then
        Assert.True(success);
        Assert.True(viewModel.IsEquivalent(result));
        Assert.Single(entities);
        Assert.Equal(entities.First(), result);
    }

    [Fact]
    public async Task CreateWatchedMovie_WhenCalled_AddsMovieToDbAndReturnsIt()
    {
        // Given
        var viewModel = new MovieViewModel().Build();

        // When
        var (success, result) = await _manager.CreateWatchedMovie(viewModel);
        var entities = await _dbContext.Movies.ToListAsync();

        // Then
        Assert.True(success);
        Assert.True(viewModel.IsEquivalent(result));
        Assert.True(result?.Watched);
        Assert.Single(entities);
        Assert.Equal(entities.First(), result);
    }

    [Fact]
    public async Task SetMovieAsWatched_WhenCalledWithExistingMovieToWatchId_SetsMovieAsWatchedAndReturnsIt()
    {
        // Given
        var entity = new Movie().Build();
        await _dbContext.Movies.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        // When
        var (success, result) = await _manager.SetMovieAsWatched(stringId: entity.Id.ToString());
        await _dbContext.Entry(entity).ReloadAsync();

        // Then
        Assert.True(success);
        Assert.True(result?.Watched);
        Assert.True(entity?.Watched);
    }

    [Fact]
    public async Task SetMovieAsWatched_WhenCalledWithInvalidId_ReturnsFailure()
    {
        // Given

        // When
        var (success, result) = await _manager.SetMovieAsWatched(stringId: Guid.NewGuid().ToString());

        // Then
        Assert.False(success);
        Assert.Null(result);
    }

    [Fact]
    public async Task SetMovieAsWatched_WhenCalledWithUnknownId_ReturnsFailure()
    {
        // Given

        // When
        var (success, result) = await _manager.SetMovieAsWatched(stringId: new Random().Next().ToString());

        // Then
        Assert.False(success);
        Assert.Null(result);
    }

    [Fact]
    public async Task SetMovieAsWatched_WhenCalledWithExistingWatchedMovieId_ReturnsFailureWithMovie()
    {
        // Given
        var entity = new Movie().Build().Watched();
        await _dbContext.Movies.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        // When
        var (success, result) = await _manager.SetMovieAsWatched(stringId: entity.Id.ToString());
        await _dbContext.Entry(entity).ReloadAsync();

        // Then
        Assert.False(success);
        Assert.NotNull(result);
        Assert.Equal(entity, result);
    }
}
