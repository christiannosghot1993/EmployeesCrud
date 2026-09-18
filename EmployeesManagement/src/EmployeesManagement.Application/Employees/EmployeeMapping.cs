using EmployeesManagement.Application.Employees.Dtos;
using EmployeesManagement.Domain.Entities;

namespace EmployeesManagement.Application.Employees;

internal static class EmployeeMapping
{
    public static EmployeeDto ToDto(this Employee employee) => new(
        employee.Id,
        employee.FirstName,
        employee.LastName,
        employee.Email,
        employee.Position,
        employee.HireDate);
}
