using EmployeesManagement.Domain.Entities;

namespace EmployeesManagement.Domain.Repositories;

/// <summary>Persistence abstraction for <see cref="Employee"/> aggregates.</summary>
public interface IEmployeeRepository
{
    Task<List<Employee>> GetAll();
    Task<Employee?> GetById(Guid id);
    Task<bool> EmailExists(string email, Guid? excludeId = null);
    Task Add(Employee employee);
    void Update(Employee employee);
    void Remove(Employee employee);
    Task<int> SaveChanges();
}
