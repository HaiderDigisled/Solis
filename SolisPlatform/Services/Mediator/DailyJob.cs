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
        private VendorFactory Factory;
        private VendorBase Vendor;
        public DailyJob(IGraphRepository graph) {
            _graph = graph;
        }
        public void Start() {
            string[] arr = { "g", "s" };
            foreach (var i in arr) {
                switch (i) {
                    case "g":
                        Factory = new GrowWattFactory();
                        break;
                    case "s":
                        Factory = new SunGrowFactory();
                        break;
                }

                Vendor = Factory.Create();
                Vendor.RecoverGraphData();

            }
        }
    }
}
