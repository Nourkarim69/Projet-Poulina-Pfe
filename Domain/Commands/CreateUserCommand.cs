namespace Domain.Commands;

/// <summary>
/// Command to create a new user
/// </summary>
public class CreateUserCommand
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
