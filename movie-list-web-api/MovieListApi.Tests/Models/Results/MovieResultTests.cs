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

    [Theory]
    [InlineData(null)]
    public void ShouldThrowException(Movie entity)
    {
        Assert.Throws<ArgumentNullException>(() => new MovieResult(entity));
    }
}
