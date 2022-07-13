using RocketInsights.Contextual.Services;
using RocketInsights.DXP.Models;
using RocketInsights.DXP.Services;
using System.Threading.Tasks;

namespace RocketInsights.Examples.RestfulAPI.Services
{
    public class LayoutService : ILayoutService
    {
        private IContextService ContextService { get; }

        public LayoutService(IContextService contextService)
        {
            ContextService = contextService;
        }

        public Task<Composition> GetCompositionAsync()
        {
            var composition = new Composition();

            if (ContextService.TryGetContext(out var context))
            {
                composition.Name = context.Culture.DisplayName;
            }

            return Task.FromResult(composition);
        }
    }
}
