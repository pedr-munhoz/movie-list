using MovieListApi.Models.Entities;
using MovieListApi.Models.Results;
using MovieListApi.Tests.Comparators;
using MovieListApi.Tests.Factories.Entities;

namespace MovieListApi.Tests.Models.Results;

public class MovieResultTests
{
    [Fact]
    public void ShouldInstantiate()
    {
        // Given
        var entity = new Movie().Build();

        // When
        var result = new MovieResult(entity);

        // Then
        Assert.True(entity.IsEquivalent(result));
    }

    [Fact]
    public void ShouldThrowException()
    {
#pragma warning disable CS8625
        Assert.Throws<ArgumentNullException>(() => new MovieResult(entity: null));
#pragma warning restore CS8625
    }
}
