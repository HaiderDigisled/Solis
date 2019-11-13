using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contracts;
using Services.Mediator.Providers.Vendors;

namespace Services.Mediator.Factory.Vendors
{
    public class SunGrowFactory : VendorFactory
    {
        private readonly IGraphRepository _graph;
        public SunGrowFactory(IGraphRepository graph) { _graph = graph; }
        public override VendorBase Create()
        {
            return new SunGrowProvider(_graph);
        }
    }
}
