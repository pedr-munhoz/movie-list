using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieListApi.Infrastructure.Database;

namespace MovieListApi.Tests.Factories.Infrastructure;

public static class MockDbContextFactory
{
    public static MoviesDbContext BuildInMemory<T>()
        where T : DbContext
    {
        var options = new DbContextOptionsBuilder<T>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var dbContext = new MoviesDbContext(options);

        dbContext.Database.EnsureCreated();

        return dbContext;
    }
}
