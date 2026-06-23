using Microsoft.AspNetCore.Mvc.RazorPages;
using PermissionHub.Models;

namespace PermissionHub.Pages.Dashboard;

public class IndexModel : PageModel
{
    private readonly CompanyRepository _companyRepo;

    public IndexModel(CompanyRepository companyRepo)
    {
        _companyRepo = companyRepo;
    }

    public List<Company> Companies { get; set; } = new();

    public async Task OnGetAsync()
    {
        Companies = await _companyRepo.GetAllAsync();
    }
}