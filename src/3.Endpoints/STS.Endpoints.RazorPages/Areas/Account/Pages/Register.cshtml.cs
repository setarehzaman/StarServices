using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.ApplicationUser;
using STS.Domain.Core.Entities.BaseEntities;

namespace STS.Endpoints.RazorPages.Areas.Account.Pages
{
    public class RegisterModel (IApplicationUserAppService applicationUserAppService): PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string RePassword { get; set; }
            public int CityId { get; set; }
            public int RoleId { get; set; }

        }
        public async Task OnGet(CancellationToken cancellationToken)
        {
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (Input.Password != Input.RePassword)
            {
                ModelState.AddModelError(string.Empty, "کلمه عبور و تکرار آن برابر نیست ");
                return Page();
            }

            var registerModel = new ApplicationUserDto()
            {
                Email = Input.Email,
                Password = Input.Password,
                CityId = Input.CityId,
                RoleId = Input.RoleId,
            };

            var result = await applicationUserAppService.Register(registerModel, cancellationToken);

            if (result.IsSuccess)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                foreach (var error in result.Data.Errors)
                {
                    ModelState.AddModelError(string.Empty , error.Description);
                    return Page();
                }
            }
            return Page();
        }
    }
}
