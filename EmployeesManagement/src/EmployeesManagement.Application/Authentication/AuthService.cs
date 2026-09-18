using EmployeesManagement.Application.Authentication.Dtos;
using EmployeesManagement.Domain.Entities;
using EmployeesManagement.Domain.Repositories;
namespace EmployeesManagement.Application.Authentication;

public sealed class AuthService : IAuthService
{
    private readonly IUserRepository _users;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _tokenGenerator;
    public AuthService(
        IUserRepository users,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator tokenGenerator)
    {
        _users = users;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<AuthResponse> Register(RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || request.Username.Length is < 3 or > 50)
            throw new ArgumentException("Username must be between 3 and 50 characters.");
        if (string.IsNullOrWhiteSpace(request.Email) || request.Email.Length > 256 ||
            !request.Email.Contains('@') || !request.Email.Contains('.'))
            throw new ArgumentException("Email is invalid.");
        if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 8 ||
            !request.Password.Any(char.IsUpper) || !request.Password.Any(char.IsLower) ||
            !request.Password.Any(char.IsDigit))
            throw new ArgumentException("Password must be at least 8 characters and contain uppercase, lowercase, and digit characters.");

        var username = request.Username.Trim();
        var email = request.Email.Trim().ToLowerInvariant();

        if (await _users.UsernameExists(username))
            throw new InvalidOperationException($"Username '{username}' is already taken.");
        if (await _users.EmailExists(email))
            throw new InvalidOperationException($"Email '{email}' is already registered.");

        var user = User.Create(username, email, _passwordHasher.Hash(request.Password));
        await _users.Add(user);
        await _users.SaveChanges();

        return CreateAuthResponse(user);
    }

    public async Task<AuthResponse> Login(LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            throw new ArgumentException("Username and password are required.");

        var identifier = request.Username.Trim();
        var user = await _users.GetByUsername(identifier)
            ?? await _users.GetByEmail(identifier.ToLowerInvariant());

        if (user is null || !_passwordHasher.Verify(user.PasswordHash, request.Password))
            throw new UnauthorizedAccessException("Invalid username or password.");

        return CreateAuthResponse(user);
    }

    private AuthResponse CreateAuthResponse(User user)
    {
        var token = _tokenGenerator.GenerateToken(user);
        return new AuthResponse(token.Token, token.ExpiresAtUtc, user.Id, user.Username, user.Email);
    }

}
