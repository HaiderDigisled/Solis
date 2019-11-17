using Data.Contracts;
using Data.Contracts.GrowWatt;
using Data.Contracts.SunGrow;
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
        private readonly IGrowWattRepository _growWatt;
        private readonly ISunGrowRepository _sunGrow;

        private VendorFactory Factory;
        private VendorBase Vendor;

        public DailyJob(IVendorRepository vendors,IGraphRepository graph,ISunGrowRepository sunGrow,IGrowWattRepository growWatt) {
            _vendors = vendors;
            _graph = graph;
            _sunGrow = sunGrow;
            _growWatt = growWatt;
        }

        public void Start() {
            Console.WriteLine("***************Daily Job Started***************");
            var allvendors = _vendors.GetVendors();
            foreach (var vendor in allvendors) {
                switch (vendor.Name) {
                    case "GrowWatt":
                        Factory = new GrowWattFactory(_graph,_growWatt);
                        break;
                    case "SunGrow":
                        Factory = new SunGrowFactory(_graph,_sunGrow);
                        break;
                }

                Vendor = Factory.Create();

                #region Energy Graph Recovery
                Vendor.GetPlants();
                Vendor.SaveAPIResponses();
                Vendor.SaveEnergyGraph(vendor.Name);
                # endregion


                Console.WriteLine("***************Daily Job End***************");
            }
        }
    }
}
