using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieListApi.Models.ViewModels;

namespace MovieListApi.Tests.Factories.ViewModels;

public static class MovieGenreViewModelFactory
{
    public static MovieGenreViewModel Build(this MovieGenreViewModel model)
    {
        model.Name = Guid.NewGuid().ToString();

        return model;
    }
}
