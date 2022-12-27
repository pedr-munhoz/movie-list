using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieListApi.Tests.Infrastructure.Database;

public static class DbContextExtensions
{
    public static Task ReloadAllEntities(this DbContext context)
    {
        var entries = context.ChangeTracker
            .Entries().ToList().Select(e => e.ReloadAsync())
            .ToList();

        return Task.WhenAll(entries);
    }
}
