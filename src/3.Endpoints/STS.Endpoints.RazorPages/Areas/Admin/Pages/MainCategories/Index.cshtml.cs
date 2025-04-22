using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.Category;
using STS.Domain.Core.Entities;

namespace STS.Endpoints.RazorPages.Areas.Admin.Pages.MainCategories
{
    [Authorize(Roles = "Admin")]
    public class IndexModel(IMainCategoryAppService categoryAppService) : PageModel
    {
        [BindProperty]
        public IEnumerable<MainCategoryDto> MainCategories { get; set; } = new List<MainCategoryDto>();
        public async Task OnGet(CancellationToken cancellationToken)
        {
            var result = await categoryAppService.GetAll(cancellationToken);
            MainCategories = result.Data!;
        }

        [HttpGet]
        public async Task<IActionResult> OnGetDelete(int id, CancellationToken cancellationToken)
        {
            var result = await categoryAppService.SoftDelete(id, cancellationToken);

            if (result.IsSuccess) return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty, result.Message!);
            return Page();
        }
    }
}
