using Services.Mediator.Providers.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mediator.Factory.Vendors
{
    public abstract class VendorFactory
    {
        public abstract VendorBase Create(); 
    }
}
