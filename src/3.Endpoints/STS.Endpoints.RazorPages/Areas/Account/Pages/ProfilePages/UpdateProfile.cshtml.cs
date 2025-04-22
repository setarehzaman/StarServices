using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.ApplicationUser;
using STS.Domain.Core.Dtos.TaskItemDtos;
using STS.Endpoints.RazorPages.Extensions;

namespace STS.Endpoints.RazorPages.Areas.Account.Pages.ProfilePages
{
    [Authorize(Roles = "Client,Expert")]
    public class UpdateProfileModel(IApplicationUserAppService applicationUserAppService,
        ITaskItemAppService taskItemAppService, IExpertAppService expertAppService) : PageModel
    {
        [BindProperty]
        public ApplicationUserDto UserModel { get; set; }

        [BindProperty]
        public List<int> SelectedSkills { get; set; } = new List<int>();

        [BindProperty]
        public IEnumerable<TaskItemDto> AllSkills { get; set; } = new List<TaskItemDto>();

        [BindProperty]
        public List<int>? ExpertSkills { get; set; }


        public async Task OnGet(CancellationToken cancellationToken)
        {
            TempData["Show-pencil"] = true;

            var userId = User.GetUserId();
            var result = await applicationUserAppService.GetBy(int.Parse(userId), cancellationToken);
            UserModel = result.Data!;

            var resultSkills = await taskItemAppService.GetAll(cancellationToken);
            AllSkills = resultSkills.Data!;

            if (User.IsInRole("Expert"))
            {
                var expertId = int.Parse(User.GetExpertId());

                var resultExpert = await expertAppService.GetBy(expertId, cancellationToken);
                var expert = resultExpert.Data!;

                if (expert.Skills is not null)
                {
                    ExpertSkills = expert.Skills!.Select(s => s.TaskId).ToList();
                }
            }
        }

        public async Task<IActionResult> OnPost(IFormFile profilePic, CancellationToken cancellationToken)
        {
            UserModel.ProfileImage = profilePic is null ? null : profilePic;

            var expertId = int.Parse(User.GetExpertId());

            var resultExpert = await expertAppService.GetBy(expertId, cancellationToken);
            var expert = resultExpert.Data!;

            if (ExpertSkills is not null) 
            {
                await expertAppService.UpdateSkills(expertId, ExpertSkills, cancellationToken);
            }

            await applicationUserAppService.Update(UserModel, cancellationToken);

            return RedirectToPage("Profile");
        }

    }
}
