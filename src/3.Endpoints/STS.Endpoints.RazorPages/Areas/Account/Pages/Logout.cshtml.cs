using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.Core.Entities.User;

namespace STS.Endpoints.RazorPages.Areas.Account.Pages
{
    public class LogoutModel(SignInManager<ApplicationUser> signInManager) : PageModel
    {

        public async Task<IActionResult> OnGet()
        {
            await signInManager.SignOutAsync();

            return RedirectToPage("Index");
        }

    }
}
