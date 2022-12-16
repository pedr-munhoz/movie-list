using System.Net;
using Microsoft.AspNetCore.Mvc;
using MovieListApi.Controllers;

namespace MovieListApi.Tests.Controllers;

public class MoviesControllerTests
{
    [Fact]
    public async void ShouldListMoviesToWatch()
    {
        // Given
        var controller = new MoviesController();

        // When
        var result = await controller.GetMoviesToWatch();

        // Then
        Assert.IsType<OkResult>(result);
    }
}
