using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Contracts.GrowWatt;
using Services.Mediator.Providers.Vendors;

namespace Services.Mediator.Factory.Vendors
{
    public class GrowWattFactory : VendorFactory
    {
        private readonly IGraphRepository _graph;
        private readonly IGrowWattRepository _growWatt;
        public GrowWattFactory(IGraphRepository graph,IGrowWattRepository growWatt) {
            _graph = graph;
            _growWatt = growWatt;
        }
        public override VendorBase Create()
        {
            return new GrowWattProvider(_graph,_growWatt);
        }
    }
}
