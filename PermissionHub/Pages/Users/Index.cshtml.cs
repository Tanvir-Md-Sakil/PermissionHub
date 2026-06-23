using Microsoft.AspNetCore.Mvc.RazorPages;
using PermissionHub;
using PermissionHub.Models;

public class UsersModel : PageModel
{
    private readonly UserRepository _repo;

    public UsersModel(UserRepository repo)
    {
        _repo = repo;
    }

    public List<User> Users { get; set; }

    public async Task OnGet()
    {
        Users = await _repo.GetAll();
    }
}