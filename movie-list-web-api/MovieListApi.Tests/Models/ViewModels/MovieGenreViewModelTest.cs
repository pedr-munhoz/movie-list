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
    public void ShouldAcceptMovieViewModel()
    {
        // Given
        var viewModel = new MovieGenreViewModel { Name = "Some Genre" };

        // When
        var errors = TestModelHelper.Validate(viewModel);

        // Then
        Assert.Empty(errors);
    }

    [Theory]
    [InlineData(null)]
    public void ShouldNotAcceptMovieViewModelNoName(string name)
    {
        // Given
        var viewModel = new MovieGenreViewModel { Name = name };

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
