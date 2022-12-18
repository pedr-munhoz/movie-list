using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieListApi.Models.Entities;
using MovieListApi.Models.Results;

namespace MovieListApi.Tests.Comparators;

public static class MovieComparator
{
    public static bool IsEquivalent(this Movie entity, MovieResult result)
    {
        return entity.Id.ToString() == result.Id &&
            entity.Name == result.Name &&
            entity.ReleaseDate == result.ReleaseDate &&
            entity.Country == result.Country;
    }
}
