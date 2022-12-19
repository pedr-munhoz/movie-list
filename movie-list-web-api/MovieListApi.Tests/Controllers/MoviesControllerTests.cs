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
    private readonly Mock<IMoviesManager> _manager;
    private readonly MoviesController _controller;

    public MoviesControllerTests()
    {
        _manager = new Mock<IMoviesManager>();
        _controller = new MoviesController(_manager.Object);
    }

    [Fact]
    public async Task ShouldListMoviesToWatch()
    {
        // Given
        var movieList = new List<Movie>().Build();
        _manager.MockGetMoviesToWatch(movies: movieList);

        // When
        var actionResult = await _controller.GetMoviesToWatch();
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
        _manager.MockGetWatchedMovies(movies: movieList);

        // When
        var actionResult = await _controller.GetWatchedMovies();
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

        _manager.MockAddMovieToWatch(viewModel: viewModel, entity: entity);

        // When
        var actionResult = await _controller.AddMovieToWatch(viewModel);
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

        _manager.MockFailureToAddMovieToWatch(viewModel: viewModel);

        // When
        var actionResult = await _controller.AddMovieToWatch(viewModel);
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

        _manager.MockInternalFailureToAddMovieToWatch(viewModel: viewModel);

        // When
        var actionResult = await _controller.AddMovieToWatch(viewModel);
        var (successfullyParsed, contentResult) = actionResult.Parse<string>();

        // Then
        Assert.True(actionResult.IsUnprocessableEntityResult());
        Assert.True(successfullyParsed);
        Assert.Contains("failed", contentResult?.ToLower());
    }



    [Fact]
    public async Task ShouldAddWatchedMovie()
    {
        // Given
        var viewModel = new MovieViewModel();
        var entity = new Movie().Build();

        _manager.MockAddWatchedMovie(viewModel: viewModel, entity: entity);

        // When
        var actionResult = await _controller.AddWatchedMovie(viewModel);
        var (successfullyParsed, contentResult) = actionResult.Parse<MovieResult>();

        // Then
        Assert.True(actionResult.IsOkResult());
        Assert.True(successfullyParsed);
        Assert.True(entity.IsEquivalent(contentResult));
    }

    [Fact]
    public async Task ShouldNotAddWatchedMovie()
    {
        // Given
        var viewModel = new MovieViewModel();
        var entity = new Movie().Build();

        _manager.MockFailureToAddWatchedMovie(viewModel: viewModel);

        // When
        var actionResult = await _controller.AddWatchedMovie(viewModel);
        var (successfullyParsed, contentResult) = actionResult.Parse<string>();

        // Then
        Assert.True(actionResult.IsUnprocessableEntityResult());
        Assert.True(successfullyParsed);
        Assert.Contains("failed", contentResult?.ToLower());
    }

    [Fact]
    public async Task ShouldHandleSucessButNoWatchedMovieAsFailure()
    {
        // Given
        var viewModel = new MovieViewModel();
        var entity = new Movie().Build();

        _manager.MockInternalFailureToAddWatchedMovie(viewModel: viewModel);

        // When
        var actionResult = await _controller.AddWatchedMovie(viewModel);
        var (successfullyParsed, contentResult) = actionResult.Parse<string>();

        // Then
        Assert.True(actionResult.IsUnprocessableEntityResult());
        Assert.True(successfullyParsed);
        Assert.Contains("failed", contentResult?.ToLower());
    }
}
