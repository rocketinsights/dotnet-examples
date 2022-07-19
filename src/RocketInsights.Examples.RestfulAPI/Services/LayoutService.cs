using RocketInsights.Common.Extensions;
using RocketInsights.Common.Models;
using RocketInsights.Contextual.Services;
using RocketInsights.DXP.Models;
using RocketInsights.DXP.Services;
using System;
using System.Collections.Generic;
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
            if (ContextService.TryGetContext(out var context))
            {
                if (context.Content.TryParse<Uri>("uri", out var uri))
                {
                    var composition = new Composition()
                    {
                        Content = new Content()
                        {
                            { "slug", uri.AbsolutePath }
                        },
                        Regions = new List<Region>()
                        {
                            new Region()
                            {
                                Name = "Test Region",
                                Template = new Template()
                                {
                                    Name = "layout"
                                },
                                Fragments = new List<Fragment>()
                                {
                                    new Fragment()
                                    {
                                        Name = "Test Fragment",
                                        Template = new Template()
                                        {
                                            Name = "body-content"
                                        },
                                        Content = new Content()
                                        {
                                            { "title", "Lorum Ipsum" },
                                            { "publishedDate", DateTime.Now },
                                            { "body", @"# Illa rata natura

        ## Diva cum aliquod ergo

        Lorem *markdownum curvi* ipsos haerentem optas medeatur sua tenentibus contigit
        lacrimis torpet siccis. Iuratus quaeris, ego fuerunt multiplicique restitit, non
        *lenta tremit*! Avem dolore! Solet est ponto sedit mox sui nomine Belo ecce
        pulsumque, caput rigebant celanda amaris Leucothoen omnia,
        [dederint](http://tympanaque.io/quibus-maneas.html)." }
                                        }
                                    }
                                }
                            }
                        }
                    };

                    return Task.FromResult(composition);
                }
            }

            throw new Exception("Not Found");
        }
    }
}
