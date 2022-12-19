using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieListApi.Models.Entities;

namespace MovieListApi.Services;

public interface IMoviesManager
{
    public Task<ICollection<Movie>> GetMoviesToWatch();
}
