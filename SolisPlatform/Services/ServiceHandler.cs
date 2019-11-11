using Services.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceHandler : IServiceHandler
    {
        private readonly IDailyJob _job;
        public ServiceHandler(IDailyJob job) {
            _job = job;
        }

        public void ThirdPartyHandler()
        {
            _job.Start();
        }
    }
}
