using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieListApi.Models.Entities;
using MovieListApi.Models.Results;
using Xunit;

namespace MovieListApi.Tests.Models.Results;

public class MovieResultTests
{
    [Fact]
    public void ShouldInstantiate()
    {
        // Given
        var entity = new Movie
        {
            Id = new Random().Next(),
            Name = Guid.NewGuid().ToString(),
            ReleaseDate = DateTime.Now,
            Country = Guid.NewGuid().ToString(),
        };

        // When
        var result = new MovieResult(entity);

        // Then
        Assert.Equal(entity.Id.ToString(), result.Id);
        Assert.Equal(entity.Name, result.Name);
        Assert.Equal(entity.ReleaseDate, result.ReleaseDate);
        Assert.Equal(entity.Country, result.Country);
    }

    [Fact]
    public void ShouldThrowException()
    {
#pragma warning disable CS8625
        Assert.Throws<ArgumentNullException>(() => new MovieResult(entity: null));
#pragma warning restore CS8625
    }
}
