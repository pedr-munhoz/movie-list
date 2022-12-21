using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieListApi.Infrastructure.Database;
using MovieListApi.Models.Entities;
using MovieListApi.Models.ViewModels;
using MovieListApi.Services;
using MovieListApi.Tests.Comparators;
using MovieListApi.Tests.Factories.Entities;
using MovieListApi.Tests.Factories.Infrastructure;
using MovieListApi.Tests.Factories.ViewModels;
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
        var result = await _manager.List();

        // Then
        Assert.Equal(entities.Count, result.Count);
        Assert.True(result.SequenceEqual(entities));
    }

    [Fact]
    public async Task ShouldCreateMovieGenre()
    {
        // Given
        var viewModel = new MovieGenreViewModel().Build();

        // When
        var (success, result) = await _manager.Create(viewModel);
        var entities = await _dbContext.MovieGenres.ToListAsync();

        // Then
        Assert.True(success);
        Assert.True(viewModel.IsEquivalent(result));
        Assert.Single(entities);
        Assert.Equal(entities.First(), result);
    }
}
