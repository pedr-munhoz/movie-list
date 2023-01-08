using Microsoft.EntityFrameworkCore;
using MovieListApi.Infrastructure.Database;
using MovieListApi.Infrastructure.Extensions;
using MovieListApi.Models.Entities;
using MovieListApi.Models.ViewModels;
using MovieListApi.Services;
using MovieListApi.Tests.Comparators;
using MovieListApi.Tests.Factories.Entities;
using MovieListApi.Tests.Factories.Infrastructure;
using MovieListApi.Tests.Factories.ViewModels;
using MovieListApi.Tests.Infrastructure.Database;

namespace MovieListApi.Tests.Services;

public class MoviesManagerTest
{
    private readonly IMoviesManager _manager;
    private readonly MoviesDbContext _dbContext;

    public MoviesManagerTest()
    {
        _dbContext = MockDbContextFactory.BuildInMemory<MoviesDbContext>();
        _manager = new MoviesManager(_dbContext);
    }

    [Fact]
    public async Task ListMoviesToWatch_WhenCalled_ReturnsOnlyMoviesToWatch()
    {
        // Given
        var entities = new List<Movie>().Build(count: 10);
        await _dbContext.Movies.AddRangeAsync(entities);

        var entity = new Movie().Build().Watched();
        await _dbContext.Movies.AddAsync(entity);

        await _dbContext.SaveChangesAsync();

        var offset = new ListMoviesViewModel { Index = 0, Length = 20 };

        // When
        var result = await _manager.ListMoviesToWatch(offset);

        // Then
        Assert.Equal(entities.Count, result.Count);
        Assert.True(result.SequenceEqual(entities));
    }

    [Theory]
    [InlineData(0, 11)]
    [InlineData(3, 7)]
    public async Task ListMoviesToWatch_WhenCalledWithOffset_ReturnsCorrectItemCount(int index, int length)
    {
        // Given
        var skipped = new List<Movie>().Build(count: index);
        var selected = new List<Movie>().Build(count: length);
        var leftover = new List<Movie>().Build();

        await _dbContext.Movies.AddRangeAsync(skipped);
        await _dbContext.Movies.AddRangeAsync(selected);
        await _dbContext.Movies.AddRangeAsync(leftover);

        await _dbContext.SaveChangesAsync();

        var offset = new ListMoviesViewModel { Index = index, Length = length };

        // When
        var result = await _manager.ListMoviesToWatch(offset);

        // Then
        Assert.Equal(length, result.Count);
        Assert.True(result.SequenceEqual(selected));
    }

    [Fact]
    public async Task ListMoviesToWatch_WhenCalledWithoutOffset_ReturnsBaseItemCount()
    {
        // Given
        var length = MoviesManager.BaseLength;

        var selected = new List<Movie>().Build(count: length);
        var leftover = new List<Movie>().Build();

        await _dbContext.Movies.AddRangeAsync(selected);
        await _dbContext.Movies.AddRangeAsync(leftover);

        await _dbContext.SaveChangesAsync();

        var offset = new ListMoviesViewModel { Index = null, Length = null };

        // When
        var result = await _manager.ListMoviesToWatch(offset);

        // Then
        Assert.Equal(length, result.Count);
        Assert.True(result.SequenceEqual(selected));
    }

    [Fact]
    public async Task ListMoviesToWatch_WhenFilteredByTitle_ReturnsFilteredMoviesToWatch()
    {
        // Given
        var title = "A Specific Title";

        var entities = new List<Movie>().Build(count: 10);
        await _dbContext.Movies.AddRangeAsync(entities);

        var selectedEntity = new Movie().Build().WithTitle(title);
        await _dbContext.Movies.AddAsync(selectedEntity);

        await _dbContext.SaveChangesAsync();

        var model = new ListMoviesViewModel().Build().WithTitle(title);

        // When
        var result = await _manager.ListMoviesToWatch(model);

        // Then
        Assert.Single(result);
        Assert.Equal(selectedEntity, result.FirstOrDefault());
    }

    [Fact]
    public async Task ListMoviesToWatch_WhenFilteredByCountry_ReturnsFilteredMoviesToWatch()
    {
        // Given
        var country = "A Specific Country";

        var entities = new List<Movie>().Build(count: 10);
        await _dbContext.Movies.AddRangeAsync(entities);

        var selectedEntity = new Movie().Build().WithCountry(country);
        await _dbContext.Movies.AddAsync(selectedEntity);

        await _dbContext.SaveChangesAsync();

        var model = new ListMoviesViewModel().Build().WithCountry(country);

        // When
        var result = await _manager.ListMoviesToWatch(model);

        // Then
        Assert.Single(result);
        Assert.Equal(selectedEntity, result.FirstOrDefault());
    }

    [Fact]
    public async Task ListMoviesToWatch_WhenFilteredByReleaseDate_ReturnsFilteredMoviesToWatch()
    {
        // Given
        var releaseDate = new DateTime(year: 1997, month: 4, day: 29);

        var entities = new List<Movie>().Build(count: 10);
        await _dbContext.Movies.AddRangeAsync(entities);

        var selectedEntity = new Movie().Build().WithReleaseDate(releaseDate);
        await _dbContext.Movies.AddAsync(selectedEntity);

        await _dbContext.SaveChangesAsync();

        var model = new ListMoviesViewModel().Build().WithReleaseDate(releaseDate);

        // When
        var result = await _manager.ListMoviesToWatch(model);

        // Then
        Assert.Single(result);
        Assert.Equal(selectedEntity, result.FirstOrDefault());
    }

    [Fact]
    public async Task ListMoviesToWatch_WhenFilteredByGenre_ReturnsFilteredMoviesToWatch()
    {
        // Given
        var genre = new MovieGenre().Build();

        var entities = new List<Movie>().Build(count: 10);
        await _dbContext.Movies.AddRangeAsync(entities);

        var selectedEntity = new Movie().Build().WithGenre(genre);
        await _dbContext.Movies.AddAsync(selectedEntity);

        await _dbContext.SaveChangesAsync();

        var model = new ListMoviesViewModel().Build().WithGenreId(genre.Id.ToStringId());

        // When
        var result = await _manager.ListMoviesToWatch(model);

        // Then
        Assert.Single(result);
        Assert.Equal(selectedEntity, result.FirstOrDefault());
    }

    [Fact]
    public async Task ListWatchedMovies_WhenCalled_ReturnsOnlyWatchedMovies()
    {
        // Given
        var entities = new List<Movie>().Build(count: 10).Watched();
        await _dbContext.Movies.AddRangeAsync(entities);

        var movie = new Movie().Build();
        await _dbContext.Movies.AddAsync(movie);

        await _dbContext.SaveChangesAsync();

        var offset = new ListMoviesViewModel { Index = 0, Length = 20 };

        // When
        var result = await _manager.ListWatchedMovies(offset);

        // Then
        Assert.Equal(entities.Count, result.Count);
        Assert.True(result.SequenceEqual(entities));
    }

    [Theory]
    [InlineData(0, 11)]
    [InlineData(3, 7)]
    public async Task ListWatchedMovies_WhenCalledWithOffset_ReturnsCorrectItemCount(int index, int length)
    {
        // Given
        var skipped = new List<Movie>().Build(count: index).Watched();
        var selected = new List<Movie>().Build(count: length).Watched();
        var leftover = new List<Movie>().Build().Watched();

        await _dbContext.Movies.AddRangeAsync(skipped);
        await _dbContext.Movies.AddRangeAsync(selected);
        await _dbContext.Movies.AddRangeAsync(leftover);

        await _dbContext.SaveChangesAsync();

        var offset = new ListMoviesViewModel { Index = index, Length = length };

        // When
        var result = await _manager.ListWatchedMovies(offset);

        // Then
        Assert.Equal(length, result.Count);
        Assert.True(result.SequenceEqual(selected));
    }

    [Fact]
    public async Task ListWatchedMovies_WhenCalledWithoutOffset_ReturnsBaseItemCount()
    {
        // Given
        var length = MoviesManager.BaseLength;

        var selected = new List<Movie>().Build(count: length).Watched();
        var leftover = new List<Movie>().Build().Watched();

        await _dbContext.Movies.AddRangeAsync(selected);
        await _dbContext.Movies.AddRangeAsync(leftover);

        await _dbContext.SaveChangesAsync();

        var offset = new ListMoviesViewModel { Index = null, Length = null };

        // When
        var result = await _manager.ListWatchedMovies(offset);

        // Then
        Assert.Equal(length, result.Count);
        Assert.True(result.SequenceEqual(selected));
    }

    [Fact]
    public async Task CreateMovieToWatch_WhenCalled_AddsMovieToDbAndReturnsIt()
    {
        // Given
        var viewModel = new MovieViewModel().Build();

        // When
        var (success, result) = await _manager.CreateMovieToWatch(viewModel);
        var entities = await _dbContext.Movies.ToListAsync();

        // Then
        Assert.True(success);
        Assert.True(viewModel.IsEquivalent(result));
        Assert.Single(entities);
        Assert.Equal(entities.First(), result);
    }

    [Fact]
    public async Task CreateWatchedMovie_WhenCalled_AddsMovieToDbAndReturnsIt()
    {
        // Given
        var viewModel = new MovieViewModel().Build();

        // When
        var (success, result) = await _manager.CreateWatchedMovie(viewModel);
        var entities = await _dbContext.Movies.ToListAsync();

        // Then
        Assert.True(success);
        Assert.True(viewModel.IsEquivalent(result));
        Assert.True(result?.Watched);
        Assert.Single(entities);
        Assert.Equal(entities.First(), result);
    }

    [Fact]
    public async Task SetMovieAsWatched_WhenCalledWithExistingMovieToWatchId_SetsMovieAsWatchedAndReturnsIt()
    {
        // Given
        var entity = new Movie().Build();
        await _dbContext.Movies.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        // When
        var (success, result) = await _manager.SetMovieAsWatched(stringId: entity.Id.ToStringId());
        await _dbContext.Entry(entity).ReloadAsync();

        // Then
        Assert.True(success);
        Assert.True(result?.Watched);
        Assert.True(entity?.Watched);
    }

    [Fact]
    public async Task SetMovieAsWatched_WhenCalledWithInvalidId_ReturnsFailure()
    {
        // Given

        // When
        var (success, result) = await _manager.SetMovieAsWatched(stringId: Guid.NewGuid().ToString());

        // Then
        Assert.False(success);
        Assert.Null(result);
    }

    [Fact]
    public async Task SetMovieAsWatched_WhenCalledWithUnknownId_ReturnsFailure()
    {
        // Given

        // When
        var (success, result) = await _manager.SetMovieAsWatched(stringId: new Random().Next().ToString());

        // Then
        Assert.False(success);
        Assert.Null(result);
    }

    [Fact]
    public async Task SetMovieAsWatched_WhenCalledWithExistingWatchedMovieId_ReturnsFailureWithMovie()
    {
        // Given
        var entity = new Movie().Build().Watched();
        await _dbContext.Movies.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        // When
        var (success, result) = await _manager.SetMovieAsWatched(stringId: entity.Id.ToString());
        await _dbContext.Entry(entity).ReloadAsync();

        // Then
        Assert.False(success);
        Assert.NotNull(result);
        Assert.Equal(entity, result);
    }

    [Fact]
    public async Task AddGenre_WhenCalledWithExistingIdAndExistingGenreId_SetRelationAndReturnMovie()
    {
        // Given
        var movie = new Movie().Build();
        await _dbContext.Movies.AddAsync(movie);

        var genre = new MovieGenre().Build();
        await _dbContext.MovieGenres.AddAsync(genre);

        await _dbContext.SaveChangesAsync();

        // When
        var (success, result) = await _manager.AddGenre(movieStringId: movie.Id.ToStringId(), genreStringId: genre.Id.ToStringId());
        await _dbContext.ReloadAllEntities();

        // Then
        Assert.True(success);
        Assert.NotNull(result);
        Assert.Equal(movie, result);
        Assert.NotEmpty(movie.Genres);
        Assert.Contains(genre, movie.Genres);
    }

    [Fact]
    public async Task AddGenre_WhenCalledWithInvalidId_ReturnFailure()
    {
        // Given
        var genre = new MovieGenre().Build();
        await _dbContext.MovieGenres.AddAsync(genre);

        await _dbContext.SaveChangesAsync();

        // When
        var (success, result) = await _manager.AddGenre(movieStringId: "eou15", genreStringId: genre.Id.ToStringId());

        // Then
        Assert.False(success);
        Assert.Null(result);
    }

    [Fact]
    public async Task AddGenre_WhenCalledWithUnknownId_ReturnFailure()
    {
        // Given
        var genre = new MovieGenre().Build();
        await _dbContext.MovieGenres.AddAsync(genre);

        await _dbContext.SaveChangesAsync();

        // When
        var (success, result) = await _manager.AddGenre(movieStringId: "12545", genreStringId: genre.Id.ToStringId());

        // Then
        Assert.False(success);
        Assert.Null(result);
    }

    [Fact]
    public async Task AddGenre_WhenCalledWithInvalidGenreId_ReturnFailureWithMovie()
    {
        // Given
        var movie = new Movie().Build();
        await _dbContext.Movies.AddAsync(movie);

        await _dbContext.SaveChangesAsync();

        // When
        var (success, result) = await _manager.AddGenre(movieStringId: movie.Id.ToStringId(), genreStringId: "5o4eu5");
        await _dbContext.Entry(movie).ReloadAsync();

        // Then
        Assert.False(success);
        Assert.NotNull(result);
        Assert.Equal(movie, result);
        Assert.Empty(movie.Genres);
    }

    [Fact]
    public async Task AddGenre_WhenCalledWithUnknownGenreId_ReturnFailureWithMovie()
    {
        // Given
        var movie = new Movie().Build();
        await _dbContext.Movies.AddAsync(movie);

        await _dbContext.SaveChangesAsync();

        // When
        var (success, result) = await _manager.AddGenre(movieStringId: movie.Id.ToStringId(), genreStringId: "1548");
        await _dbContext.Entry(movie).ReloadAsync();

        // Then
        Assert.False(success);
        Assert.NotNull(result);
        Assert.Equal(movie, result);
        Assert.Empty(movie.Genres);
    }

    [Fact]
    public async Task AddGenre_WhenCalledWithAlreadyContainedGenreId_ReturnFailureWithMovie()
    {
        // Given
        var genre = new MovieGenre().Build();
        await _dbContext.MovieGenres.AddAsync(genre);

        var movie = new Movie().Build().WithGenre(genre);
        await _dbContext.Movies.AddAsync(movie);

        await _dbContext.SaveChangesAsync();

        // When
        var (success, result) = await _manager.AddGenre(movieStringId: movie.Id.ToStringId(), genreStringId: genre.Id.ToStringId());
        await _dbContext.Entry(movie).ReloadAsync();

        // Then
        Assert.False(success);
        Assert.NotNull(result);
        Assert.Equal(movie, result);
        Assert.NotEmpty(movie.Genres);
        Assert.Contains(genre, movie.Genres);
    }
}
