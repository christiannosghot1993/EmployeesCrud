using System.Net;
using System.Net.Http.Json;
using EmployeesManagement.Application.Authentication.Dtos;
using FluentAssertions;
using Xunit;

namespace EmployeesManagement.Tests.Api;

public class AuthEndpointsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public AuthEndpointsTests(CustomWebApplicationFactory factory) => _factory = factory;

    [Fact]
    public async Task Register_ThenLogin_ReturnsToken()
    {
        var client = _factory.CreateClient();
        var username = $"u{Guid.NewGuid():N}".Substring(0, 12);
        var email = $"{username}@example.com";

        var register = await client.PostAsJsonAsync("/api/auth/register", new RegisterRequest(username, email, "Password1"));
        register.StatusCode.Should().Be(HttpStatusCode.OK);

        var login = await client.PostAsJsonAsync("/api/auth/login", new LoginRequest(username, "Password1"));
        login.StatusCode.Should().Be(HttpStatusCode.OK);
        var auth = await login.Content.ReadFromJsonAsync<AuthResponse>();
        auth!.Token.Should().NotBeNullOrWhiteSpace();
    }

}
