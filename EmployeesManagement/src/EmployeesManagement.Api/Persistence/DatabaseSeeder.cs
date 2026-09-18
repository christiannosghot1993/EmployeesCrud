using EmployeesManagement.Application.Authentication;
using EmployeesManagement.Domain.Entities;
using EmployeesManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmployeesManagement.Api.Persistence;

/// <summary>Applies pending migrations and seeds a default admin account.</summary>
public static class DatabaseSeeder
{
    public static async Task MigrateAndSeed(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

        await context.Database.MigrateAsync();

        const string adminUsername = "admin";
        if (!await context.Users.AnyAsync(u => u.Username == adminUsername))
        {
            var admin = User.Create(adminUsername, "admin@example.com", hasher.Hash("Admin123!"));
            await context.Users.AddAsync(admin);
            await context.SaveChangesAsync();
        }
    }
}
