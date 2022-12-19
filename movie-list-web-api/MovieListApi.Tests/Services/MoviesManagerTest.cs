using Microsoft.EntityFrameworkCore;
using MovieListApi.Infrastructure.Database;
using MovieListApi.Models.Entities;
using MovieListApi.Services;
using MovieListApi.Tests.Factories.Entities;
using MovieListApi.Tests.Factories.Infrastructure;

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
}
