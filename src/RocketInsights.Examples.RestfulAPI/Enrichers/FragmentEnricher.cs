using RocketInsights.Common.Patterns.Pipelines;
using RocketInsights.DXP.Models;
using System;
using System.Threading.Tasks;

namespace RocketInsights.Examples.RestfulAPI.Enrichers
{
    public class FragmentEnricher : ConditionalOperation<Fragment>
    {
        protected override Func<Fragment, bool> Condition => (f) => f.Template.Name.Equals("body-content");

        protected override Task<Fragment> ConditionalInvokeAsync(Fragment input)
        {
            input.Name = String.Concat(input.Name, " (enriched as fragment)");

            return Task.FromResult(input);
        }
    }
}
