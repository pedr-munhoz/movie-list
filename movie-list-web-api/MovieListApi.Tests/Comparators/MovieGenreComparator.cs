using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieListApi.Models.Entities;
using MovieListApi.Models.Results;
using MovieListApi.Models.ViewModels;

namespace MovieListApi.Tests.Comparators;

public static class MovieGenreComparator
{
    public static bool IsEquivalent(this MovieGenre entity, MovieGenreResult? result)
    {
        if (result is null)
            return false;

        return entity.Id.ToString() == result.Id &&
            entity.Name == result.Name;
    }

    public static bool IsEquivalent(this MovieGenreViewModel model, MovieGenre? entity)
    {
        if (entity is null)
            return false;

        return model.Name == entity.Name;
    }
}
