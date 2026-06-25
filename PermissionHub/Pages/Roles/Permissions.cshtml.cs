using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PermissionHub.Models;
using PermissionHub;

namespace PermissionHub.Pages.Roles
{
    public class PermissionsModel : PageModel
    {
        private readonly RolePermissionRepository _repo;

        public PermissionsModel(RolePermissionRepository repo)
        {
            _repo = repo;
        }

        [BindProperty(SupportsGet = true)]
        public Guid RoleId { get; set; }

        public List<RolePagePermission> Permissions { get; set; } = new();

        public async Task OnGetAsync()
        {
            Permissions = await _repo.GetByRoleAsync(RoleId);
        }

        public async Task<IActionResult> OnPostAssignAsync(string moduleCode, string pageCode)
        {
            await _repo.AssignAsync(RoleId, moduleCode, pageCode);
            return RedirectToPage(new { RoleId });
        }

        public async Task<IActionResult> OnPostRemoveAsync(string moduleCode, string pageCode)
        {
            await _repo.RemoveAsync(RoleId, moduleCode, pageCode);
            return RedirectToPage(new { RoleId });
        }
    }
}
