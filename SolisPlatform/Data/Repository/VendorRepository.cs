﻿using Data.Contracts;
using Data.Dapper;
using Data.DTO;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class VendorRepository :  IVendorRepository
    {
        DapperManager dapper;
        public VendorRepository() {
            dapper = new DapperManager();
        }

        public IEnumerable<Vendors> GetVendors()
        {
            //var vendors = dapper.Get<Vendors>(StoredProcedures.GetVendors,null,null,true,null,System.Data.CommandType.StoredProcedure);
            try
            {
                var vendors = dapper.Get<Vendors>("select * from VendorDetails with (nolock)", null, null, true, null, System.Data.CommandType.Text);
                return vendors;
            }
            catch (Exception ex) {
                ex.Data["MethodAndClass"] = "GetVendors() in VendorRepository";
                throw ex;
            }
        }
    }
}
