using Microsoft.EntityFrameworkCore;
using MovieListApi.Infrastructure.Database;

namespace api.Services;

public static class DatabaseManagementService
{
    public static void MigrationInitialisation(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            serviceScope.ServiceProvider.GetService<MoviesDbContext>()?.Database.Migrate();
        }
    }
}