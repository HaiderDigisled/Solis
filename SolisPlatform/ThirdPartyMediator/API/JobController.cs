using Microsoft.AspNetCore.Mvc;
using System;
using Services;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThirdPartyMediator.API
{
    [Route("api/job")]
    public class JobController
    {
        Thread t;
        private readonly IServiceHandler _handler;
        public JobController(IServiceHandler handler) {
            _handler = handler;
        }

        [Route("initiate")]
        [HttpGet]
        public void Index() {
            
            t = new Thread(_handler.ThirdPartyHandler);
            t.Start();
        }

        [Route("recoverplants")]
        [HttpPost]
        public void recover()
        {
            //DailyJob tp = new DailyJob();
            //t = new Thread(tp.Start);
            //t.Start();
        }

    }
}
