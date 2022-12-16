using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieListApi.Entities;

public class Movie
{
    public string Name { get; set; } = null!;
    public DateTime? ReleaseDate { get; set; }
    public string? Country { get; set; }
}
