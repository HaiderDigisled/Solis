using Data.Contracts;
using Data.Dapper;
using Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class MiscRepository : IMiscRepository
    {
        DapperManager dapper; 
        public MiscRepository() {
            dapper = new DapperManager();
        }
        public void CalculateRanking()
        {
            try
            {
                dapper.Execute<bool>(StoredProcedures.CalculateRanking, null, null, true, null, System.Data.CommandType.StoredProcedure);
                Console.WriteLine("Ranking Calculated");
            }
            catch (Exception ex) {
                ex.Data["MethodAndClass"] = "CalculateRanking() in MiscRepository";
                throw ex;
            }
            
        }
    }
}
