using Data.Contracts;
using Data.Contracts.GrowWatt;
using Data.Mappers;
using Data.Model;
using Foundation;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mediator.Providers.Vendors
{
    public class GrowWattProvider : VendorBase
    {
        private readonly IGraphRepository _graph;
        private readonly IGrowWattRepository _growWatt;
        private Helper helper;
        private IEnumerable<int> PlantIds;
        private List<APISuccessResponses> apiresponses;
        private EnergyGraphMapper mapper;


        public GrowWattProvider(IGraphRepository graph,IGrowWattRepository growWatt) {
            _graph = graph;
            _growWatt = growWatt;
            helper = new Helper();
            apiresponses = new List<APISuccessResponses>();
            mapper = new EnergyGraphMapper();
        }

        public override void GetPlants()
        {
            PlantIds = _growWatt.GetGrowWattPlants();
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
            string StartDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            string EndDate = DateTime.Now.ToString("yyyy-MM-dd");

            foreach (var plant in PlantIds) {
                Console.WriteLine($"Recovering Plant {plant}");
                GetPlantGraph(plant, StartDate, EndDate);
                Console.WriteLine($"Recovered Plant {plant}");
            }
            Console.WriteLine("Inserting API Responses");
            ReconcileAPIResponses();
            Console.WriteLine("API Responses Inserted");
        }

        private void GetPlantGraph(int plantid, string StartDate, string EndDate)
        {
            string[] graphTypes = { "month", "day", "year" };
            var WeekRange = helper.GetWeeklyRange(StartDate, EndDate);
            string parameters = String.Empty;
            try
            {
                foreach (var week in WeekRange)
                {
                    parameters = $"plant_id={plantid}&start_date={week.StartDate}&time_unit=day&end_date={week.EndDate}";
                    var client = new RestClient(string.Concat("http://server.growatt.com/v1/plant/energy?", parameters));
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("Connection", "keep-alive");
                    request.AddHeader("Cookie", "JSESSIONID=F8B2F87B3F50416ADD03CD89DCD08508; SERVERID=0e6c7216448f123fa30077444fb1651c|1572190748|1572190724");
                    request.AddHeader("Accept-Encoding", "gzip, deflate");
                    request.AddHeader("Host", "server.growatt.com");
                    request.AddHeader("Postman-Token", "e4dcb2b8-718d-4eda-9ac6-a5d8a21e65c8,b5ee0bd7-20e7-44fe-b80a-418444bf9f8f");
                    request.AddHeader("Cache-Control", "no-cache");
                    request.AddHeader("Accept", "*/*");
                    request.AddHeader("User-Agent", "PostmanRuntime/7.17.1");
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    request.AddHeader("Token", "pep3pf2x82il6og55e151az7v4k2kfik");
                    IRestResponse response = client.Execute(request);
                    if (response.IsSuccessful)
                    {
                        apiresponses.Add(new APISuccessResponses
                        {
                            plantId = plantid.ToString(),
                            Provider = "GrowWatt",
                            APIMethod = String.Concat("getHistoryInfo", "_", "Day"),
                            response = response.Content,
                            Hour = DateTime.Now.Hour,
                            Mapped = 0
                        });
                    }
                }

                parameters = $"plant_id={plantid}&start_date={StartDate}&time_unit=month&end_date={EndDate}";
                var m_client = new RestClient(string.Concat("http://server.growatt.com/v1/plant/energy?", parameters));
                var m_request = new RestRequest(Method.GET);
                m_request.AddHeader("cache-control", "no-cache");
                m_request.AddHeader("Connection", "keep-alive");
                m_request.AddHeader("Cookie", "JSESSIONID=F8B2F87B3F50416ADD03CD89DCD08508; SERVERID=0e6c7216448f123fa30077444fb1651c|1572190748|1572190724");
                m_request.AddHeader("Accept-Encoding", "gzip, deflate");
                m_request.AddHeader("Host", "server.growatt.com");
                m_request.AddHeader("Postman-Token", "e4dcb2b8-718d-4eda-9ac6-a5d8a21e65c8,b5ee0bd7-20e7-44fe-b80a-418444bf9f8f");
                m_request.AddHeader("Cache-Control", "no-cache");
                m_request.AddHeader("Accept", "*/*");
                m_request.AddHeader("User-Agent", "PostmanRuntime/7.17.1");
                m_request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                m_request.AddHeader("Token", "pep3pf2x82il6og55e151az7v4k2kfik");
                IRestResponse m_response = m_client.Execute(m_request);
                if (m_response.IsSuccessful)
                {
                    apiresponses.Add(new APISuccessResponses
                    {
                        plantId = plantid.ToString(),
                        Provider = "GrowWatt",
                        APIMethod = String.Concat("getHistoryInfo", "_", "Month"),
                        response = m_response.Content,
                        Hour = DateTime.Now.Hour,
                        Mapped = 0
                    });
                }
            }
            catch (Exception ex) {
                ex.Data["MethodAndClass"] = "GetPlantGraph() in GrowWattProvider";
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
