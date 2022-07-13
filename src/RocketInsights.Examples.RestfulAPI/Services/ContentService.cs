using RocketInsights.Contextual.Services;
using RocketInsights.DXP.Models;
using RocketInsights.DXP.Services;
using System.Threading.Tasks;

namespace RocketInsights.Examples.RestfulAPI.Services
{
    public class ContentService : IContentService
    {
        private IContextService ContextService { get; }

        public ContentService(IContextService contextService)
        {
            ContextService = contextService;
        }

        public Task<Fragment> GetFragmentAsync(string id)
        {
            var fragment = new Fragment() { Id = id };

            if (ContextService.TryGetContext(out var context))
            {
                fragment.Name = context.Culture.DisplayName;
            }

            return Task.FromResult(fragment);
        }
    }
}
