using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieListApi.Controllers;
using MovieListApi.Models.Entities;
using MovieListApi.Models.Results;
using MovieListApi.Services;
using MovieListApi.Tests.Factories.Entities;
using MovieListApi.Tests.Factories.Services;

namespace MovieListApi.Tests.Controllers;

public class MoviesControllerTests
{
    [Fact]
    public async void ShouldListMoviesToWatch()
    {
        // Given
        var movieList = new List<Movie>().Build();
        var manager = new Mock<IMoviesManager>().MockGetMoviesToWatch(movies: movieList);
        var controller = new MoviesController(manager.Object);

        // When
        var actionResult = await controller.GetMoviesToWatch();
        var objectResult = actionResult as ObjectResult;

        var collectionResult = objectResult?.Value as ICollection<MovieResult>;

        // Then
        Assert.IsType<OkObjectResult>(actionResult);
        Assert.IsAssignableFrom<ICollection<MovieResult>>(objectResult?.Value);
        Assert.Equal(movieList.Count, collectionResult?.Count);
        Assert.True(collectionResult?.SequenceEqual(movieList.Select(x => new MovieResult(x))));
    }
}
