using Data.Contracts;
using Data.Dapper;
using Data.DTO;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class GraphRepository : IGraphRepository
    {
        DapperManager dapper;
        public GraphRepository()
        {
            dapper = new DapperManager();
        }
        public void InsertGraphStats(List<APISuccessResponses> apiresponse)
        {
            try
            {
                var plantDayList = dapper.Execute<bool>(StoredProcedures.PopulateAPISuccessResponses, apiresponse, null, true, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ex.Data["MethodAndClass"] = "InsertGraphStats() in GraphRepository";
                throw ex;
            }

        }
        public IEnumerable<APISuccessResponses> GetGraphResponses(string provider)
        {
            Console.WriteLine($"Getting UnMapped API Responses for {provider}");
            string query = $"select * from APISuccessResponses with (nolock) where Provider ='{provider}' and Mapped=0";
            try
            {
                return dapper.Query<APISuccessResponses>(query, null, null, true, null, System.Data.CommandType.Text);
            }
            catch (Exception ex)
            {
                ex.Data["MethodAndClass"] = "GetGraphResponses() in GraphRepository";
                throw ex;
            }

        }
        public void InsertGraph(IEnumerable<EnergyGraph> graph)
        {
            try
            {
                Console.WriteLine("Inserting Records in EnergyGraph");
                string spname = "InsertEnergyGraphValues";
                dapper.Execute<bool>(spname, graph, null, true, null, System.Data.CommandType.StoredProcedure);
                Console.WriteLine("Records Inserted in EnergyGraph");

                Console.WriteLine("Marking Mapped Responses as 1");
                string plantids = string.Join(",", graph.Select(x => x.plantid).ToList().Distinct());
                plantids = string.Concat("'",plantids.Replace(",","','"),"'");
                string query = $"update APISuccessResponses set Mapped=1 where Mapped=0 and plantId IN ({plantids})";
                dapper.Execute<bool>(query, null, null, true, null, System.Data.CommandType.Text);

                Console.WriteLine("Marked Mapped Responses as 1");
            }
            catch (Exception ex)
            {
                ex.Data["MethodAndClass"] = "InsertGraph() in GraphRepository";
                throw ex;
            }

        }

    }
   
}
