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

        entity.Name = Guid.NewGuid().ToString();

        return entity;
    }

    public static List<MovieGenre> Build(this List<MovieGenre> entities, int? count = null)
    {
        count = count ?? _random.Next(50);

        for (int i = 0; i < count; i++)
            entities.Add(new MovieGenre().Build());

        return entities;
    }
}
