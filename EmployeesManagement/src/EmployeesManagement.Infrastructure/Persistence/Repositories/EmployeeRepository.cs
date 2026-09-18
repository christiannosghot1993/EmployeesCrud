using EmployeesManagement.Domain.Entities;
using EmployeesManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeesManagement.Infrastructure.Persistence.Repositories;

public sealed class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context) => _context = context;

    public async Task<List<Employee>> GetAll()
        => await _context.Employees.AsNoTracking().OrderBy(e => e.LastName).ToListAsync();

    public async Task<Employee?> GetById(Guid id)
        => await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);

    public async Task<bool> EmailExists(string email, Guid? excludeId = null)
    {
        var normalized = email.Trim().ToLowerInvariant();
        return await _context.Employees
            .AnyAsync(e => e.Email == normalized && (excludeId == null || e.Id != excludeId));
    }

    public async Task Add(Employee employee)
        => await _context.Employees.AddAsync(employee);

    public void Update(Employee employee) => _context.Employees.Update(employee);

    public void Remove(Employee employee) => _context.Employees.Remove(employee);

    public async Task<int> SaveChanges()
        => await _context.SaveChangesAsync();
}
