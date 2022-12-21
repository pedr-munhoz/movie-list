using MovieListApi.Models.Entities;

namespace MovieListApi.Models.Results;

public class MovieGenreResult
{
    public MovieGenreResult(MovieGenre entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        Id = entity.Id.ToString();
        Name = entity.Name;
    }

    public string Id { get; set; }
    public string Name { get; set; }
}
