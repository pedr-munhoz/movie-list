using MovieListApi.Models.Entities;
using MovieListApi.Models.Results;

namespace MovieListApi.Tests.Comparators;

public static class MovieComparator
{
    public static bool IsEquivalent(this Movie entity, MovieResult? result)
    {
        if (result is null)
            return false;

        return entity.Id.ToString() == result.Id &&
            entity.Name == result.Name &&
            entity.ReleaseDate == result.ReleaseDate &&
            entity.Country == result.Country;
    }
}
