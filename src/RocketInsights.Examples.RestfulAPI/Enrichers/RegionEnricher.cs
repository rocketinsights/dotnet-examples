using RocketInsights.Common.Patterns.Pipelines;
using RocketInsights.DXP.Models;
using System;
using System.Threading.Tasks;

namespace RocketInsights.Examples.RestfulAPI.Enrichers
{
    public class RegionEnricher : ConditionalOperation<Region>
    {
        protected override Func<Region, bool> Condition => (r) => r.Template.Name.Equals("layout");

        protected override Task<Region> ConditionalInvokeAsync(Region input)
        {
            input.Name = String.Concat(input.Name, " (enriched as region)");

            return Task.FromResult(input);
        }
    }
}
