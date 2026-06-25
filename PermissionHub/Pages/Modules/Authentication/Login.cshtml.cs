using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PermissionHub;

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

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _repo.GetByEmail(Email);

        if (user == null)
        {
            ModelState.AddModelError("", "Invalid login");
            return Page();
        }

        HttpContext.Session.SetString("UserId", user.Id.ToString());

        return RedirectToPage("/Index");
    }
}