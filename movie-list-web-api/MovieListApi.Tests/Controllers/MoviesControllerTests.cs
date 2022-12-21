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
        _manager.MockListMoviesToWatch(movies: movieList);

        // When
        var actionResult = await _controller.ListMoviesToWatch();
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
        _manager.MockListWatchedMovies(movies: movieList);

        // When
        var actionResult = await _controller.ListWatchedMovies();
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

        _manager.MockCreateMovieToWatch(viewModel: viewModel, entity: entity);

        // When
        var actionResult = await _controller.CreateMovieToWatch(viewModel);
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

        _manager.MockFailureToCreateMovieToWatch(viewModel: viewModel);

        // When
        var actionResult = await _controller.CreateMovieToWatch(viewModel);
        var (successfullyParsed, contentResult) = actionResult.Parse<string>();

        // Then
        Assert.True(actionResult.IsUnprocessableEntityResult());
        Assert.True(successfullyParsed);
        Assert.Contains("failed", contentResult?.ToLower());
    }

    [Fact]
    public async Task ShouldHandleSucessButNoMovieAsAddFailure()
    {
        // Given
        var viewModel = new MovieViewModel();
        var entity = new Movie().Build();

        _manager.MockInternalFailureToCreateMovieToWatch(viewModel: viewModel);

        // When
        var actionResult = await _controller.CreateMovieToWatch(viewModel);
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

        _manager.MockCreateWatchedMovie(viewModel: viewModel, entity: entity);

        // When
        var actionResult = await _controller.CreateWatchedMovie(viewModel);
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

        _manager.MockFailureToCreateWatchedMovie(viewModel: viewModel);

        // When
        var actionResult = await _controller.CreateWatchedMovie(viewModel);
        var (successfullyParsed, contentResult) = actionResult.Parse<string>();

        // Then
        Assert.True(actionResult.IsUnprocessableEntityResult());
        Assert.True(successfullyParsed);
        Assert.Contains("failed", contentResult?.ToLower());
    }

    [Fact]
    public async Task ShouldHandleSucessButNoWatchedMovieAsAddFailure()
    {
        // Given
        var viewModel = new MovieViewModel();
        var entity = new Movie().Build();

        _manager.MockInternalFailureToCreateWatchedMovie(viewModel: viewModel);

        // When
        var actionResult = await _controller.CreateWatchedMovie(viewModel);
        var (successfullyParsed, contentResult) = actionResult.Parse<string>();

        // Then
        Assert.True(actionResult.IsUnprocessableEntityResult());
        Assert.True(successfullyParsed);
        Assert.Contains("failed", contentResult?.ToLower());
    }

    [Fact]
    public async void SholdMarkMovieAsWatched()
    {
        // Given
        var entity = new Movie().Build();

        _manager.MockSetMovieAsWatched(entity);

        // When
        var actionResult = await _controller.SetMovieAsWatched(id: entity.Id.ToString());
        var (successfullyParsed, contentResult) = actionResult.Parse<MovieResult>();

        // Then
        Assert.True(actionResult.IsOkResult());
        Assert.True(successfullyParsed);
        Assert.True(entity.IsEquivalent(contentResult));
    }

    [Fact]
    public async Task ShouldNotMarkMovieAsWatched()
    {
        // Given
        var entity = new Movie().Build();

        _manager.MockFailureSetMovieAsWatched(entity);

        // When
        var actionResult = await _controller.SetMovieAsWatched(id: entity.Id.ToString());
        var (successfullyParsed, contentResult) = actionResult.Parse<string>();

        // Then
        Assert.True(actionResult.IsUnprocessableEntityResult());
        Assert.True(successfullyParsed);
        Assert.Contains("failed", contentResult?.ToLower());
    }

    [Fact]
    public async Task ShouldHandleSucessButNoWatchedMovieAsMarkFailure()
    {
        // Given
        var entity = new Movie().Build();

        _manager.MockInternalFailureToSetMovieAsWatched(entity);

        // When
        var actionResult = await _controller.SetMovieAsWatched(id: entity.Id.ToString());
        var (successfullyParsed, contentResult) = actionResult.Parse<string>();

        // Then
        Assert.True(actionResult.IsUnprocessableEntityResult());
        Assert.True(successfullyParsed);
        Assert.Contains("failed", contentResult?.ToLower());
    }
}
