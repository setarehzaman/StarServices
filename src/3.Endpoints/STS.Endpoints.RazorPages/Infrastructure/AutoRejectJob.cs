using STS.Domain.AppService.Feature;
using STS.Domain.Core.Contracts.AppService;

namespace STS.Endpoints.RazorPages.Infrastructure
{
    public class AutoRejectJob
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public AutoRejectJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task AutoReject(CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var reminderService = scope.ServiceProvider.GetRequiredService<IOrderAppService>();
            await reminderService.AutoReject(cancellationToken);
        }
    }
}
