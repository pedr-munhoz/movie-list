using MovieListApi.Models.Entities;

namespace MovieListApi.Models.Results;

public class MovieResult
{
    public MovieResult(Movie entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        Id = entity.Id.ToString();
        Title = entity.Title;
        ReleaseDate = entity.ReleaseDate;
        Country = entity.Country;
    }

    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public DateTime? ReleaseDate { get; set; }
    public string? Country { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is MovieResult result &&
               Id == result.Id &&
               Title == result.Title &&
               ReleaseDate == result.ReleaseDate &&
               Country == result.Country;
    }

    public override int GetHashCode() => base.GetHashCode();
}
