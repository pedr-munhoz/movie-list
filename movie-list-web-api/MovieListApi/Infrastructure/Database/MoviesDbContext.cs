using Microsoft.EntityFrameworkCore;
using MovieListApi.Models.Entities;

namespace MovieListApi.Infrastructure.Database;

public class MoviesDbContext : DbContext
{
    public MoviesDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; } = null!;
}
