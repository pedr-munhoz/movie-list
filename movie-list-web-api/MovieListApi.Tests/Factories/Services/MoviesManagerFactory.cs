using Moq;
using MovieListApi.Models.Entities;
using MovieListApi.Models.ViewModels;
using MovieListApi.Services;

namespace MovieListApi.Tests.Factories.Services;

public static class MoviesManagerFactory
{
    public static Mock<IMoviesManager> MockGetMoviesToWatch(this Mock<IMoviesManager> service, ICollection<Movie> movies)
    {
        service.Setup(x => x.GetMoviesToWatch()).ReturnsAsync(movies);

        return service;
    }

    public static Mock<IMoviesManager> MockGetWatchedMovies(this Mock<IMoviesManager> service, ICollection<Movie> movies)
    {
        service.Setup(x => x.GetWatchedMovies()).ReturnsAsync(movies);

        return service;
    }

    public static Mock<IMoviesManager> MockAddMovieToWatch(
        this Mock<IMoviesManager> service,
        MovieViewModel viewModel,
        Movie entity)
    {
        service.Setup(x => x.AddMovieToWatch(viewModel)).ReturnsAsync((true, entity));

        return service;
    }

    public static Mock<IMoviesManager> MockFailureToAddMovieToWatch(
        this Mock<IMoviesManager> service,
        MovieViewModel viewModel)
    {
        service.Setup(x => x.AddMovieToWatch(viewModel)).ReturnsAsync((false, null));

        return service;
    }

    public static Mock<IMoviesManager> MockInternalFailureToAddMovieToWatch(
        this Mock<IMoviesManager> service,
        MovieViewModel viewModel)
    {
        service.Setup(x => x.AddMovieToWatch(viewModel)).ReturnsAsync((true, null));

        return service;
    }
}
