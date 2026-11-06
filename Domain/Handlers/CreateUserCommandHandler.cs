using Domain.Commands;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Handlers;

/// <summary>
/// Handler for CreateUserCommand
/// </summary>
public class CreateUserCommandHandler
{
    private readonly IRepository<User> _userRepository;

    public CreateUserCommandHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> HandleAsync(CreateUserCommand command)
    {
        var user = new User
        {
            Name = command.Name,
            Email = command.Email,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return user;
    }
}
