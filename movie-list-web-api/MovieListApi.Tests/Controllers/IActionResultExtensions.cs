using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MovieListApi.Tests.Controllers;

public static class IActionResultExtensions
{
    public static bool IsOkResult(this IActionResult actionResult)
    {
        if (actionResult is OkObjectResult)
            return true;

        if (actionResult is OkResult)
            return true;

        return false;
    }

    public static (bool sucess, T? result) Parse<T>(this IActionResult actionResult)
        where T : class
    {
        var objectResult = actionResult as ObjectResult;

        if (objectResult is null)
            return (false, default(T));

        var parseResult = objectResult.Value as T;

        if (parseResult is null)
            return (false, default(T));

        return (true, parseResult);
    }
}
