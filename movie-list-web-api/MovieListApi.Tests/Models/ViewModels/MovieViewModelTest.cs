using MovieListApi.Models.ViewModels;
using MovieListApi.Tests.Utils;

namespace MovieListApi.Tests.Models.ViewModels;

public class MovieViewModelTest
{
    [Theory]
    [InlineData("Some Movie", null, null)]
    [InlineData("Some Movie", null, "A Country")]
    [InlineData("Some Movie", "2017-3-1", null)]
    [InlineData("Some Movie", "2017-3-1", "A Country")]
    public void ShouldAcceptMovieViewModel(string name, string? realeaseDate, string? country)
    {
        // Given
        var viewModel = new MovieViewModel
        {
            Name = name,
            ReleaseDate = realeaseDate.ToDateTime(),
            Country = country,
        };

        // When
        var errors = TestModelHelper.Validate(viewModel);

        // Then
        Assert.Empty(errors);
    }

    [Theory]
    [InlineData(null, null, null)]
    [InlineData(null, null, "A Country")]
    [InlineData(null, "2017-3-1", null)]
    [InlineData(null, "2017-3-1", "A Country")]
    public void ShouldNotAcceptMovieViewModelNoName(string name, string? realeaseDate, string? country)
    {
        // Given
        var viewModel = new MovieViewModel
        {
            Name = name,
            ReleaseDate = realeaseDate.ToDateTime(),
            Country = country,
        };

        // When
        var errors = TestModelHelper.Validate(viewModel);

        // Then
        Assert.NotEmpty(errors);
        Assert.True(errors.Any(x =>
            x.ErrorMessage is not null &&
            x.ErrorMessage.ToLower().Contains("name") &&
            x.ErrorMessage.ToLower().Contains("required"))
        );
    }
}
