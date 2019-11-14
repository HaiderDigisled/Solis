using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Contracts.SunGrow;
using Services.Mediator.Providers.Vendors;

namespace Services.Mediator.Factory.Vendors
{
    public class SunGrowFactory : VendorFactory
    {
        private readonly IGraphRepository _graph;
        private readonly ISunGrowRepository _sungrow;
        public SunGrowFactory(IGraphRepository graph, ISunGrowRepository sunGrow)
        {
            _graph = graph;
            _sungrow = sunGrow;
        }
        public override VendorBase Create()
        {
            return new SunGrowProvider(_graph,_sungrow);
        }
    }
}
