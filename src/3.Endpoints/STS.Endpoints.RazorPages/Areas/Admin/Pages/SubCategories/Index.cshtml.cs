using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.Category;

namespace STS.Endpoints.RazorPages.Areas.Admin.Pages.SubCategories
{
    [Authorize(Roles = "Admin")]
    public class IndexModel(ISubCategoryAppService subCategoryAppService) : PageModel
    {
        [BindProperty]
        public IEnumerable<SubCategoryDto> SubCategories { get; set; } = new List<SubCategoryDto>();    
        public async Task OnGet(CancellationToken cancellationToken)
        {
            var result = await subCategoryAppService.GetAll(cancellationToken);
            SubCategories = result.Data!;
        }
        [HttpGet]
        public async Task<IActionResult> OnGetDelete(int id, CancellationToken cancellationToken)
        {
            var result = await subCategoryAppService.SoftDelete(id, cancellationToken);

            if (result.IsSuccess) return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty, result.Message!);
            return Page();
        }
    }
}
