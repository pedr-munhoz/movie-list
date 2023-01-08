using MovieListApi.Models.ViewModels;

namespace MovieListApi.Tests.Factories.ViewModels;

public static class ListMoviesViewModelFactory
{
    public static ListMoviesViewModel Build(this ListMoviesViewModel model)
    {
        model.Index = 0;
        model.Length = 30;

        return model;
    }
}
