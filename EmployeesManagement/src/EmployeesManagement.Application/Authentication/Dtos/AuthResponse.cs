namespace EmployeesManagement.Application.Authentication.Dtos;

public record AuthResponse(
    string Token,
    DateTime ExpiresAtUtc,
    Guid UserId,
    string Username,
    string Email);
