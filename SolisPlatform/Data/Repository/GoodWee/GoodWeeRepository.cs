using Dapper;
using Data.Contracts.GoodWee;
using Data.Dapper;
using Data.DTO;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.GoodWee
{
    public class GoodWeeRepository : IGoodWeeRepository
    {
        DapperManager dapper;
        public GoodWeeRepository()
        {
            dapper = new DapperManager();
        }

        public IEnumerable<PlantInformation> GetGoodWeePlants()
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@FromTable", "PlantInformation");
                parameters.Add("@ColumnName", "PlantId,CreatedTime");

                var plants = dapper.Get<PlantInformation>(StoredProcedures.GetPlants, parameters, null, true, null, System.Data.CommandType.StoredProcedure);
                return plants;
            }
            catch (Exception ex)
            {
                ex.Data["MethodAndClass"] = "GetGoodWeePlants() in GoodWeeRepository";
                throw ex;
            }
        }

        public void MarkPlantHistoric(string PlantId)
        {
            try
            {
                string query = $"update PlantInformation set IsHistoric=1 where PlantId= '{PlantId}'";
                dapper.Execute<int>(query,null,null,true,null,System.Data.CommandType.Text);
            }
            catch (Exception ex) {
                ex.Data["MethodAndClass"] = "MarkPlantHistoric() in GoodWeeRepository";
                throw ex;
            }
        }
    }
}
