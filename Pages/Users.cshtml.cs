using Data.Repositories;
using Domain.Commands;
using Domain.Handlers;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyWebApp.Pages;

public class UsersModel : PageModel
{
    private readonly CreateUserCommandHandler _createUserHandler;
    private readonly IRepository<User> _userRepository;

    [BindProperty]
    public CreateUserCommand NewUser { get; set; } = new();

    public IEnumerable<User> Users { get; set; } = new List<User>();

    public UsersModel(CreateUserCommandHandler createUserHandler, IRepository<User> userRepository)
    {
        _createUserHandler = createUserHandler;
        _userRepository = userRepository;
    }

    public async Task OnGetAsync()
    {
        // Query: Get all users
        Users = await _userRepository.GetAllAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            // Command: Create a new user
            await _createUserHandler.HandleAsync(NewUser);
            return RedirectToPage();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error creating user: {ex.Message}");
            return Page();
        }
    }
}
