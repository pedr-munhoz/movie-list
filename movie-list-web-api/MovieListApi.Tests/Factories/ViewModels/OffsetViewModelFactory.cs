using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieListApi.Models.ViewModels;

namespace MovieListApi.Tests.Factories.ViewModels;

public static class OffsetViewModelFactory
{
    public static OffsetViewModel Build(this OffsetViewModel model)
    {
        model.Index = 0;
        model.Length = 30;

        return model;
    }
}
