using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PermissionHub.Models;

namespace PermissionHub.Pages.UserRoles
{
    public class AssignModel : PageModel
    {
        private readonly UserRepository _userRepository;
        private readonly CompanyRepository _companyRepository;
        private readonly RoleRepository _roleRepository;
        private readonly UserRoleRepository _userRoleRepository;

        public AssignModel(
            UserRepository userRepository,
            CompanyRepository companyRepository,
            RoleRepository roleRepository,
            UserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        // GET state
        [BindProperty(SupportsGet = true)]
        public Guid UserId { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid CompanyId { get; set; }

        // POST only
        [BindProperty]
        public Guid RoleId { get; set; }

        public List<User> Users { get; set; } = new();
        public List<Company> Companies { get; set; } = new();
        public List<Role> Roles { get; set; } = new();

        public async Task OnGetAsync()
        {
            Users = await _userRepository.GetAll();
            Companies = await _companyRepository.GetAllAsync();

            if (CompanyId != Guid.Empty)
            {
                Roles = await _roleRepository.GetByCompanyIdAsync(CompanyId);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (UserId == Guid.Empty ||
                CompanyId == Guid.Empty ||
                RoleId == Guid.Empty)
            {
                ModelState.AddModelError("", "Please select User, Company and Role.");

                await OnGetAsync();
                return Page();
            }

            await _userRoleRepository.AssignRoleAsync(
                UserId,
                CompanyId,
                RoleId);

            return RedirectToPage("/Dashboard/Index");
        }
    }
}