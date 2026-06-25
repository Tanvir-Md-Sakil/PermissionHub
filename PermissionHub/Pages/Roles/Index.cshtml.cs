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

    [BindProperty(SupportsGet = true)]
    public Guid CompanyId { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _repo.CreateAsync(new Role
        {
            Id = Guid.NewGuid(),
            CompanyId = CompanyId,
            Name = Name
        });

        return RedirectToPage("Index", new { companyId = CompanyId });
    }
}