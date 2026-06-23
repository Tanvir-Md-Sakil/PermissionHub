using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace PermissionHub.Pages.Account
{

    public class LoginModel : PageModel
    {
        private readonly UserRepository _repo;

        public LoginModel(UserRepository repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPost()
        {
            var user = await _repo.GetByEmail(Email);

            if (user == null)
            {
                Console.WriteLine("USER NOT FOUND");
                return Page();
            }

            if (!PasswordHelper.Verify(Password, user.PasswordHash))
            {
                Console.WriteLine("PASSWORD WRONG");
                return Page();
            }

            Console.WriteLine("LOGIN SUCCESS");

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.FullName)
    };

            var identity = new ClaimsIdentity(claims, "Cookies");

            await HttpContext.SignInAsync(
                "Cookies",
                new ClaimsPrincipal(identity));

            return RedirectToPage("/Dashboard/Index");
        }
    }
}
