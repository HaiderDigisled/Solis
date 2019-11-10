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
        public JobController() {
            
        }

        [Route("initiate")]
        [HttpGet]
        public void Index() {
            DailyJob tp = new DailyJob();
            t = new Thread(tp.Start);
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
