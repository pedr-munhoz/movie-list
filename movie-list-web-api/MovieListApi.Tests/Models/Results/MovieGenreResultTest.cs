using MovieListApi.Models.Entities;
using MovieListApi.Models.Results;
using MovieListApi.Tests.Comparators;
using MovieListApi.Tests.Factories.Entities;

namespace MovieListApi.Tests.Models.Results;

public class MovieGenreResultTest
{
    [Fact]
    public void Initialize_WhenCalledWithValidEntity_SetsPropertiesCorrectly()
    {
        // Given
        var entity = new MovieGenre().Build();

        // When
        var result = new MovieGenreResult(entity);

        // Then
        Assert.True(entity.IsEquivalent(result));
    }

    [Fact]
    public void Initialize_WhenCalledWithNullEntity_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new MovieGenreResult(null!));
    }
}
