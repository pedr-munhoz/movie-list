using MovieListApi.Infrastructure.Extensions;
using MovieListApi.Models.Entities;
using MovieListApi.Models.Results;
using MovieListApi.Models.ViewModels;

namespace MovieListApi.Tests.Comparators;

public static class MovieComparator
{
    public static bool IsEquivalent(this Movie entity, MovieResult? result)
    {
        if (result is null)
            return false;

        return entity.Id.ToStringId() == result.Id &&
            entity.Title == result.Title &&
            entity.ReleaseDate == result.ReleaseDate &&
            entity.Country == result.Country;
    }

    public static bool IsEquivalent(this MovieViewModel model, Movie? entity)
    {
        if (entity is null)
            return false;

        return model.Title == entity.Title &&
            model.ReleaseDate == entity.ReleaseDate &&
            model.Country == entity.Country;
    }
}
