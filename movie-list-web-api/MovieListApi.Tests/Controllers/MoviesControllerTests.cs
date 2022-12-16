using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieListApi.Controllers;
using MovieListApi.Entities;
using MovieListApi.Services;

namespace MovieListApi.Tests.Controllers;

public class MoviesControllerTests
{
    [Fact]
    public async void ShouldListMoviesToWatch()
    {
        // Given
        var manager = new Mock<IMoviesManager>();

        var movieList = new List<Movie>();

        movieList.Add(new Movie { Name = "Movie1" });
        movieList.Add(new Movie { Name = "Movie2" });
        movieList.Add(new Movie { Name = "Movie3" });

        manager.Setup(x => x.GetMoviesToWatch()).ReturnsAsync(movieList);

        var controller = new MoviesController(manager.Object);

        // When
        var actionResult = await controller.GetMoviesToWatch();
        var objectResult = actionResult as ObjectResult;

        var collectionResult = objectResult?.Value as ICollection<Movie>;

        // Then
        Assert.IsType<OkObjectResult>(actionResult);
        Assert.IsAssignableFrom<ICollection<Movie>>(objectResult?.Value);
        Assert.Equal(movieList.Count, collectionResult?.Count);
    }
}
