using Data.Contracts;
using Data.Dapper;
using Data.Model;
using System;
using System.Collections.Generic;
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
        
    }
}
