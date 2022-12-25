using System.ComponentModel.DataAnnotations;

namespace MovieListApi.Models.ViewModels;

public class MovieViewModel
{
    [Required]
    public string Title { get; set; } = null!;
    public DateTime? ReleaseDate { get; set; }
    public string? Country { get; set; }
}
