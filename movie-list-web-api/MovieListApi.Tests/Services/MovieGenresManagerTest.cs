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

    [Theory]
    [InlineData(0, 11)]
    [InlineData(3, 7)]
    public async Task List_WhenCalledWithOffset_ReturnsCorrectItemCount(int index, int length)
    {
        // Given
        var skipped = new List<MovieGenre>().Build(count: index);
        var selected = new List<MovieGenre>().Build(count: length);
        var leftover = new List<MovieGenre>().Build();

        await _dbContext.MovieGenres.AddRangeAsync(skipped);
        await _dbContext.MovieGenres.AddRangeAsync(selected);
        await _dbContext.MovieGenres.AddRangeAsync(leftover);

        await _dbContext.SaveChangesAsync();

        var offset = new OffsetViewModel { Index = index, Length = length };

        // When
        var result = await _manager.List(offset);

        // Then
        Assert.Equal(length, result.Count);
        Assert.True(result.SequenceEqual(selected));
    }

    [Fact]
    public async Task List_WhenCalledWithoutOffset_ReturnsBaseLength()
    {
        // Given
        var length = MovieGenresManager.BaseLength;

        var selected = new List<MovieGenre>().Build(count: length);
        var leftover = new List<MovieGenre>().Build();

        await _dbContext.MovieGenres.AddRangeAsync(selected);
        await _dbContext.MovieGenres.AddRangeAsync(leftover);

        await _dbContext.SaveChangesAsync();

        var offset = new OffsetViewModel { Index = null, Length = null };

        // When
        var result = await _manager.List(offset);

        // Then
        Assert.Equal(length, result.Count);
        Assert.True(result.SequenceEqual(selected));
    }

    [Fact]
    public async Task Create_WhenCalled_AddsMovieGenreToDbAndReturnsIt()
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
