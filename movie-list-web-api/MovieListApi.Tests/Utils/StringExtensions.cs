using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieListApi.Tests.Utils;

public static class StringExtensions
{
    public static DateTime? ToDateTime(this string? date)
    {
        if (date is null)
            return null;

        var success = DateTime.TryParse(date, out var result);

        if (!success)
            return null;

        return result;
    }
}
