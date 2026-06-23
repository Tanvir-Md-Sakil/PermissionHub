using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PermissionHub;
using PermissionHub.Models;

public class CreateRoleModel : PageModel
{
    private readonly RoleRepository _repo;

    public CreateRoleModel(RoleRepository repo)
    {
        _repo = repo;
    }

    [BindProperty]
    public string Name { get; set; }

    public async Task<IActionResult> OnPost()
    {
        await _repo.CreateAsync(new Role
        {
            Id = Guid.NewGuid(),
            Name = Name
        });

        return RedirectToPage("Index");
    }
}