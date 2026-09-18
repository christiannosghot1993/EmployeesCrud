using EmployeesManagement.Application.Employees.Dtos;
using EmployeesManagement.Domain.Entities;
using EmployeesManagement.Domain.Repositories;
using System.Net.Mail;
namespace EmployeesManagement.Application.Employees;

public sealed class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;
    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<EmployeeDto>> GetAll()
    {
        var employees = await _repository.GetAll();
        return employees.Select(e => e.ToDto()).ToList();
    }

    public async Task<EmployeeDto> GetById(Guid id)
    {
        var employee = await _repository.GetById(id)
            ?? throw new KeyNotFoundException($"Employee with key '{id}' was not found.");
        return employee.ToDto();
    }

    public async Task<EmployeeDto> Create(CreateEmployeeRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FirstName) || request.FirstName.Length > 100 ||
            string.IsNullOrWhiteSpace(request.LastName) || request.LastName.Length > 100 ||
            string.IsNullOrWhiteSpace(request.Email) || request.Email.Length > 256 || !IsEmail(request.Email) ||
            string.IsNullOrWhiteSpace(request.Position) || request.Position.Length > 100 ||
            request.HireDate.Date > DateTime.UtcNow.Date)
            throw new ArgumentException("Employee data is invalid.");

        var email = request.Email.Trim().ToLowerInvariant();
        if (await _repository.EmailExists(email, null))
            throw new InvalidOperationException($"An employee with email '{email}' already exists.");

        var employee = Employee.Create(
            request.FirstName, request.LastName, request.Email, request.Position, request.HireDate);

        await _repository.Add(employee);
        await _repository.SaveChanges();
        return employee.ToDto();
    }

    public async Task<EmployeeDto> Update(Guid id, UpdateEmployeeRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FirstName) || request.FirstName.Length > 100 ||
            string.IsNullOrWhiteSpace(request.LastName) || request.LastName.Length > 100 ||
            string.IsNullOrWhiteSpace(request.Email) || request.Email.Length > 256 || !IsEmail(request.Email) ||
            string.IsNullOrWhiteSpace(request.Position) || request.Position.Length > 100 ||
            request.HireDate.Date > DateTime.UtcNow.Date)
            throw new ArgumentException("Employee data is invalid.");

        var employee = await _repository.GetById(id)
            ?? throw new KeyNotFoundException($"Employee with key '{id}' was not found.");

        var email = request.Email.Trim().ToLowerInvariant();
        if (await _repository.EmailExists(email, id))
            throw new InvalidOperationException($"An employee with email '{email}' already exists.");

        employee.Update(
            request.FirstName, request.LastName, request.Email, request.Position, request.HireDate);

        _repository.Update(employee);
        await _repository.SaveChanges();
        return employee.ToDto();
    }

    public async Task Delete(Guid id)
    {
        var employee = await _repository.GetById(id)
            ?? throw new KeyNotFoundException($"Employee with key '{id}' was not found.");

        _repository.Remove(employee);
        await _repository.SaveChanges();
    }

    private static bool IsEmail(string value)
    {
        try
        {
            return new MailAddress(value).Address == value;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
