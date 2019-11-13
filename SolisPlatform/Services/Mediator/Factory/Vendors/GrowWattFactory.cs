using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contracts;
using Services.Mediator.Providers.Vendors;

namespace Services.Mediator.Factory.Vendors
{
    public class GrowWattFactory : VendorFactory
    {
        private readonly IGraphRepository _graph;
        public GrowWattFactory(IGraphRepository graph) {
            _graph = graph;
        }
        public override VendorBase Create()
        {
            return new GrowWattProvider(_graph);
        }
    }
}
