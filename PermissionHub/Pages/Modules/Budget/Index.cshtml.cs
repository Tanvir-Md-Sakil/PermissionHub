using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PermissionHub.Models;
using PermissionHub;

namespace PermissionHub.Pages.Modules.Budget
{
    public class IndexModel : PageModel
    {
        private readonly PermissionService _permissionService;
        private readonly BudgetRepository _repo;

        public IndexModel(
            PermissionService permissionService,
            BudgetRepository repo)
        {
            _permissionService = permissionService;
            _repo = repo;
        }

        public List<BudgetModel> Budgets { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public Guid CompanyId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Guid userId = GetUserIdFromSession();

            bool allowed = await _permissionService.CanAccessPageAsync(
                userId,
                CompanyId,
                "BUDGET",
                "BUDGET_INDEX"
            );

            if (!allowed)
                return RedirectToPage("/AccessDenied");

            Budgets = await _repo.GetByCompanyAsync(CompanyId);

            return Page();
        }

        private Guid GetUserIdFromSession()
        {
            return Guid.Parse(HttpContext.Session.GetString("UserId"));
        }
    }
}