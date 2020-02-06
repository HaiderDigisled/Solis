using Data.Contracts;
using Data.Contracts.GoodWee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mediator.Providers.Vendors
{
    public class GoodWeeProvider : VendorBase
    {
        private readonly IGraphRepository _graph;
        private readonly IMiscRepository _misc;
        private readonly IGoodWeeRepository _goodWee;
        private IEnumerable<string> PlantIds;

        public GoodWeeProvider(IGraphRepository graph, IGoodWeeRepository goodWee, IMiscRepository misc) {
            _graph = graph;
            _goodWee = goodWee;
            _misc = misc;
        }

        public override void CalculateRanking()
        {
            
        }

        public override void CheckDeviceFaults()
        {
            
        }

        public override void GetPlants()
        {
            PlantIds = _goodWee.GetGoodWeePlants();
        }

        public override void SaveAPIResponses()
        {
            
        }

        public override void SaveEnergyGraph(string VendorName)
        {
            
        }

        public override void UpdatePlantsStatus()
        {
            
        }
    }
}
