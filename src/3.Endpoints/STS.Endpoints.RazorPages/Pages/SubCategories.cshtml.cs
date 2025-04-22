using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.Category;
using STS.Domain.Core.Dtos.TaskItemDtos;

namespace STS.Endpoints.RazorPages.Pages
{
    public class SubCategoriesModel(ISubCategoryAppService subCategoryAppService,
        ITaskItemAppService taskItemAppService) : PageModel
    {
        [BindProperty] 
        public IEnumerable<SubCategoryDto> SubCategories { get; set; }

        [BindProperty] 
        public Dictionary<int, IEnumerable<TaskItemDto>> TaskItems { get; set; } 
            = new Dictionary<int, IEnumerable<TaskItemDto>>();

        [BindProperty] 
        public string MainCategoryName { get; set; }
        
        public async Task OnGet(int id, CancellationToken cancellationToken)
        {

            var result = await subCategoryAppService.GetAllByMainCategoryId(id,cancellationToken);
            SubCategories = result.Data;

            if (SubCategories.Any())
            {
                MainCategoryName = SubCategories.First().MainCategoryName;

                foreach (var subCategory in SubCategories)
                {
                    var taskItemResult = await taskItemAppService.GetAllBySubCategoryId(subCategory.Id, cancellationToken);

                    TaskItems.Add(subCategory.Id, taskItemResult.Data!);
                }
            }
        }
    }
}
