using MovieListApi.Models.Entities;
using MovieListApi.Models.Results;
using MovieListApi.Tests.Comparators;
using MovieListApi.Tests.Factories.Entities;

namespace MovieListApi.Tests.Models.Results;

public class MovieGenreResultTest
{
    [Fact]
    public void ShouldInstantiate()
    {
        // Given
        var entity = new MovieGenre().Build();

        // When
        var result = new MovieGenreResult(entity);

        // Then
        Assert.True(entity.IsEquivalent(result));
    }

    [Theory]
    [InlineData(null)]
    public void ShouldThrowException(MovieGenre entity)
    {
        Assert.Throws<ArgumentNullException>(() => new MovieGenreResult(entity));
    }
}
