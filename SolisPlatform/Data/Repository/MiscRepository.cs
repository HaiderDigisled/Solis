using Dapper;
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
    public class MiscRepository : IMiscRepository
    {
        DapperManager dapper; 
        public MiscRepository() {
            dapper = new DapperManager();
        }
        public IEnumerable<RankingCalculationViewDTO> CalculateRanking(IEnumerable<string> PlantIds,string Provider)
        {
            var plantids = string.Join(",", PlantIds.ToList());
            plantids = string.Concat("'",plantids.Replace(",","','"),"'");
            string query = $"select * from RankingCalculationView with (nolock) where PlantId in ({plantids})";
            try
            {
                var result = dapper.Get<RankingCalculationViewDTO>(query, null, null, true, null, System.Data.CommandType.Text);
                Console.WriteLine($"{Provider} : Plants Details Fetched for RankingCalculation");
                return result;
            }
            catch (Exception ex) {
                ex.Data["MethodAndClass"] = "CalculateRanking() in MiscRepository";
                throw ex;
            }
        }

        public IEnumerable<RankingCalculationViewDTO> GetPlantsCapacity(string TableName,string Field,string Filter, IEnumerable<string> PlantIds) {
            var plantids = string.Join(",", PlantIds.ToList());
            plantids = string.Concat("'", plantids.Replace(",", "','"), "'");

            string query = $"select {Filter} as PlantId,{Field} as PlantCapacity from {TableName} with (nolock) where {Filter} in ({plantids})";
            try
            {
                var result = dapper.Get<RankingCalculationViewDTO>(query, null, null, true, null, System.Data.CommandType.Text);
                Console.WriteLine($"Plants Capacity Retrieved");
                return result;
            }
            catch (Exception ex)
            {
                ex.Data["MethodAndClass"] = "GetPlantsCapacity() in MiscRepository";
                throw ex;
            }
        }

        public void FinalRanking(IEnumerable<Ranking> ranking)
        {
            var plantids = string.Join(",", ranking.Select(x => x.PlantId).ToList());
            plantids = string.Concat("'", plantids.Replace(",", "','"), "'");

            string query = $"delete from RankingTable where PlantId in ({plantids}) and Convert(nvarchar(20),CreatedOn,112) Like '%{DateTime.Now.ToString("yyyyMM")}%'";
            

            try
            {
                dapper.Execute<bool>(query, null, null, true, null, System.Data.CommandType.Text);
                dapper.Execute<bool>(StoredProcedures.CalculateRanking,ranking, null, true, null, System.Data.CommandType.StoredProcedure);
                Console.WriteLine("Ranks Calculated");
            }
            catch (Exception ex)
            {
                ex.Data["MethodAndClass"] = "FinalRanking() in MiscRepository";
                throw ex;
            }
        }
    }
}
