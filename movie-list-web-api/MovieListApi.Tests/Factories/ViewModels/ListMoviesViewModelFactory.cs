using MovieListApi.Models.ViewModels;
using MovieListApi.Tests.Utils;

namespace MovieListApi.Tests.Factories.ViewModels;

public static class ListMoviesViewModelFactory
{
    public static ListMoviesViewModel Build(this ListMoviesViewModel model)
    {
        model.Index = 0;
        model.Length = 30;

        return model;
    }

    public static ListMoviesViewModel WithTitle(this ListMoviesViewModel model, string? title)
    {
        model.Title = title;

        return model;
    }

    public static ListMoviesViewModel WithReleaseDate(this ListMoviesViewModel model, DateTime? date)
    {
        model.ReleaseDate = date;

        return model;
    }

    public static ListMoviesViewModel WithReleaseDate(this ListMoviesViewModel model, string? date)
    {
        model.ReleaseDate = date.ToDateTime();

        return model;
    }

    public static ListMoviesViewModel WithCountry(this ListMoviesViewModel model, string? country)
    {
        model.Country = country;

        return model;
    }

    public static ListMoviesViewModel WithGenreId(this ListMoviesViewModel model, string? genreId)
    {
        model.GenreId = genreId;

        return model;
    }
}
