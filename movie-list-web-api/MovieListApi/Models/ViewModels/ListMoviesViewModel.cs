using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieListApi.Models.ViewModels;

public class ListMoviesViewModel : OffsetViewModel
{
    public string? Title { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string? Country { get; set; }
    public int? GenreId { get; set; }
}
