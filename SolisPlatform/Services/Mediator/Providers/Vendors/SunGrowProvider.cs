using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mediator.Providers.Vendors
{
    public class SunGrowProvider : VendorBase
    {
        public override void RecoverGraphData()
        {
            Console.WriteLine("SunGrow Recovery");
        }
    }
}
