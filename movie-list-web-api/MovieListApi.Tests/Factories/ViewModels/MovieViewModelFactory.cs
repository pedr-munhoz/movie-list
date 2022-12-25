using MovieListApi.Models.ViewModels;

namespace MovieListApi.Tests.Factories.ViewModels;

public static class MovieViewModelFactory
{
    public static MovieViewModel Build(this MovieViewModel model)
    {
        model.Title = Guid.NewGuid().ToString();
        model.Country = Guid.NewGuid().ToString();
        model.ReleaseDate = DateTime.Now;

        return model;
    }
}
