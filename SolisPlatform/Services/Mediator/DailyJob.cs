using Data.Contracts;
using Data.Contracts.GrowWatt;
using Data.Contracts.SunGrow;
using Data.Repository;
using Foundation.AlertConfiguration;
using Services.Mediator.Factory.Vendors;
using Services.Mediator.Providers.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Mediator
{
    public class DailyJob : IDailyJob
    {
        private readonly IGraphRepository _graph;
        private readonly IVendorRepository _vendors;
        private readonly IGrowWattRepository _growWatt;
        private readonly ISunGrowRepository _sunGrow;
        private readonly IMiscRepository _misc;

        private VendorFactory Factory;
        private VendorBase Vendor;

        public DailyJob(IVendorRepository vendors, IGraphRepository graph, ISunGrowRepository sunGrow, IGrowWattRepository growWatt, IMiscRepository misc)
        {
            _vendors = vendors;
            _graph = graph;
            _sunGrow = sunGrow;
            _growWatt = growWatt;
            _misc = misc;
        }

        public void Start()
        {
            Console.WriteLine("***************Daily Job Started***************");
            try
            {
                var allvendors = _vendors.GetVendors();
                foreach (var vendor in allvendors)
                {
                    switch (vendor.Name)
                    {
                        case "GrowWatt":
                            Factory = new GrowWattFactory(_graph, _growWatt, _misc);
                            break;
                        case "SunGrow":
                            Factory = new SunGrowFactory(_graph, _sunGrow, _misc);
                            break;
                    }

                    Vendor = Factory.Create();

                    #region Energy Graph Recovery
                    Vendor.GetPlants();
                    Vendor.SaveAPIResponses();
                    Vendor.SaveEnergyGraph(vendor.Name);
                    Vendor.CalculateRanking();  // TODO : Refactoring Needed for CalculateRanking, Create New Repo for Ranking and move all misc repo code to Ranking Repo
                    Vendor.UpdatePlantsStatus();
                    Vendor.CheckDeviceFaults();
                    #endregion

                }

            }
            catch (Exception ex)
            {
                new FailureAlerts().SendEmail(ex.Data["MethodAndClass"].ToString(), ex.Message);
            }

            Console.WriteLine("***************Daily Job End***************");
        }
    }
}
