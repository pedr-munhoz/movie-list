using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieListApi.Infrastructure.Database;
using MovieListApi.Models.Entities;
using MovieListApi.Models.ViewModels;

namespace MovieListApi.Services;

public class MovieGenresManager : IMovieGenresManager
{
    public const int BaseLength = 30;
    private readonly MoviesDbContext _dbContext;

    public MovieGenresManager(MoviesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<(bool success, MovieGenre? entity)> Create(MovieGenreViewModel model)
    {
        var entity = ModelToEntity(model);

        await _dbContext.MovieGenres.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return (true, entity);
    }
    public async Task<ICollection<MovieGenre>> List(OffsetViewModel offset)
    {
        var index = offset.Index ?? 0;
        var length = offset.Length ?? BaseLength;

        var entities = await _dbContext.MovieGenres
            .OrderBy(x => x.Id)
            .Skip(index)
            .Take(length)
            .ToListAsync();

        return entities;
    }

    private MovieGenre ModelToEntity(MovieGenreViewModel model)
    {
        var entity = new MovieGenre
        {
            Name = model.Name,
        };

        return entity;
    }

}
