using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Endpoints.RazorPages.Extensions;
using System.ComponentModel.DataAnnotations;
using STS.Domain.Core.Contracts.AppService;

namespace STS.Endpoints.RazorPages.Areas.Account.Pages
{
    public class LoginModel(IApplicationUserAppService userAppService) : PageModel
    {

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            var result = await userAppService.Login(Input.Email, Input.Password);

            if (result.Data!.Succeeded)
            {
                switch (UserUtility.GetRole(User))
                {
                    case "Admin":
                        return LocalRedirect("~/Admin/Index");

                    case "Client":
                        return RedirectToPage("./Index");

                    case "Expert":
                        return RedirectToPage("./ExpertIndex");
                }
            }
            else
            {
                if (result.Data!.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "یوزر شما غیر فعال می باشد * .");
                    return Page();
                }

                ModelState.AddModelError(string.Empty, "نام کاربری یا کلمه عبور اشتباه است * .");

                return Page();
            }

            return Page();
        }
    }
}
