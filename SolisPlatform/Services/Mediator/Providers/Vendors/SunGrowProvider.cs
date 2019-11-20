using Data.Contracts;
using Data.Contracts.SunGrow;
using Data.Enums;
using Data.Mappers;
using Data.Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.Model.LoginRestApiDTO;

namespace Services.Mediator.Providers.Vendors
{
    public class SunGrowProvider : VendorBase
    {
        private readonly IGraphRepository _graph;
        private readonly ISunGrowRepository _sungrow;
        private IEnumerable<int> PlantIds;
        private List<APISuccessResponses> apiresponses;
        private EnergyGraphMapper mapper;

        public SunGrowProvider(IGraphRepository graph,ISunGrowRepository sungrow) {
            _graph = graph;
            _sungrow = sungrow;
            apiresponses = new List<APISuccessResponses>();
            mapper = new EnergyGraphMapper();
        }

        public override void GetPlants()
        {
            PlantIds = _sungrow.GetSunGrowPlants();
        }

        public override void SaveEnergyGraph(string Vendor)
        {
            Console.WriteLine($"Mapping Responses for {Vendor} to EnergyGraph");
            var responses = _graph.GetGraphResponses(Vendor);
            var graphValues = mapper.Map(responses.ToList(), Vendor);
            _graph.InsertGraph(graphValues);
            Console.WriteLine($"Responses Mapped");
        }

        public override void SaveAPIResponses()
        {
            var logindetails = LoginSunGrow();
            if (!String.IsNullOrEmpty(logindetails.Token))
            {
                GetPlantGraph(logindetails.UserId, logindetails.Token);
                ReconcileAPIResponses();
            }
        }

        private SunGrowLoginModel LoginSunGrow()
        {
            SunGrowLoginModel obj = new SunGrowLoginModel();
            var client = new RestClient("https://api.isolarcloud.com.hk/sungws/AppService");
            var request = new RestRequest(Method.POST);
            try
            {
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("Connection", "keep-alive");
                request.AddHeader("Content-Length", "655");
                request.AddHeader("Accept-Encoding", "gzip, deflate");
                request.AddHeader("Host", "api.isolarcloud.com.hk");
                request.AddHeader("Postman-Token", "af9066cc-fda6-4db3-91bb-11ceab8a9cf9,f3eb356a-7e63-4194-b70b-c3f8ae87898a");
                request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("Accept", "*/*");
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("User-Agent", "sungrow-agent");
                request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
                request.AddParameter("multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW", "------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"service\"\r\n\r\nlogin\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"user_account\"\r\n\r\nSolisAdmin\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"user_password\"\r\n\r\n12345678\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"appkey\"\r\n\r\n3C6596F62C7BC3A69847B071ED3C20EC\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"login_type\"\r\n\r\n1\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW--", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    var Response = JsonConvert.DeserializeObject<LoginRestAPiobject>(response.Content);
                    obj.Token = Response.result_data.token;
                    obj.UserId = Response.result_data.user_id;
                    return obj;
                }
                return null;
            }
            catch (Exception ex) {
                ex.Data["MethodAndClass"] = "LoginSunGrow() in SunGrowProvider";
                throw ex;
            }
           
        }

        public void GetPlantGraph(int UserId, string Token)
        {
            int[] graphTypes = { 1, 2, 3, 4 };
            int year = DateTime.Now.Year; int month = DateTime.Now.Month; int day = DateTime.Now.Day;

            var client = new RestClient("https://api.isolarcloud.com.hk/sungws/AppService");
            try
            {
                foreach (var plantid in PlantIds)
                {
                    foreach (var graphType in graphTypes.ToList())
                    {
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("Connection", "keep-alive");
                        request.AddHeader("Content-Length", "668");
                        request.AddHeader("Accept-Encoding", "gzip, deflate");
                        request.AddHeader("Host", "api.isolarcloud.com.hk");
                        request.AddHeader("Postman-Token", "66952a74-0430-4a01-b16c-18adbb521f6d,8082fe3a-801d-4c01-b2c6-e5a9848f04ee");
                        request.AddHeader("Cache-Control", "no-cache");
                        request.AddHeader("Accept", "*/*");
                        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                        request.AddHeader("User-Agent", "sungrow-agent");
                        request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
                        //request.AddParameter("multipart/form-data", "------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"service\"\r\n\r\n" + SunGrowMethods.getHistoryInfo + "\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"user_id\"\r\n\r\n" + UserId + "\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"ps_id\"\r\n\r\n" + PlantId + "\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"year\"\r\n\r\n" + year + "\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"day\"\r\n\r\n" + day + "\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"month\"\r\n\r\n" + month + "\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"date_type\"\r\n\r\n" + graphType + "\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"appkey\"\r\n\r\n3C6596F62C7BC3A69847B071ED3C20EC\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"token\"\r\n\r\n" + Token + "\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"lang\"\r\n\r\n_en_US\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW--", ParameterType.RequestBody);
                        request.AddParameter("multipart/form-data", "------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"service\"\r\n\r\n" + SunGrowMethods.getHistoryInfo + "\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"user_id\"\r\n\r\n" + UserId + "\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"ps_id\"\r\n\r\n" + plantid + "\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"year\"\r\n\r\n" + year + "\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"day\"\r\n\r\n" + day + "\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"month\"\r\n\r\n" + month + "\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"date_type\"\r\n\r\n" + graphType + "\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"appkey\"\r\n\r\n3C6596F62C7BC3A69847B071ED3C20EC\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"token\"\r\n\r\n" + Token + "\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"lang\"\r\n\r\n_en_US\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW--", ParameterType.RequestBody);
                        IRestResponse response = client.Execute(request);
                        if (response.IsSuccessful)
                        {
                            apiresponses.Add(new APISuccessResponses
                            {
                                plantId = plantid.ToString(),
                                Provider = "SunGrow",
                                APIMethod = String.Concat(SunGrowMethods.getHistoryInfo.ToString(), "_", graphType.Equals(1) ? "Year" : graphType.Equals(2) ? "Month" : graphType.Equals(3) ? "Day" : "OverAll"),
                                response = response.Content,
                                Hour = DateTime.UtcNow.Hour + 5
                            });
                        }
                    }
                }
            }
            catch (Exception ex) {
                ex.Data["MethodAndClass"] = "GetPlantGraph() in SunGrowProvider";
                throw ex;
            }
           
        }

        private void ReconcileAPIResponses()
        {
            _graph.InsertGraphStats(apiresponses.ToList());
            Console.WriteLine("Graph Stats Inserted in Response Table");
            apiresponses.Clear();
        }

    }
}
