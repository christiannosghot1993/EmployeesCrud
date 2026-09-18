using EmployeesManagement.Application.Employees.Dtos;

namespace EmployeesManagement.Application.Employees;

/// <summary>Employee CRUD use cases.</summary>
public interface IEmployeeService
{
    Task<List<EmployeeDto>> GetAll();
    Task<EmployeeDto> GetById(Guid id);
    Task<EmployeeDto> Create(CreateEmployeeRequest request);
    Task<EmployeeDto> Update(Guid id, UpdateEmployeeRequest request);
    Task Delete(Guid id);
}
