using System.ComponentModel.DataAnnotations;

namespace MovieListApi.Models.ViewModels;

public class MovieGenreViewModel
{
    [Required]
    public string Name { get; set; } = null!;
}
