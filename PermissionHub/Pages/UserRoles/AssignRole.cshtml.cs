using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PermissionHub.Models;

namespace PermissionHub.Pages.UserRoles
{
    public class AssignModel : PageModel
    {
        private readonly RoleRepository _roleRepository;
        private readonly UserRoleRepository _userRoleRepository;

        public AssignModel(
            RoleRepository roleRepository,
            UserRoleRepository userRoleRepository)
        {
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        [BindProperty(SupportsGet = true)]
        public Guid UserId { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid CompanyId { get; set; }

        [BindProperty]
        public Guid RoleId { get; set; }

        public List<Role> Roles { get; set; } = new();

        public async Task OnGetAsync()
        {
            Roles = await _roleRepository
                .GetByCompanyIdAsync(CompanyId);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _userRoleRepository
                .AssignRoleAsync(UserId, RoleId);

            return RedirectToPage(
                "/Users/Details",
                new { id = UserId });
        }
    }
}