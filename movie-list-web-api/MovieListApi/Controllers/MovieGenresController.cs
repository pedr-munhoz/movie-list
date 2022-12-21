using Microsoft.AspNetCore.Mvc;
using MovieListApi.Models.ViewModels;
using MovieListApi.Services;

namespace MovieListApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieGenresController : ControllerBase
    {
        public MovieGenresController(IMovieGenresManager manager)
        {
        }

        [HttpPost]
        [Route("")]
        public Task<IActionResult> Create([FromBody] MovieGenreViewModel model)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("")]
        public Task<IActionResult> List()
        {
            throw new NotImplementedException();
        }
    }
}