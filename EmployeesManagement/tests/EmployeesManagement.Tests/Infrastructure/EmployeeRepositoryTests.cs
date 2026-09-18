using EmployeesManagement.Domain.Entities;
using EmployeesManagement.Infrastructure.Persistence.Repositories;
using FluentAssertions;
using Xunit;

namespace EmployeesManagement.Tests.Infrastructure;

public class EmployeeRepositoryTests
{
    [Fact]
    public async Task AddAndGetById_RoundTrips()
    {
        using var db = new SqliteInMemoryContext();
        var repo = new EmployeeRepository(db.Context);
        var employee = Employee.Create("Ada", "Lovelace", "ada@example.com", "Engineer", DateTime.UtcNow.AddDays(-1));

        await repo.Add(employee);
        await repo.SaveChanges();

        var loaded = await new EmployeeRepository(db.NewContext()).GetById(employee.Id);
        loaded.Should().NotBeNull();
        loaded!.Email.Should().Be("ada@example.com");
    }

}
