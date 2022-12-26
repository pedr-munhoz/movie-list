namespace MovieListApi.Models.Entities;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public DateTime? ReleaseDate { get; set; }
    public string? Country { get; set; }
    public bool Watched { get; set; }
    public ICollection<MovieGenre> Genres { get; set; } = null!;
}
