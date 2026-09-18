using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using EmployeesManagement.Application.Authentication.Dtos;
using EmployeesManagement.Application.Employees.Dtos;
using FluentAssertions;
using Xunit;

namespace EmployeesManagement.Tests.Api;

public class EmployeesEndpointsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public EmployeesEndpointsTests(CustomWebApplicationFactory factory) => _factory = factory;

    [Fact]
    public async Task GetEmployees_WithoutToken_Returns401()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/employees");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task EmployeeCrud_WithToken_Succeeds()
    {
        var client = await CreateAuthenticatedClientAsync();

        var create = new CreateEmployeeRequest("Grace", "Hopper", $"grace{Guid.NewGuid():N}@example.com", "Admiral", DateTime.UtcNow.AddDays(-30));
        var createResponse = await client.PostAsJsonAsync("/api/employees", create);
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var created = await createResponse.Content.ReadFromJsonAsync<EmployeeDto>();
        created!.Id.Should().NotBeEmpty();

        var getResponse = await client.GetAsync($"/api/employees/{created.Id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var update = new UpdateEmployeeRequest("Grace", "Hopper", created.Email, "Rear Admiral", DateTime.UtcNow.AddDays(-20));
        var updateResponse = await client.PutAsJsonAsync($"/api/employees/{created.Id}", update);
        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var updated = await updateResponse.Content.ReadFromJsonAsync<EmployeeDto>();
        updated!.Position.Should().Be("Rear Admiral");

        var deleteResponse = await client.DeleteAsync($"/api/employees/{created.Id}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var afterDelete = await client.GetAsync($"/api/employees/{created.Id}");
        afterDelete.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateEmployee_WithInvalidData_Returns400()
    {
        var client = await CreateAuthenticatedClientAsync();

        var invalid = new CreateEmployeeRequest("", "Hopper", "not-an-email", "Admiral", DateTime.UtcNow.AddDays(10));
        var response = await client.PostAsJsonAsync("/api/employees", invalid);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    private async Task<HttpClient> CreateAuthenticatedClientAsync()
    {
        var client = _factory.CreateClient();
        var register = new RegisterRequest($"user{Guid.NewGuid():N}".Substring(0, 12), $"user{Guid.NewGuid():N}@example.com", "Password1");
        var response = await client.PostAsJsonAsync("/api/auth/register", register);
        response.EnsureSuccessStatusCode();
        var auth = await response.Content.ReadFromJsonAsync<AuthResponse>();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth!.Token);
        return client;
    }
}
