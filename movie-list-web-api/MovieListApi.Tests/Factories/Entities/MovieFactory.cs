using MovieListApi.Models.Entities;

namespace MovieListApi.Tests.Factories.Entities;

public static class MovieFactory
{
    private static Random _random = new Random();

    public static Movie Build(this Movie entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        entity.Title = Guid.NewGuid().ToString();
        entity.ReleaseDate = DateTime.Now;
        entity.Country = Guid.NewGuid().ToString();
        entity.Genres = new List<MovieGenre>();

        return entity;
    }

    public static Movie Watched(this Movie entity)
    {
        entity.Watched = true;

        return entity;
    }

    public static Movie WithGenre(this Movie entity, MovieGenre genre)
    {
        entity.Genres.Add(genre);

        return entity;
    }

    public static Movie WithTitle(this Movie entity, string title)
    {
        entity.Title = title;

        return entity;
    }

    public static Movie WithReleaseDate(this Movie entity, DateTime date)
    {
        entity.ReleaseDate = date;

        return entity;
    }

    public static Movie WithCountry(this Movie entity, string country)
    {
        entity.Country = country;

        return entity;
    }

    public static List<Movie> Build(this List<Movie> entities, int? count = null)
    {
        count = count ?? _random.Next(50);

        for (int i = 0; i < count; i++)
            entities.Add(new Movie().Build());

        return entities;
    }

    public static List<Movie> Watched(this List<Movie> entities)
    {
        foreach (var entity in entities)
            entity.Watched = true;

        return entities;
    }
}
