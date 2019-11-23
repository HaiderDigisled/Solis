using Microsoft.AspNetCore.Mvc;
using System;
using Services;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data.DTO;
using Hangfire;
using System.Net;

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

        [Route("createrecurringjob")]
        [HttpPost]
        public void recover([FromBody]RecurringJobDTO job)
        {
            RecurringJob.AddOrUpdate(job.Name,() => JobBuilder.TriggerUserJob(
                job.BaseUrl, job.Method, job.Model),job.CronExpression);
        }

    }
}
