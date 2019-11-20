using Dapper;
using Data.Contracts.GrowWatt;
using Data.Dapper;
using Data.DTO;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.GrowWatt
{
    public class GrowWattRepository : IGrowWattRepository
    {
        DapperManager dapper;
        public GrowWattRepository() {
            dapper = new DapperManager();
        }
        public IEnumerable<int> GetGrowWattPlants()
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@FromTable", "GrowWattPlantInformation");
                parameters.Add("@ColumnName", "PlantId");
                var plants = dapper.Get<GrowWattPlantInformation>(StoredProcedures.GetPlants, parameters, null, true, null, System.Data.CommandType.StoredProcedure);
                return plants.Select(x => x.PlantId);
            }
            catch (Exception ex) {
                ex.Data["MethodAndClass"] = "GetSunGrowPlants() in SunGrowRepository";
                throw ex;
            }
        }
    }
}
