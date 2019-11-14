using Data.Contracts;
using Data.Contracts.SunGrow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mediator.Providers.Vendors
{
    public class SunGrowProvider : VendorBase
    {
        private readonly IGraphRepository _graph;
        private readonly ISunGrowRepository _sungrow;
        private IEnumerable<int> PlantIds;

        public SunGrowProvider(IGraphRepository graph,ISunGrowRepository sungrow) {
            _graph = graph;
            _sungrow = sungrow;
        }

        public override void GetPlants()
        {
            PlantIds = _sungrow.GetSunGrowPlants();
        }

        public override void SaveEnergyGraph()
        {
            Console.WriteLine("SunGrow Recovery");
        }

        public override void SaveAPIResponses()
        {
            
        }
    }
}
