using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Mail;
using WebApp.Data.Entities;
using WebApp.Data.Models;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace WebApp.Pages.Auth;
public class IndexModel : PageModel
{
    private readonly UserManager<IdentityAppUser> _userManager;
    private readonly SignInManager<IdentityAppUser> _signInManager;

    public IndexModel(UserManager<IdentityAppUser> userManager, SignInManager<IdentityAppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [BindProperty]
    public Credentials Credentials { get; set; }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostLoginAsync()
    {
        var user = await _userManager.FindByEmailAsync(Credentials.Identity);

        if (user == null)
        {
            ModelState.AddModelError("Login", "Failed to login");
            return Page();
        }

        var result = await SignIn(user);


        if (result.Succeeded) return RedirectToPage("/index");

        ModelState.AddModelError("Login", result.IsLockedOut ? "You are locked out" : "Failed to login");

        return Page();


    }

    public async Task<IActionResult> OnPostRegisterAsync()
    {
        if (!ModelState.IsValid) return Page();

        if (Credentials.Secret != Credentials.RepeatSecret)
        {
            ModelState.AddModelError("PasswordRepeat", "Passwords do not match");
            return Page();
        }

        var email = new MailAddress(Credentials.Identity);

        var alias = new Alias
        {
            Name = Credentials.Alias,
            Domain = email.Host
        };

        var user = new IdentityAppUser
        {
            Email = email.Address,
            UserName = Credentials.Alias,
        };

        user.Aliases.Add(alias);

        var result = await _userManager.CreateAsync(user, Credentials.Secret);

        if (result.Succeeded)
        {
            await SignIn(user);
            return RedirectToPage("/index");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("Register", error.Description);
            }

            return Page();
        }

    }

    private async Task<SignInResult> SignIn(IdentityAppUser user)
    {
        return await _signInManager
            .PasswordSignInAsync(user, Credentials.Secret, Credentials.RememberMe, false);
    }
}
