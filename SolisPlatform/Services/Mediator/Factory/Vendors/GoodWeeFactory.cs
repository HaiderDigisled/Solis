using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Contracts.GoodWee;
using Services.Mediator.Providers.Vendors;

namespace Services.Mediator.Factory.Vendors
{
    public class GoodWeeFactory : VendorFactory
    {
        private readonly IGraphRepository _graph;
        private readonly IMiscRepository _misc;
        private readonly IGoodWeeRepository _goodWee;

        public GoodWeeFactory(IGraphRepository graph, IGoodWeeRepository goodWee, IMiscRepository misc)
        {
            _graph = graph;
            _goodWee = goodWee;
            _misc = misc;
        }

        public override VendorBase Create()
        {
            return new GoodWeeProvider(_graph,_goodWee,_misc);
        }
    }
}
