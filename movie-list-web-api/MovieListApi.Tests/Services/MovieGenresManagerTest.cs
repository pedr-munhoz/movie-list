using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieListApi.Infrastructure.Database;
using MovieListApi.Models.Entities;
using MovieListApi.Services;
using MovieListApi.Tests.Factories.Entities;
using MovieListApi.Tests.Factories.Infrastructure;
using Xunit;

namespace MovieListApi.Tests.Services;

public class MovieGenresManagerTest
{
    private readonly IMovieGenresManager _manager;
    private readonly MoviesDbContext _dbContext;

    public MovieGenresManagerTest()
    {
        _dbContext = MockDbContextFactory.BuildInMemory<MoviesDbContext>();
        _manager = new MovieGenresManager(_dbContext);
    }

    [Fact]
    public async Task ShouldListMovieGenres()
    {
        // Given
        var entities = new List<MovieGenre>().Build();
        await _dbContext.MovieGenres.AddRangeAsync(entities);

        await _dbContext.SaveChangesAsync();

        // When
        var result = await _manager.GetMovieGenres();

        // Then
        Assert.Equal(entities.Count, result.Count);
        Assert.True(result.SequenceEqual(entities));
    }

    [Fact]
    public void ShouldCreateMovieGenre()
    {
        // Given

        // When

        // Then
        Assert.True(false);
    }
}
