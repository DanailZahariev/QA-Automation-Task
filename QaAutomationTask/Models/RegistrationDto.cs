namespace QaAutomationTask.Models;

public class RegistrationDto
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Email { get; init; }
    public string? Gender { get; init; }
    public string? Mobile { get; init; }
    public string? BirthDate { get; init; }
    public string? Subject { get; init; }
    public string[]? Hobbies { get; init; }
    public string? Address { get; init; }
    public string? State { get; init; }
    public string? City { get; init; }
}