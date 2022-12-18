using MovieListApi.Models.Entities;

namespace MovieListApi.Tests.Factories.Entities;

public static class MovieFactory
{
    private static Random _random = new Random();
    public static Movie Build(this Movie entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        entity.Id = _random.Next();
        entity.Name = Guid.NewGuid().ToString();
        entity.ReleaseDate = DateTime.Now;
        entity.Country = Guid.NewGuid().ToString();

        return entity;
    }

    public static List<Movie> Build(this List<Movie> entities)
    {
        var count = _random.Next(50);

        for (int i = 0; i < count; i++)
            entities.Add(new Movie().Build());

        return entities;
    }
}
