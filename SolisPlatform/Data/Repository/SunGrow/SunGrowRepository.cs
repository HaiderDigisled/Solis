using Dapper;
using Data.Contracts.SunGrow;
using Data.Dapper;
using Data.DTO;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.SunGrow
{
    public class SunGrowRepository : ISunGrowRepository
    {
        DapperManager dapper;
        public SunGrowRepository()
        {
            dapper = new DapperManager();
        }
        public IEnumerable<int> GetSunGrowPlants()
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@FromTable", "SunGrowPlantInformation");
                parameters.Add("@ColumnName", "PowerStationId");
                var plants = dapper.Get<SunGrowPlantInformation>(StoredProcedures.GetPlants, parameters, null, true, null, System.Data.CommandType.StoredProcedure);
                return plants.Select(x => x.PowerStationId);
            }
            catch (Exception ex) {
                ex.Data["MethodAndClass"] = "GetSunGrowPlants() in SunGrowRepository";
                throw ex;
            }
            
        }
    }
}
