using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieListApi.Models.ViewModels;
using Xunit;

namespace MovieListApi.Tests.Models.ViewModels;

public class OffsetViewModelTest
{
    [Fact]
    public void Validation_WhenValid_ReturnsNoError()
    {
        // Given
        var viewModel = new OffsetViewModel { Index = 10, Length = OffsetViewModel.MaxLength };

        // When
        var (isValid, errors) = TestModelHelper.Validate(viewModel);

        // Then
        Assert.True(isValid);
        Assert.Empty(errors);
    }

    [Fact]
    public void Validation_WhenIndexNegative_ReturnsOutOfRangeError()
    {
        // Given
        var viewModel = new OffsetViewModel { Index = -1 };

        // When
        var (isValid, errors) = TestModelHelper.Validate(viewModel);

        // Then
        Assert.False(isValid);
        Assert.NotEmpty(errors);
        Assert.Contains(errors, x =>
            x.ErrorMessage is not null &&
            x.ErrorMessage.ToLower().Contains("index") &&
            x.ErrorMessage.ToLower().Contains("between 0")
        );
    }

    [Fact]
    public void Validation_WhenLengthNegative_ReturnsOutOfRangeError()
    {
        // Given
        var viewModel = new OffsetViewModel { Length = -1 };

        // When
        var (isValid, errors) = TestModelHelper.Validate(viewModel);

        // Then
        Assert.False(isValid);
        Assert.NotEmpty(errors);
        Assert.Contains(errors, x =>
            x.ErrorMessage is not null &&
            x.ErrorMessage.ToLower().Contains("length") &&
            x.ErrorMessage.ToLower().Contains("between 0")
        );
    }

    [Fact]
    public void Validation_WhenLengthOverMax_ReturnsOutOfRangeError()
    {
        // Given
        var viewModel = new OffsetViewModel { Length = OffsetViewModel.MaxLength + 1 };

        // When
        var (isValid, errors) = TestModelHelper.Validate(viewModel);

        // Then
        Assert.False(isValid);
        Assert.NotEmpty(errors);
        Assert.Contains(errors, x =>
            x.ErrorMessage is not null &&
            x.ErrorMessage.ToLower().Contains("length") &&
            x.ErrorMessage.ToLower().Contains("between 0")
        );
    }
}
