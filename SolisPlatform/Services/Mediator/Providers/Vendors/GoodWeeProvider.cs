using Data.Contracts;
using Data.Contracts.GoodWee;
using Data.DTO;
using Data.Mappers;
using Data.Model;
using Foundation;
using Miscellaneous.Foundation;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mediator.Providers.Vendors
{
    public class GoodWeeProvider : VendorBase
    {
        private readonly IGraphRepository _graph;
        private readonly IMiscRepository _misc;
        private readonly IGoodWeeRepository _goodWee;
        private IEnumerable<PlantInformation> PlantIds;
        private List<APISuccessResponses> apiresponses;
        private EnergyGraphMapper mapper;
        private Helper helper;

        public GoodWeeProvider(IGraphRepository graph, IGoodWeeRepository goodWee, IMiscRepository misc)
        {
            _graph = graph;
            _goodWee = goodWee;
            _misc = misc;
            apiresponses = new List<APISuccessResponses>();
            mapper = new EnergyGraphMapper();
        }

        public override void CalculateRanking()
        {

        }

        public override void CheckDeviceFaults()
        {

        }

        public override void GetPlants()
        {
            PlantIds = _goodWee.GetGoodWeePlants();
        }

        public override void SaveAPIResponses()
        {
            GetPlantGraph();
            ReconcileAPIResponses();
        }

        public override void SaveEnergyGraph(string Vendor)
        {
            Console.WriteLine($"Mapping Responses for {Vendor} to EnergyGraph");
            var responses = _graph.GetGraphResponses(Vendor);
            var graphValues = mapper.Map(responses.ToList(), Vendor);
            _graph.InsertGraph(graphValues);
            Console.WriteLine($"Responses Mapped");
        }

        public override void UpdatePlantsStatus()
        {

        }

        private void ReconcileAPIResponses()
        {
            _graph.InsertGraphStats(apiresponses.ToList());
            Console.WriteLine("Graph Stats Inserted in Response Table");
            apiresponses.Clear();
        }
        private void GetPlantGraph()
        {
            try
            {
                foreach (var plant in PlantIds)
                {
                    if (!plant.IsHistoric)
                    {
                        helper = new Helper();
                        var weeklyRange = helper.GetWeeklyRange(plant.CreatedTime.Split('T')[0], DateTime.Now.ToString("yyyy-MM-dd"));
                        foreach (var week in weeklyRange)
                        {
                            GetPlantDetails(plant.PlantId, week.EndDate, "0"); // Get Daily Stats
                        }

                        GetPlantDetails(plant.PlantId, DateTime.Now.ToString("yyyy-MM-dd"), "1"); // Get Monthly Stats
                        _goodWee.MarkPlantHistoric(plant.PlantId);
                    }
                    else {
                        string[] types = { "0", "1" };
                        foreach (var type in types)
                        {
                            GetPlantDetails(plant.PlantId, DateTime.Now.ToString("yyyy-MM-dd"), type);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data["MethodAndClass"] = "GetPlantGraph() in GoodWeeProvider";
                throw ex;
            }
        }

        private void GetPlantDetails(string PlantId, string Date, string type)
        {

            #region LoginGoodWee
            var loginclient = new RestClient("http://openapi.semsportal.com/api/OpenApi/GetToken");
            var loginrequest = new RestRequest(Method.POST);
            loginrequest.AddParameter("account", "Abdul.mateen@solis-energy.com");
            loginrequest.AddParameter("pwd", "77b3fd7c3b0b3b22a992c9cb9b45e967");
            IRestResponse loginresponse = loginclient.Execute(loginrequest);

            if (loginresponse.IsSuccessful)
            {
                var GoodWee = JsonConvert.DeserializeObject<GoodWeeLoginDTO>(loginresponse.Content);
                var client = new RestClient("http://openapi.semsportal.com/api/OpenApi/GetPlantGeneration");
                var request = new RestRequest(Method.POST);
                request.AddHeader("token", GoodWee.data.token);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", "{\n    \"plant_id\": \""+PlantId+"\",\n    \"date\": \""+Date+"\",\n    \"type\":"+type+"\n    \n    \n}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    apiresponses.Add(new APISuccessResponses
                    {
                        plantId = PlantId,
                        Provider = "GoodWee",
                        APIMethod = String.Concat("getHistoryInfo", "_", type == "0" ? "Day ": "Month"),
                        response = response.Content,
                        Hour = DateTime.Now.Hour,
                        Mapped = 0
                    });
                }
            }
            #endregion

        }
    }
}
