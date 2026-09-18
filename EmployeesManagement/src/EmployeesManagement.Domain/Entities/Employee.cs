namespace EmployeesManagement.Domain.Entities;

/// <summary>Employee aggregate root enforcing core business invariants.</summary>
public class Employee
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Position { get; private set; } = string.Empty;
    public DateTime HireDate { get; private set; }

    // Required by EF Core.
    private Employee() { }

    private Employee(Guid id, string firstName, string lastName, string email, string position, DateTime hireDate)
    {
        Id = id;
        SetFirstName(firstName);
        SetLastName(lastName);
        SetEmail(email);
        SetPosition(position);
        SetHireDate(hireDate);
    }

    public static Employee Create(string firstName, string lastName, string email, string position, DateTime hireDate)
        => new(Guid.NewGuid(), firstName, lastName, email, position, hireDate);

    public void Update(string firstName, string lastName, string email, string position, DateTime hireDate)
    {
        SetFirstName(firstName);
        SetLastName(lastName);
        SetEmail(email);
        SetPosition(position);
        SetHireDate(hireDate);
    }

    private void SetFirstName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("First name is required.", nameof(value));
        FirstName = value.Trim();
    }

    private void SetLastName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Last name is required.", nameof(value));
        LastName = value.Trim();
    }

    private void SetEmail(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email is required.", nameof(value));
        Email = value.Trim().ToLowerInvariant();
    }

    private void SetPosition(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Position is required.", nameof(value));
        Position = value.Trim();
    }

    private void SetHireDate(DateTime value)
    {
        if (value.Date > DateTime.UtcNow.Date)
            throw new ArgumentException("Hire date cannot be a future date.", nameof(value));
        HireDate = value.Date;
    }
}
