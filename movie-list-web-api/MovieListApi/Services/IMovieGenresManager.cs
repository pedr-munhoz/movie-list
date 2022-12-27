using MovieListApi.Models.Entities;
using MovieListApi.Models.ViewModels;

namespace MovieListApi.Services;

public interface IMovieGenresManager
{
    Task<ICollection<MovieGenre>> List(OffsetViewModel offset);
    Task<(bool success, MovieGenre? entity)> Create(MovieGenreViewModel model);
}
