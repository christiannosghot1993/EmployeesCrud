using EmployeesManagement.Domain.Entities;

namespace EmployeesManagement.Application.Authentication;

public record TokenResult(string Token, DateTime ExpiresAtUtc);

/// <summary>Generates signed JWT access tokens for authenticated users.</summary>
public interface IJwtTokenGenerator
{
    TokenResult GenerateToken(User user);
}
