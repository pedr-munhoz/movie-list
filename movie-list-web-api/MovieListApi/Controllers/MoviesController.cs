using Microsoft.AspNetCore.Mvc;
using MovieListApi.Models.Results;
using MovieListApi.Services;

namespace MovieListApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMoviesManager _moviesManager;

    public MoviesController(IMoviesManager moviesManager)
    {
        _moviesManager = moviesManager;
    }

    [HttpGet]
    [Route("to-watch")]
    public async Task<IActionResult> GetMoviesToWatch()
    {
        var movies = await _moviesManager.GetMoviesToWatch();
        return Ok(movies.Select(x => new MovieResult(x)).ToList());
    }

    [HttpGet]
    [Route("to-watch")]
    public async Task<IActionResult> GetWatchedMovies()
    {
        var movies = await _moviesManager.GetWatchedMovies();
        return Ok(movies.Select(x => new MovieResult(x)).ToList());
    }
}
