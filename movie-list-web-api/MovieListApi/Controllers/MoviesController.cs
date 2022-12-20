using Microsoft.AspNetCore.Mvc;
using MovieListApi.Models.Results;
using MovieListApi.Models.ViewModels;
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
    [Route("watched")]
    public async Task<IActionResult> GetWatchedMovies()
    {
        var movies = await _moviesManager.GetWatchedMovies();
        return Ok(movies.Select(x => new MovieResult(x)).ToList());
    }

    [HttpPost]
    [Route("to-watch")]
    public async Task<IActionResult> AddMovieToWatch([FromBody] MovieViewModel model)
    {
        var (sucess, movie) = await _moviesManager.AddMovieToWatch(model);

        if (!sucess || movie is null)
            return UnprocessableEntity("Failed to add movie to watch");

        return Ok(new MovieResult(movie));
    }

    [HttpPost]
    [Route("watched")]
    public async Task<IActionResult> AddWatchedMovie([FromBody] MovieViewModel model)
    {
        var (sucess, movie) = await _moviesManager.AddWatchedMovie(model);

        if (!sucess || movie is null)
            return UnprocessableEntity("Failed to add watched movie");

        return Ok(new MovieResult(movie));
    }

    [HttpPatch]
    [Route("{id}/watched")]
    public async Task<IActionResult> MarkMovieAsWatched([FromRoute] string id)
    {
        var (sucess, movie) = await _moviesManager.MarkMovieAsWatched(id);

        if (!sucess || movie is null)
            return UnprocessableEntity("Failed to mark movie as watched");

        return Ok(new MovieResult(movie));
    }
}
