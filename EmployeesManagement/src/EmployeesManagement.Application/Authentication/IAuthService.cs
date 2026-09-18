using EmployeesManagement.Application.Authentication.Dtos;

namespace EmployeesManagement.Application.Authentication;

/// <summary>Authentication use cases.</summary>
public interface IAuthService
{
    Task<AuthResponse> Register(RegisterRequest request);
    Task<AuthResponse> Login(LoginRequest request);
}
