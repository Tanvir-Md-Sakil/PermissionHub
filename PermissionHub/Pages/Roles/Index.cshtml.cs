using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PermissionHub;
using PermissionHub.Models;

namespace PermissionHub.Pages.Roles
{

    public class IndexModel : PageModel
    {
        private readonly RoleRepository _roleRepo;
        private readonly CompanyRepository _companyRepo;

        public IndexModel(RoleRepository roleRepo, CompanyRepository companyRepo)
        {
            _roleRepo = roleRepo;
            _companyRepo = companyRepo;
        }

        // IMPORTANT: must match query string "companyId"
        [BindProperty(SupportsGet = true)]
        public Guid CompanyId { get; set; }

        public List<Role> Roles { get; set; } = new();
        public Company? Company { get; set; }

        public async Task OnGetAsync()
        {
            if (CompanyId == Guid.Empty)
                return;

            Company = await _companyRepo.GetByIdAsync(CompanyId);

            Roles = await _roleRepo.GetByCompanyIdAsync(CompanyId);
        }
    }
}