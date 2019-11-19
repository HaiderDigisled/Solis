using Data.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ThirdPartyMediator.API.JobsManagment;

namespace ThirdPartyMediator.API
{
    public class JobBuilder
    {
        public async static Task<bool> TriggerUserJob(string endPoint, string serviceActions, string serviceParameters)
        {
            try
            {
                var ReqClient = new ApiClient(new Uri(endPoint));
                var requestUrl = ReqClient.CreateRequestUri(string.Format(CultureInfo.InvariantCulture, serviceActions /*"Hangfire/SaveJob"*/));
                //var serviceParams = new ServiceParams() { Params = serviceParameters, ServiceInfo = requestUrl.ToString() };
                await ReqClient.GetAsync<RecurringJobDTO>(requestUrl);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
