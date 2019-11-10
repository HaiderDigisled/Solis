using Data.Contracts;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DailyJob
    {
        private readonly IGraphRepository _graph;
        public DailyJob() {
            _graph = new GraphRepository();
        }
        public void Start() {
            
        }
    }
}
