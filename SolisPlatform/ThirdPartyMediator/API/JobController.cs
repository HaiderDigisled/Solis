using Microsoft.AspNetCore.Mvc;
using System;
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
            
        }

        
    }
}
