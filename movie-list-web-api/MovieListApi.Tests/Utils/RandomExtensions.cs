using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieListApi.Tests.Utils;

public static class RandomExtensions
{
    public static bool NextBool(this Random random) => random.Next(2) == 1;
}
