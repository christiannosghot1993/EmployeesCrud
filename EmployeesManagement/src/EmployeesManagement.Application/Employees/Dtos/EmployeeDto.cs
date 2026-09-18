namespace EmployeesManagement.Application.Employees.Dtos;

public record EmployeeDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Position,
    DateTime HireDate);
