using Microsoft.AspNetCore.Mvc;

namespace MovieListApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    [HttpGet]
    [Route("to-watch")]
    public async Task<IActionResult> GetMoviesToWatch()
    {
        return Ok();
    }
}
