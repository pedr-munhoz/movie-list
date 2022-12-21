using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using MovieListApi.Models.Entities;
using MovieListApi.Models.ViewModels;
using MovieListApi.Services;

namespace MovieListApi.Tests.Factories.Services;

public static class MovieGenresManagerFactory
{
    public static Mock<IMovieGenresManager> MockList(this Mock<IMovieGenresManager> service, ICollection<MovieGenre> entities)
    {
        service.Setup(x => x.List()).ReturnsAsync(entities);

        return service;
    }

    public static Mock<IMovieGenresManager> MockCreate(
        this Mock<IMovieGenresManager> service,
        MovieGenreViewModel viewModel,
        MovieGenre entity)
    {
        service.Setup(x => x.Create(viewModel)).ReturnsAsync((true, entity));

        return service;
    }

    public static Mock<IMovieGenresManager> MockFailureToCreate(
        this Mock<IMovieGenresManager> service,
        MovieGenreViewModel viewModel)
    {
        service.Setup(x => x.Create(viewModel)).ReturnsAsync((false, null));

        return service;
    }

    public static Mock<IMovieGenresManager> MockInternalFailureToCreate(
        this Mock<IMovieGenresManager> service,
        MovieGenreViewModel viewModel)
    {
        service.Setup(x => x.Create(viewModel)).ReturnsAsync((true, null));

        return service;
    }
}
