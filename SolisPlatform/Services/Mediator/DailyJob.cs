using Data.Contracts;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Mediator
{
    public class DailyJob :  IDailyJob
    {
        private readonly IGraphRepository _graph;
        public DailyJob(IGraphRepository graph) {
            _graph = graph;
        }
        public void Start() {
            
        }
    }
}
