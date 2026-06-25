using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PermissionHub;
using PermissionHub.Models;

public class CreateRoleModel : PageModel
{
    private readonly RoleRepository _repo;
    private readonly CompanyRepository _companyRepo;

    public CreateRoleModel(RoleRepository repo, CompanyRepository companyRepo)
    {
        _repo = repo;
        _companyRepo = companyRepo;
    }

    [BindProperty]
    public string Name { get; set; }

    [BindProperty(SupportsGet = true)]
    public Guid CompanyId { get; set; }

    public string? CompanyName { get; set; }

    public async Task OnGetAsync()
    {
        var company = await _companyRepo.GetByIdAsync(CompanyId);
        CompanyName = company?.Name;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _repo.CreateAsync(new Role
        {
            Id = Guid.NewGuid(),
            CompanyId = CompanyId,
            Name = Name
        });

        return RedirectToPage("/Roles/Index", new { companyId = CompanyId });
    }
}