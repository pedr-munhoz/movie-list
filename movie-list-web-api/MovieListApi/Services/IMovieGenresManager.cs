using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieListApi.Models.Entities;

namespace MovieListApi.Services;

public interface IMovieGenresManager
{
    Task<ICollection<MovieGenre>> GetMovieGenres();
}
