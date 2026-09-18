using EmployeesManagement.Application.Authentication;
using EmployeesManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EmployeesManagement.Infrastructure.Authentication;

/// <summary>Wraps ASP.NET Core Identity's PBKDF2 password hasher.</summary>
public sealed class IdentityPasswordHasher : IPasswordHasher
{
    private readonly PasswordHasher<User> _hasher = new();

    public string Hash(string password) => _hasher.HashPassword(user: null!, password);

    public bool Verify(string passwordHash, string providedPassword)
    {
        var result = _hasher.VerifyHashedPassword(user: null!, passwordHash, providedPassword);
        return result is PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded;
    }
}
