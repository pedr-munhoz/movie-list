using Moq;
using MovieListApi.Infrastructure.Extensions;
using MovieListApi.Models.Entities;
using MovieListApi.Models.ViewModels;
using MovieListApi.Services;

namespace MovieListApi.Tests.Factories.Services;

public static class MoviesManagerFactory
{
    public static Mock<IMoviesManager> MockListMoviesToWatch(
        this Mock<IMoviesManager> service,
        ICollection<Movie> movies,
        ListMoviesViewModel model)
    {
        service.Setup(x => x.ListMoviesToWatch(model)).ReturnsAsync(movies);

        return service;
    }

    public static Mock<IMoviesManager> MockListWatchedMovies(
        this Mock<IMoviesManager> service,
        ICollection<Movie> movies,
        ListMoviesViewModel model)
    {
        service.Setup(x => x.ListWatchedMovies(model)).ReturnsAsync(movies);

        return service;
    }

    public static Mock<IMoviesManager> MockCreateMovieToWatch(
        this Mock<IMoviesManager> service,
        MovieViewModel viewModel,
        Movie entity)
    {
        service.Setup(x => x.CreateMovieToWatch(viewModel)).ReturnsAsync((true, entity));

        return service;
    }

    public static Mock<IMoviesManager> MockFailureToCreateMovieToWatch(
        this Mock<IMoviesManager> service,
        MovieViewModel viewModel)
    {
        service.Setup(x => x.CreateMovieToWatch(viewModel)).ReturnsAsync((false, null));

        return service;
    }

    public static Mock<IMoviesManager> MockInternalFailureToCreateMovieToWatch(
        this Mock<IMoviesManager> service,
        MovieViewModel viewModel)
    {
        service.Setup(x => x.CreateMovieToWatch(viewModel)).ReturnsAsync((true, null));

        return service;
    }

    public static Mock<IMoviesManager> MockCreateWatchedMovie(
        this Mock<IMoviesManager> service,
        MovieViewModel viewModel,
        Movie entity)
    {
        service.Setup(x => x.CreateWatchedMovie(viewModel)).ReturnsAsync((true, entity));

        return service;
    }

    public static Mock<IMoviesManager> MockFailureToCreateWatchedMovie(
        this Mock<IMoviesManager> service,
        MovieViewModel viewModel)
    {
        service.Setup(x => x.CreateWatchedMovie(viewModel)).ReturnsAsync((false, null));

        return service;
    }

    public static Mock<IMoviesManager> MockInternalFailureToCreateWatchedMovie(
        this Mock<IMoviesManager> service,
        MovieViewModel viewModel)
    {
        service.Setup(x => x.CreateWatchedMovie(viewModel)).ReturnsAsync((true, null));

        return service;
    }

    public static Mock<IMoviesManager> MockSetMovieAsWatched(
        this Mock<IMoviesManager> service,
        Movie entity)
    {
        service.Setup(x => x.SetMovieAsWatched(entity.Id.ToStringId())).ReturnsAsync((true, entity));

        return service;
    }

    public static Mock<IMoviesManager> MockFailureSetMovieAsWatched(
        this Mock<IMoviesManager> service,
        Movie entity)
    {
        service.Setup(x => x.SetMovieAsWatched(entity.Id.ToStringId())).ReturnsAsync((false, null));

        return service;
    }

    public static Mock<IMoviesManager> MockInternalFailureToSetMovieAsWatched(
        this Mock<IMoviesManager> service,
        Movie entity)
    {
        service.Setup(x => x.SetMovieAsWatched(entity.Id.ToStringId())).ReturnsAsync((true, null));

        return service;
    }

    public static Mock<IMoviesManager> MockAddGenre(
        this Mock<IMoviesManager> service,
        Movie movie,
        MovieGenre genre)
    {
        service
            .Setup(x => x.AddGenre(movie.Id.ToStringId(), genre.Id.ToStringId()))
            .ReturnsAsync((true, movie));

        return service;
    }

    public static Mock<IMoviesManager> MockFailureAddGenre(
        this Mock<IMoviesManager> service,
        string movieId,
        string genreId)
    {
        service
            .Setup(x => x.AddGenre(movieId, genreId))
            .ReturnsAsync((false, null));

        return service;
    }

    public static Mock<IMoviesManager> MockAddGenreNoMovieReturn(
        this Mock<IMoviesManager> service,
        string movieId,
        string genreId)
    {
        service
            .Setup(x => x.AddGenre(movieId, genreId))
            .ReturnsAsync((true, null));

        return service;
    }
}
