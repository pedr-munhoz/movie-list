using Microsoft.AspNetCore.Mvc;
using MovieListApi.Models.Results;
using MovieListApi.Models.ViewModels;
using MovieListApi.Services;

namespace MovieListApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieGenresController : ControllerBase
    {
        private readonly IMovieGenresManager _manager;

        public MovieGenresController(IMovieGenresManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create([FromBody] MovieGenreViewModel model)
        {
            var (sucess, entity) = await _manager.Create(model);

            if (!sucess || entity is null)
                return UnprocessableEntity("Failed to create movie genre");

            return Ok(new MovieGenreResult(entity));
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> List([FromRoute] OffsetViewModel offset)
        {
            var entities = await _manager.List(offset);
            return Ok(entities.Select(x => new MovieGenreResult(x)).ToList());
        }
    }
}