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
    }
}
