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
        private readonly IMiscRepository _misc;
        public SunGrowFactory(IGraphRepository graph, ISunGrowRepository sunGrow, IMiscRepository misc)
        {
            _graph = graph;
            _sungrow = sunGrow;
            _misc = misc;
        }
        public override VendorBase Create()
        {
            return new SunGrowProvider(_graph,_sungrow,_misc);
        }
    }
}
