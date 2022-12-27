using Moq;
using MovieListApi.Controllers;
using MovieListApi.Infrastructure.Extensions;
using MovieListApi.Models.Entities;
using MovieListApi.Models.Results;
using MovieListApi.Models.ViewModels;
using MovieListApi.Services;
using MovieListApi.Tests.Comparators;
using MovieListApi.Tests.Factories.Entities;
using MovieListApi.Tests.Factories.Services;
using MovieListApi.Tests.Factories.ViewModels;

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
    public async Task ListMoviesToWatch_WhenCalled_ReturnsListOfMoviesToWatch()
    {
        // Given
        var movieList = new List<Movie>().Build();
        var offset = new OffsetViewModel().Build();

        _manager.MockListMoviesToWatch(movies: movieList, offset: offset);

        // When
        var actionResult = await _controller.ListMoviesToWatch(offset);
        var (successfullyParsed, collectionResult) = actionResult.Parse<ICollection<MovieResult>>();

        // Then
        Assert.True(actionResult.IsOkResult());
        Assert.True(successfullyParsed);
        Assert.Equal(movieList.Count, collectionResult?.Count);
        Assert.True(collectionResult?.SequenceEqual(movieList.Select(x => new MovieResult(x))));
    }

    [Fact]
    public async Task ListWatchedMovies_WhenCalled_ReturnsListOfWatchedMovies()
    {
        // Given
        var movieList = new List<Movie>().Build().Watched();
        var offset = new OffsetViewModel().Build();

        _manager.MockListWatchedMovies(movies: movieList, offset: offset);

        // When
        var actionResult = await _controller.ListWatchedMovies(offset);
        var (successfullyParsed, collectionResult) = actionResult.Parse<ICollection<MovieResult>>();

        // Then
        Assert.True(actionResult.IsOkResult());
        Assert.True(successfullyParsed);
        Assert.Equal(movieList.Count, collectionResult?.Count);
        Assert.True(collectionResult?.SequenceEqual(movieList.Select(x => new MovieResult(x))));
    }

    [Fact]
    public async Task CreateMovieToWatch_WhenOperationSucedes_ReturnsCreatedMovie()
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
    public async Task CreateMovieToWatch_WhenOperationFails_ReturnsErrorMessage()
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
    public async Task CreateMovieToWatch_WhenOperationReturnsNoMovie_ReturnsErrorMessage()
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
    public async Task CreateWatchedMovie_WhenOperationSucedes_ReturnsCreatedMovie()
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
    public async Task CreateWatchedMovie_WhenOperationFails_ReturnsErrorMessage()
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
    public async Task CreateWatchedMovie_WhenOperationReturnsNoMovie_ReturnsErrorMessage()
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
    public async void SetMovieAsWatched_WhenOperationSucedes_ReturnsMovie()
    {
        // Given
        var entity = new Movie().Build();

        _manager.MockSetMovieAsWatched(entity);

        // When
        var actionResult = await _controller.SetMovieAsWatched(id: entity.Id.ToStringId());
        var (successfullyParsed, contentResult) = actionResult.Parse<MovieResult>();

        // Then
        Assert.True(actionResult.IsOkResult());
        Assert.True(successfullyParsed);
        Assert.True(entity.IsEquivalent(contentResult));
    }

    [Fact]
    public async Task SetMovieAsWatched_WhenOperationFails_ReturnsErrorMessage()
    {
        // Given
        var entity = new Movie().Build();

        _manager.MockFailureSetMovieAsWatched(entity);

        // When
        var actionResult = await _controller.SetMovieAsWatched(id: entity.Id.ToStringId());
        var (successfullyParsed, contentResult) = actionResult.Parse<string>();

        // Then
        Assert.True(actionResult.IsUnprocessableEntityResult());
        Assert.True(successfullyParsed);
        Assert.Contains("failed", contentResult?.ToLower());
    }

    [Fact]
    public async Task SetMovieAsWatched_WhenOperationReturnsNoMovie_ReturnsErrorMessage()
    {
        // Given
        var entity = new Movie().Build();

        _manager.MockInternalFailureToSetMovieAsWatched(entity);

        // When
        var actionResult = await _controller.SetMovieAsWatched(id: entity.Id.ToStringId());
        var (successfullyParsed, contentResult) = actionResult.Parse<string>();

        // Then
        Assert.True(actionResult.IsUnprocessableEntityResult());
        Assert.True(successfullyParsed);
        Assert.Contains("failed", contentResult?.ToLower());
    }

    [Fact]
    public async Task AddGenre_WhenOperationSucedes_ReturnMovie()
    {
        // Given
        var genre = new MovieGenre().Build();
        var movie = new Movie().Build().WithGenre(genre);

        _manager.MockAddGenre(movie, genre);

        // When
        var actionResult = await _controller.AddGenreToMovie(id: movie.Id.ToStringId(), genreId: genre.Id.ToStringId());
        var (successfullyParsed, contentResult) = actionResult.Parse<MovieResult>();

        // Then
        Assert.True(actionResult.IsOkResult());
        Assert.True(successfullyParsed);
        Assert.True(movie.IsEquivalent(contentResult));
    }

    [Fact]
    public async Task AddGenre_WhenOperationFails_ReturnErrorMessage()
    {
        // Given
        var movieId = Guid.NewGuid().ToString();
        var genreId = Guid.NewGuid().ToString();

        _manager.MockFailureAddGenre(movieId: movieId, genreId: genreId);

        // When
        var actionResult = await _controller.AddGenreToMovie(id: movieId, genreId: genreId);
        var (successfullyParsed, contentResult) = actionResult.Parse<string>();

        // Then
        Assert.True(actionResult.IsUnprocessableEntityResult());
        Assert.True(successfullyParsed);
        Assert.Contains("failed", contentResult?.ToLower());
    }

    [Fact]
    public async Task AddGenre_WhenOperationReturnsNoMovie_ReturnErrorMessage()
    {
        // Given
        var movieId = Guid.NewGuid().ToString();
        var genreId = Guid.NewGuid().ToString();

        _manager.MockAddGenreNoMovieReturn(movieId: movieId, genreId: genreId);

        // When
        var actionResult = await _controller.AddGenreToMovie(id: movieId, genreId: genreId);
        var (successfullyParsed, contentResult) = actionResult.Parse<string>();

        // Then
        Assert.True(actionResult.IsUnprocessableEntityResult());
        Assert.True(successfullyParsed);
        Assert.Contains("failed", contentResult?.ToLower());
    }
}
