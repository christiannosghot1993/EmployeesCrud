namespace EmployeesManagement.Domain.Entities;

/// <summary>Application user whose password is always stored as a hash.</summary>
public class User
{
    public Guid Id { get; private set; }
    public string Username { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;

    // Required by EF Core.
    private User() { }

    private User(Guid id, string username, string email, string passwordHash)
    {
        Id = id;
        SetUsername(username);
        SetEmail(email);
        SetPasswordHash(passwordHash);
    }

    public static User Create(string username, string email, string passwordHash)
        => new(Guid.NewGuid(), username, email, passwordHash);

    private void SetUsername(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Username is required.", nameof(value));
        Username = value.Trim();
    }

    private void SetEmail(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email is required.", nameof(value));
        Email = value.Trim().ToLowerInvariant();
    }

    private void SetPasswordHash(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Password hash is required.", nameof(value));
        PasswordHash = value;
    }
}
