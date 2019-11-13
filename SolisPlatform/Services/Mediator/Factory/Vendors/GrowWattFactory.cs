﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Mediator.Providers.Vendors;

namespace Services.Mediator.Factory.Vendors
{
    public class GrowWattFactory : VendorFactory
    {
        public override VendorBase Create()
        {
            return new GrowWattProvider();
        }
    }
}