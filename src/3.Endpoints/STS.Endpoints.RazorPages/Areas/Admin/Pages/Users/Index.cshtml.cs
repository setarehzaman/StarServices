using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.AppService.Feature;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.ApplicationUser;

namespace STS.Endpoints.RazorPages.Areas.Admin.Pages.Users
{
    //[Authorize(Roles = "Admin")]
    public class IndexModel(IApplicationUserAppService userAppService) : PageModel
    {
        [BindProperty]
        public List<ApplicationUserDto> Users { get; set; }

        public async Task OnGet(CancellationToken cancellationToken)
        {
            var result = await userAppService.GetAll(cancellationToken);
            Users = result.Data!;
        }


        [HttpGet]
        public async Task<IActionResult> OnGetDelete(int id, CancellationToken cancellationToken)
        {
            var result = await userAppService.SoftDelete(id, cancellationToken);

            if (result.IsSuccess) return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty, result.Message!);
            return Page();
        }

        [HttpGet]
        public async Task<IActionResult> OnGetAccept(int id, CancellationToken cancellationToken)
        {
            var result = await userAppService.Accept(id, cancellationToken);

            if (result.IsSuccess) return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty, result.Message!);
            return Page();
        }

        [HttpGet]
        public async Task<IActionResult> OnGetReject(int id, CancellationToken cancellationToken)
        {
            var result = await userAppService.Reject(id, cancellationToken);

            if (result.IsSuccess) return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty, result.Message!);
            return Page();
        }
    }
}