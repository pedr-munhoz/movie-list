using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieListApi.Controllers;
using MovieListApi.Models.Entities;
using MovieListApi.Models.Results;
using MovieListApi.Models.ViewModels;
using MovieListApi.Services;
using MovieListApi.Tests.Comparators;
using MovieListApi.Tests.Factories.Entities;
using MovieListApi.Tests.Factories.Services;

namespace MovieListApi.Tests.Controllers;

public class MoviesControllerTests
{
    [Fact]
    public async Task ShouldListMoviesToWatch()
    {
        // Given
        var movieList = new List<Movie>().Build();
        var manager = new Mock<IMoviesManager>().MockGetMoviesToWatch(movies: movieList);
        var controller = new MoviesController(manager.Object);

        // When
        var actionResult = await controller.GetMoviesToWatch();
        var (successfullyParsed, collectionResult) = actionResult.Parse<ICollection<MovieResult>>();

        // Then
        Assert.True(actionResult.IsOkResult());
        Assert.True(successfullyParsed);
        Assert.Equal(movieList.Count, collectionResult?.Count);
        Assert.True(collectionResult?.SequenceEqual(movieList.Select(x => new MovieResult(x))));
    }

    [Fact]
    public async Task ShouldListWatchedMovies()
    {
        // Given
        var movieList = new List<Movie>().Build().Watched();
        var manager = new Mock<IMoviesManager>().MockGetWatchedMovies(movies: movieList);
        var controller = new MoviesController(manager.Object);

        // When
        var actionResult = await controller.GetWatchedMovies();
        var (successfullyParsed, collectionResult) = actionResult.Parse<ICollection<MovieResult>>();

        // Then
        Assert.True(actionResult.IsOkResult());
        Assert.True(successfullyParsed);
        Assert.Equal(movieList.Count, collectionResult?.Count);
        Assert.True(collectionResult?.SequenceEqual(movieList.Select(x => new MovieResult(x))));
    }

    [Fact]
    public async Task ShouldAddMovieToWatch()
    {
        // Given
        var viewModel = new MovieViewModel();
        var entity = new Movie().Build();

        var manager = new Mock<IMoviesManager>()
            .MockAddMovieToWatch(viewModel: viewModel, entity: entity);

        var controller = new MoviesController(manager.Object);

        // When
        var actionResult = await controller.AddMovieToWatch(viewModel);
        var (successfullyParsed, contentResult) = actionResult.Parse<MovieResult>();

        // Then
        Assert.True(actionResult.IsOkResult());
        Assert.True(successfullyParsed);
        Assert.True(entity.IsEquivalent(contentResult));
    }

    [Fact]
    public async Task ShouldNotAddMovieToWatch()
    {
        // Given
        var viewModel = new MovieViewModel();
        var entity = new Movie().Build();

        var manager = new Mock<IMoviesManager>().MockFailureToAddMovieToWatch(viewModel: viewModel);

        var controller = new MoviesController(manager.Object);

        // When
        var actionResult = await controller.AddMovieToWatch(viewModel);
        var (successfullyParsed, contentResult) = actionResult.Parse<string>();

        // Then
        Assert.True(actionResult.IsUnprocessableEntityResult());
        Assert.True(successfullyParsed);
        Assert.Contains("failed", contentResult?.ToLower());
    }

    [Fact]
    public async Task ShouldHandleSucessButNoMovieAsFailure()
    {
        // Given
        var viewModel = new MovieViewModel();
        var entity = new Movie().Build();

        var manager = new Mock<IMoviesManager>().MockInternalFailureToAddMovieToWatch(viewModel: viewModel);

        var controller = new MoviesController(manager.Object);

        // When
        var actionResult = await controller.AddMovieToWatch(viewModel);
        var (successfullyParsed, contentResult) = actionResult.Parse<string>();

        // Then
        Assert.True(actionResult.IsUnprocessableEntityResult());
        Assert.True(successfullyParsed);
        Assert.Contains("failed", contentResult?.ToLower());
    }
}
