using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mediator.Providers.Vendors
{
    public abstract class VendorBase
    {
        public abstract void GetPlants();
        public abstract void SaveAPIResponses();
        public abstract void SaveEnergyGraph(string VendorName);
    }
}
