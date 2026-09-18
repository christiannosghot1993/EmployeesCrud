using EmployeesManagement.Domain.Entities;
using EmployeesManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeesManagement.Infrastructure.Persistence.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) => _context = context;

    public async Task<User?> GetById(Guid id)
        => await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

    public async Task<User?> GetByUsername(string username)
        => await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

    public async Task<User?> GetByEmail(string email)
    {
        var normalized = email.Trim().ToLowerInvariant();
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == normalized);
    }

    public async Task<bool> UsernameExists(string username)
        => await _context.Users.AnyAsync(u => u.Username == username);

    public async Task<bool> EmailExists(string email)
    {
        var normalized = email.Trim().ToLowerInvariant();
        return await _context.Users.AnyAsync(u => u.Email == normalized);
    }

    public async Task Add(User user)
        => await _context.Users.AddAsync(user);

    public async Task<int> SaveChanges()
        => await _context.SaveChangesAsync();
}
