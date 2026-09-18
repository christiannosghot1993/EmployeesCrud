using EmployeesManagement.Application.Authentication;
using EmployeesManagement.Application.Employees;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeesManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}

/// <summary>Assembly marker used for validator/service scanning.</summary>
internal sealed class DependencyInjectionMarker;
