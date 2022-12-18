using MovieListApi.Models.Entities;

namespace MovieListApi.Tests.Factories.Entities;

public static class MovieFactory
{
    public static Movie Build(this Movie entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        entity.Id = new Random().Next();
        entity.Name = Guid.NewGuid().ToString();
        entity.ReleaseDate = DateTime.Now;
        entity.Country = Guid.NewGuid().ToString();

        return entity;
    }
}
