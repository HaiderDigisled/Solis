using Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mediator.Providers.Vendors
{
    public class GrowWattProvider : VendorBase
    {
        private readonly IGraphRepository _graph;
        public GrowWattProvider(IGraphRepository graph) {
            _graph = graph;
        }
        public override void RecoverGraphData()
        {
            Console.WriteLine("GrowWatt Recovery");
        }
    }
}
