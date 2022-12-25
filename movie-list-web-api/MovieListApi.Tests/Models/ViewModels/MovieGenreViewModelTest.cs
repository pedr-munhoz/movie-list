using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieListApi.Models.ViewModels;
using MovieListApi.Tests.Utils;
using Xunit;

namespace MovieListApi.Tests.Models.ViewModels;

public class MovieGenreViewModelTest
{
    [Fact]
    public void Validation_WhenModelIsValid_ReturnsNoErrors()
    {
        // Given
        var viewModel = new MovieGenreViewModel { Name = "Some Genre" };

        // When
        var (isValid, errors) = TestModelHelper.Validate(viewModel);

        // Then
        Assert.True(isValid);
        Assert.Empty(errors);
    }

    [Fact]
    public void Validation_WhenNameIsNull_ReturnsRequiredError()
    {
        // Given
        var viewModel = new MovieGenreViewModel { Name = null! };

        // When
        var (isValid, errors) = TestModelHelper.Validate(viewModel);

        // Then
        Assert.False(isValid);
        Assert.NotEmpty(errors);
        Assert.True(errors.Any(x =>
            x.ErrorMessage is not null &&
            x.ErrorMessage.ToLower().Contains("name") &&
            x.ErrorMessage.ToLower().Contains("required"))
        );
    }
}
