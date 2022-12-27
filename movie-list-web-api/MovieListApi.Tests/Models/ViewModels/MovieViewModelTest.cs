using MovieListApi.Models.ViewModels;

namespace MovieListApi.Tests.Models.ViewModels;

public class MovieViewModelTest
{
    [Fact]
    public void Validation_WhenModelIsValid_ReturnsNoErrors()
    {
        // Given
        var viewModel = new MovieViewModel
        {
            Title = "Some Movie",
            ReleaseDate = DateTime.Now,
            Country = "A Country",
        };

        // When
        var (isValid, errors) = TestModelHelper.Validate(viewModel);

        // Then
        Assert.True(isValid);
        Assert.Empty(errors);
    }

    [Fact]
    public void Validation_WhenTitleIsNull_ReturnsRequiredError()
    {
        // Given
        var viewModel = new MovieViewModel
        {
            Title = null!,
            ReleaseDate = DateTime.Now,
            Country = "A Country",
        };

        // When
        var (isValid, errors) = TestModelHelper.Validate(viewModel);

        // Then
        Assert.False(isValid);
        Assert.NotEmpty(errors);
        Assert.Contains(errors, x =>
            x.ErrorMessage is not null &&
            x.ErrorMessage.ToLower().Contains("title") &&
            x.ErrorMessage.ToLower().Contains("required")
        );
    }
}
