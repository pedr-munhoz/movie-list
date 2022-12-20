using Microsoft.AspNetCore.Mvc;
using MovieListApi.Models.ViewModels;

namespace MovieListApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieGenresController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public Task<IActionResult> CreateMovieGenre([FromBody] MovieGenreViewModel model)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("")]
        public Task<IActionResult> ListMovieGenre()
        {
            throw new NotImplementedException();
        }
    }
}