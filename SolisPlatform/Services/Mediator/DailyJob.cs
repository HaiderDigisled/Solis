using Data.Contracts;
using Data.Repository;
using Services.Mediator.Factory.Vendors;
using Services.Mediator.Providers.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Mediator
{
    public class DailyJob :  IDailyJob
    {
        private readonly IGraphRepository _graph;
        private readonly IVendorRepository _vendors;

        private VendorFactory Factory;
        private VendorBase Vendor;

        public DailyJob(IVendorRepository vendors,IGraphRepository graph) {
            _vendors = vendors;
            _graph = graph;
        }

        public void Start() {

            var allvendors = _vendors.GetVendors();
            foreach (var vendor in allvendors) {
                switch (vendor.Name) {
                    case "GrowWatt":
                        Factory = new GrowWattFactory(_graph);
                        break;
                    case "SunGrow":
                        Factory = new SunGrowFactory(_graph);
                        break;
                }

                Vendor = Factory.Create();
                Vendor.RecoverGraphData();

            }
        }
    }
}
