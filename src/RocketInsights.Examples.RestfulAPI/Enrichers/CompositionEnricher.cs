using RocketInsights.Common.Patterns.Pipelines;
using RocketInsights.DXP.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RocketInsights.Examples.RestfulAPI.Enrichers
{
    public class CompositionEnricher : ConditionalOperation<Composition>
    {
        protected override Func<Composition, bool> Condition => (c) => c.Regions.Any();

        protected override Task<Composition> ConditionalInvokeAsync(Composition input)
        {
            input.Name = string.Concat(input.Name, " (enriched as composition)");

            return Task.FromResult(input);
        }
    }
}
