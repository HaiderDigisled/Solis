using Data.Contracts;
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
            var vendors = dapper.Get<Vendors>("select * from VendorDetails",null,null,true,null,System.Data.CommandType.Text);
            return vendors;
        }
    }
}
