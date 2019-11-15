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
        public GraphRepository() {
            dapper = new DapperManager();
        }
        public void InsertGraphStats(List<APISuccessResponses> apiresponse)
        {
            var plantDayList = dapper.Execute<bool>(StoredProcedures.PopulateAPISuccessResponses, apiresponse, null, true, null, CommandType.StoredProcedure);
        }
        public IEnumerable<APISuccessResponses> GetGraphResponses(string provider)
        {
            Console.WriteLine($"Getting UnMapped API Responses for {provider}");
            string query = $"select * from APISuccessResponses where Provider ='{provider}' and Mapped=0";
            return dapper.Query<APISuccessResponses>(query, null, null, true, null, System.Data.CommandType.Text);
        }
        public void InsertGraph(IEnumerable<EnergyGraph> graph)
        {
            Console.WriteLine("Inserting Records in EnergyGraph");
            string spname = "InsertEnergyGraphValues";
            dapper.Execute<bool>(spname, graph, null, true, null, System.Data.CommandType.StoredProcedure);

            Console.WriteLine("Records Inserted in EnergyGraph");

            Console.WriteLine("Marking Mapped Responses as 1");
            string plantids = string.Join(",", graph.Select(x => x.plantid).ToList());
            string query = $"update APISuccessResponses set Mapped=1 where plantId IN ({plantids})";
            dapper.Execute<bool>(query, null, null, true, null, System.Data.CommandType.Text);

            Console.WriteLine("Marked Mapped Responses as 1");
        }
    }
}
