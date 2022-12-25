using MovieListApi.Models.Entities;
using MovieListApi.Models.Results;
using MovieListApi.Tests.Comparators;
using MovieListApi.Tests.Factories.Entities;

namespace MovieListApi.Tests.Models.Results;

public class MovieResultTests
{
    [Fact]
    public void Constructor_WhenCalledWithValidEntity_SetsPropertiesCorrectly()
    {
        // Given
        var entity = new Movie().Build();

        // When
        var result = new MovieResult(entity);

        // Then
        Assert.True(entity.IsEquivalent(result));
    }

    [Fact]
    public void Constructor_WhenCalledWithNullEntity_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new MovieResult(null!));
    }
}
