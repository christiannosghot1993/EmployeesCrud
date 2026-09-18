namespace EmployeesManagement.Application.Employees.Dtos;

public record CreateEmployeeRequest(
    string FirstName,
    string LastName,
    string Email,
    string Position,
    DateTime HireDate);
