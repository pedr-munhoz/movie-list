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
    public async Task<IActionResult> ListMoviesToWatch([FromQuery] OffsetViewModel offset)
    {
        var movies = await _moviesManager.ListMoviesToWatch(offset);
        return Ok(movies.Select(x => new MovieResult(x)).ToList());
    }

    [HttpGet]
    [Route("watched")]
    public async Task<IActionResult> ListWatchedMovies([FromQuery] OffsetViewModel offset)
    {
        var movies = await _moviesManager.ListWatchedMovies(offset);
        return Ok(movies.Select(x => new MovieResult(x)).ToList());
    }

    [HttpPost]
    [Route("to-watch")]
    public async Task<IActionResult> CreateMovieToWatch([FromBody] MovieViewModel model)
    {
        var (sucess, movie) = await _moviesManager.CreateMovieToWatch(model);

        if (!sucess || movie is null)
            return UnprocessableEntity("Failed to add movie to watch");

        return Ok(new MovieResult(movie));
    }

    [HttpPost]
    [Route("watched")]
    public async Task<IActionResult> CreateWatchedMovie([FromBody] MovieViewModel model)
    {
        var (sucess, movie) = await _moviesManager.CreateWatchedMovie(model);

        if (!sucess || movie is null)
            return UnprocessableEntity("Failed to add watched movie");

        return Ok(new MovieResult(movie));
    }

    [HttpPatch]
    [Route("{id}/watched")]
    public async Task<IActionResult> SetMovieAsWatched([FromRoute] string id)
    {
        var (sucess, movie) = await _moviesManager.SetMovieAsWatched(id);

        if (!sucess || movie is null)
            return UnprocessableEntity("Failed to mark movie as watched");

        return Ok(new MovieResult(movie));
    }

    [HttpPost]
    [Route("{id}/genre/{genreId}")]
    public async Task<IActionResult> AddGenreToMovie([FromRoute] string id, string genreId)
    {
        var (sucess, movie) = await _moviesManager.AddGenre(movieStringId: id, genreStringId: genreId);

        if (!sucess || movie is null)
            return UnprocessableEntity("Failed to add genre to the movie");

        return Ok(new MovieResult(movie));
    }
}
