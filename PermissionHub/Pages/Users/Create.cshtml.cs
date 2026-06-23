using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PermissionHub;
using PermissionHub.Models;

public class CreateUserModel : PageModel
{
    private readonly UserRepository _repo;

    public CreateUserModel(UserRepository repo)
    {
        _repo = repo;
    }

    [BindProperty]
    public string FullName { get; set; }

    [BindProperty]
    public string Email { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public void OnGet() { }

    public async Task<IActionResult> OnPost()
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = FullName,
            Email = Email,
            PasswordHash = PasswordHelper.Hash(Password)
        };

        await _repo.CreateAsync(user);

        return RedirectToPage("Index");
    }
}