namespace EmployeesManagement.Application.Employees.Dtos;

public record UpdateEmployeeRequest(
    string FirstName,
    string LastName,
    string Email,
    string Position,
    DateTime HireDate);
