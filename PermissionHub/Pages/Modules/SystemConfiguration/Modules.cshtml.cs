using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PermissionHub;
using PermissionHub.Models;


namespace PermissionHub.Pages.SystemConfig.Modules
{

    public class IndexModel : PageModel
    {
        private readonly CompanyRepository _companyRepo;
        private readonly ModuleRepository _moduleRepo;
        private readonly CompanyModuleRepository _companyModuleRepo;

        public IndexModel(
            CompanyRepository companyRepo,
            ModuleRepository moduleRepo,
            CompanyModuleRepository companyModuleRepo)
        {
            _companyRepo = companyRepo;
            _moduleRepo = moduleRepo;
            _companyModuleRepo = companyModuleRepo;
        }

        [BindProperty(SupportsGet = true)]
        public Guid CompanyId { get; set; }

        [BindProperty]
        public List<Guid> SelectedModules { get; set; } = new();

        public Company? Company { get; set; }

        public List<ModuleViewModel> Modules { get; set; } = new();

        public async Task OnGetAsync()
        {
            Company = await _companyRepo.GetByIdAsync(CompanyId);

            Modules = await _companyModuleRepo
                .GetModulesForCompanyAsync(CompanyId);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _companyModuleRepo
                .UpdateModulesAsync(
                    CompanyId,
                    SelectedModules);

            return RedirectToPage(
                new { CompanyId });
        }
    }
}