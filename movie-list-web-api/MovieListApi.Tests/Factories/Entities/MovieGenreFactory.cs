using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieListApi.Models.Entities;

namespace MovieListApi.Tests.Factories.Entities;

public static class MovieGenreFactory
{
    private static Random _random = new Random();

    public static MovieGenre Build(this MovieGenre entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        entity.Id = _random.Next();
        entity.Name = Guid.NewGuid().ToString();

        return entity;
    }
}
