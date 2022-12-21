using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using MovieListApi.Controllers;
using MovieListApi.Models.Entities;
using MovieListApi.Models.Results;
using MovieListApi.Models.ViewModels;
using MovieListApi.Services;
using MovieListApi.Tests.Comparators;
using MovieListApi.Tests.Factories.Entities;
using MovieListApi.Tests.Factories.Services;
using Xunit;

namespace MovieListApi.Tests.Controllers;

public class MovieGenresControllerTest
{
    private readonly Mock<IMovieGenresManager> _manager;
    private readonly MovieGenresController _controller;

    public MovieGenresControllerTest()
    {
        _manager = new Mock<IMovieGenresManager>();
        _controller = new MovieGenresController(_manager.Object);
    }

    [Fact]
    public async Task ShouldList()
    {
        // Given
        var entities = new List<MovieGenre>().Build();
        _manager.MockList(entities: entities);

        // When
        var actionResult = await _controller.List();
        var (successfullyParsed, collectionResult) = actionResult.Parse<ICollection<MovieGenreResult>>();

        // Then
        Assert.True(actionResult.IsOkResult());
        Assert.True(successfullyParsed);
        Assert.Equal(entities.Count, collectionResult?.Count);
        Assert.True(collectionResult?.SequenceEqual(entities.Select(x => new MovieGenreResult(x))));
    }

    [Fact]
    public async Task ShouldAdd()
    {
        // Given
        var viewModel = new MovieGenreViewModel();
        var entity = new MovieGenre().Build();

        _manager.MockCreate(viewModel: viewModel, entity: entity);

        // When
        var actionResult = await _controller.Create(viewModel);
        var (successfullyParsed, contentResult) = actionResult.Parse<MovieGenreResult>();

        // Then
        Assert.True(actionResult.IsOkResult());
        Assert.True(successfullyParsed);
        Assert.True(entity.IsEquivalent(contentResult));
    }

    [Fact]
    public async Task ShouldNotAdd()
    {
        // Given
        var viewModel = new MovieGenreViewModel();
        var entity = new MovieGenre().Build();

        _manager.MockFailureToCreate(viewModel: viewModel);

        // When
        var actionResult = await _controller.Create(viewModel);
        var (successfullyParsed, contentResult) = actionResult.Parse<string>();

        // Then
        Assert.True(actionResult.IsUnprocessableEntityResult());
        Assert.True(successfullyParsed);
        Assert.Contains("failed", contentResult?.ToLower());
    }

    [Fact]
    public async Task ShouldHandleSucessButNoMovieGenreAsAddFailure()
    {
        // Given
        var viewModel = new MovieGenreViewModel();
        var entity = new MovieGenre().Build();

        _manager.MockInternalFailureToCreate(viewModel: viewModel);

        // When
        var actionResult = await _controller.Create(viewModel);
        var (successfullyParsed, contentResult) = actionResult.Parse<string>();

        // Then
        Assert.True(actionResult.IsUnprocessableEntityResult());
        Assert.True(successfullyParsed);
        Assert.Contains("failed", contentResult?.ToLower());
    }
}
