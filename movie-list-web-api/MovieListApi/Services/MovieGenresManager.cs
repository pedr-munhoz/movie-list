using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieListApi.Infrastructure.Database;
using MovieListApi.Models.Entities;

namespace MovieListApi.Services;

public class MovieGenresManager : IMovieGenresManager
{
    private readonly MoviesDbContext _dbContext;

    public MovieGenresManager(MoviesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ICollection<MovieGenre>> GetMovieGenres()
    {
        var entities = await _dbContext.MovieGenres.ToListAsync();

        return entities;
    }
}
