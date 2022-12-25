using MovieListApi.Infrastructure.Extensions;
using MovieListApi.Models.Entities;

namespace MovieListApi.Models.Results;

public class MovieGenreResult
{
    public MovieGenreResult(MovieGenre entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        Id = entity.Id.ToStringId();
        Name = entity.Name;
    }

    public string Id { get; set; }
    public string Name { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is MovieGenreResult result &&
               Id == result.Id &&
               Name == result.Name;
    }

    public override int GetHashCode() => base.GetHashCode();
}
