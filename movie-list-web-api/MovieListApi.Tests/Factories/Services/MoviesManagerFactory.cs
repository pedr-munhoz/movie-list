using Moq;
using MovieListApi.Models.Entities;
using MovieListApi.Models.ViewModels;
using MovieListApi.Services;

namespace MovieListApi.Tests.Factories.Services;

public static class MoviesManagerFactory
{
    public static Mock<IMoviesManager> MockGetMoviesToWatch(this Mock<IMoviesManager> service, ICollection<Movie> movies)
    {
        service.Setup(x => x.CreateMoviesToWatch()).ReturnsAsync(movies);

        return service;
    }

    public static Mock<IMoviesManager> MockGetWatchedMovies(this Mock<IMoviesManager> service, ICollection<Movie> movies)
    {
        service.Setup(x => x.ListWatchedMovies()).ReturnsAsync(movies);

        return service;
    }

    public static Mock<IMoviesManager> MockAddMovieToWatch(
        this Mock<IMoviesManager> service,
        MovieViewModel viewModel,
        Movie entity)
    {
        service.Setup(x => x.CreateMovieToWatch(viewModel)).ReturnsAsync((true, entity));

        return service;
    }

    public static Mock<IMoviesManager> MockFailureToAddMovieToWatch(
        this Mock<IMoviesManager> service,
        MovieViewModel viewModel)
    {
        service.Setup(x => x.CreateMovieToWatch(viewModel)).ReturnsAsync((false, null));

        return service;
    }

    public static Mock<IMoviesManager> MockInternalFailureToAddMovieToWatch(
        this Mock<IMoviesManager> service,
        MovieViewModel viewModel)
    {
        service.Setup(x => x.CreateMovieToWatch(viewModel)).ReturnsAsync((true, null));

        return service;
    }

    public static Mock<IMoviesManager> MockAddWatchedMovie(
        this Mock<IMoviesManager> service,
        MovieViewModel viewModel,
        Movie entity)
    {
        service.Setup(x => x.CreateWatchedMovie(viewModel)).ReturnsAsync((true, entity));

        return service;
    }

    public static Mock<IMoviesManager> MockFailureToAddWatchedMovie(
        this Mock<IMoviesManager> service,
        MovieViewModel viewModel)
    {
        service.Setup(x => x.CreateWatchedMovie(viewModel)).ReturnsAsync((false, null));

        return service;
    }

    public static Mock<IMoviesManager> MockInternalFailureToAddWatchedMovie(
        this Mock<IMoviesManager> service,
        MovieViewModel viewModel)
    {
        service.Setup(x => x.CreateWatchedMovie(viewModel)).ReturnsAsync((true, null));

        return service;
    }

    public static Mock<IMoviesManager> MockMarkMovieAsWatched(
        this Mock<IMoviesManager> service,
        Movie entity)
    {
        service.Setup(x => x.SetMovieAsWatched(entity.Id.ToString())).ReturnsAsync((true, entity));

        return service;
    }

    public static Mock<IMoviesManager> MockFailureToMarkMovieAsWatched(
        this Mock<IMoviesManager> service,
        Movie entity)
    {
        service.Setup(x => x.SetMovieAsWatched(entity.Id.ToString())).ReturnsAsync((false, null));

        return service;
    }

    public static Mock<IMoviesManager> MockInternalFailureToMarkMovieAsWatched(
        this Mock<IMoviesManager> service,
        Movie entity)
    {
        service.Setup(x => x.SetMovieAsWatched(entity.Id.ToString())).ReturnsAsync((true, null));

        return service;
    }
}
