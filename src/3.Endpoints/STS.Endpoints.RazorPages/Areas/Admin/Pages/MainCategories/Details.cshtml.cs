using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace STS.Endpoints.RazorPages.Areas.Admin.Pages.MainCategories
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
