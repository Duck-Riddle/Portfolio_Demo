using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Data.Entities;

namespace WebApp.Pages.Auth
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityAppUser> _signInManager;

        public LogoutModel(SignInManager<IdentityAppUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
