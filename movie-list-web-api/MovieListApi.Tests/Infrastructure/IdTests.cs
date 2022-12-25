using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieListApi.Infrastructure.Extensions;
using Xunit;

namespace MovieListApi.Tests.Infrastructure;

public class IdTests
{
    [Fact]
    public void ToIntId_WhenCalledWithValidString_ReturnsId()
    {
        // Given
        var stringId = "125";

        // When
        var id = stringId.ToIntId();

        // Then
        Assert.NotNull(id);
        Assert.Equal(125, id);
    }

    [Fact]
    public void ToIntId_WhenCalledWithInvalidString_ReturnsNull()
    {
        // Given
        var stringId = "autnehntue";

        // When
        var id = stringId.ToIntId();

        // Then
        Assert.Null(id);
    }

    [Fact]
    public void ToStringId_WhenCalled_ReturnsId()
    {
        // Given
        var intId = 1541;

        // When
        var id = intId.ToStringId();

        // Then
        Assert.Equal("1541", id);
    }
}
