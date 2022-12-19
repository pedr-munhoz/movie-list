using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using MovieListApi.Models.Entities;
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
}
