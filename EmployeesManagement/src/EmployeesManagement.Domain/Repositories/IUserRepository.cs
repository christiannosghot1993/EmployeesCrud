using EmployeesManagement.Domain.Entities;

namespace EmployeesManagement.Domain.Repositories;

/// <summary>Persistence abstraction for <see cref="User"/> accounts.</summary>
public interface IUserRepository
{
    Task<User?> GetById(Guid id);
    Task<User?> GetByUsername(string username);
    Task<User?> GetByEmail(string email);
    Task<bool> UsernameExists(string username);
    Task<bool> EmailExists(string email);
    Task Add(User user);
    Task<int> SaveChanges();
}
