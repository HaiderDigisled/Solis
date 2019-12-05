using Data.Contracts;
using Data.Contracts.GrowWatt;
using Data.DTO;
using Data.Mappers;
using Data.Model;
using Foundation;
using Newtonsoft.Json;
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
        private readonly IMiscRepository _misc;
        private Helper helper;
        private IEnumerable<int> PlantIds;
        private List<APISuccessResponses> apiresponses;
        private EnergyGraphMapper mapper;
        private List<GrowWattDevice> deviceInformation;
        private List<GrowWattDeviceFaults> faultsInformation;


        public GrowWattProvider(IGraphRepository graph, IGrowWattRepository growWatt, IMiscRepository misc)
        {
            _graph = graph;
            _growWatt = growWatt;
            _misc = misc;
            helper = new Helper();
            apiresponses = new List<APISuccessResponses>();
            deviceInformation = new List<GrowWattDevice>();
            faultsInformation = new List<GrowWattDeviceFaults>();
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

            foreach (var plant in PlantIds)
            {
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
            catch (Exception ex)
            {
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

        public override void CalculateRanking()
        {
            List<Ranking> ranking = new List<Ranking>();
            var PlantDetails = _misc.CalculateRanking(PlantIds, "GrowWatt");
            var PlantCapacity = _misc.GetPlantsCapacity("GrowWattPlantInformation", "PeakPower", "PlantId", PlantIds);
            var RankingDetailView = from pd in PlantDetails
                                    join pc in PlantCapacity
                                    on pd.PlantId equals pc.PlantId
                                    select new RankingCalculationViewDTO
                                    {
                                        PlantId = pd.PlantId,
                                        Energy = pd.Energy,
                                        GridStationRate = pd.GridStationRate,
                                        GroupId = pd.GroupId,
                                        RateMonthDateTime = pd.RateMonthDateTime,
                                        SunHours = pd.SunHours,
                                        UserId = pd.UserId,
                                        VendorType = pd.VendorType,
                                        PlantCapacity = pc.PlantCapacity
                                    };
            if (RankingDetailView.Count() > 0)
            {
                foreach (var item in RankingDetailView)
                {
                    decimal TargetEnergy = item.SunHours * item.PlantCapacity;
                    decimal TargetAchieved;
                    if (TargetEnergy > 0)
                    {
                        TargetAchieved = item.Energy / TargetEnergy;
                    }
                    else
                    {
                        TargetAchieved = 0;
                    }
                    ranking.Add(new Ranking { PlantId = item.PlantId, RankingPercentage = TargetAchieved });
                }

                var finallist = ranking.OrderByDescending(x => x.RankingPercentage).ToList();
                int position = 1;
                var date = DateTime.Now;

                foreach (var item in finallist)
                {
                    item.Rank = position;
                    item.RankingPercentage = Convert.ToDecimal((1 - position / Convert.ToDouble(PlantIds.Count())) * 100);
                    item.CreatedOn = date;
                    item.UpdatedOn = date;
                    position++;
                }
                _misc.FinalRanking(finallist);
            }
        }

        public override void UpdatePlantsStatus()
        {
            foreach (var plant in PlantIds)
            {
                var deviceInfo = new GrowWattDevice();
                var client = new RestClient("http://server.growatt.com/v1/device/list?plant_id=" + plant + "");
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("Connection", "keep-alive");
                request.AddHeader("Cookie", "SERVERID=e4aa056c74c03eeaf1d467cfed0f8907|1565703517|1565703200; JSESSIONID=4F6CCA9C5C0603B660183D7FCED1A856");
                request.AddHeader("Accept-Encoding", "gzip, deflate");
                request.AddHeader("Host", "server.growatt.com");
                request.AddHeader("Postman-Token", "d7102c46-d253-4699-afaa-8e50c9714653,f5f8a3f0-d433-49f9-a940-cb075495c345");
                request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("Accept", "*/*");
                request.AddHeader("User-Agent", "PostmanRuntime/7.19.0");
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("Token", "pep3pf2x82il6og55e151az7v4k2kfik");
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    var plantInformation = JsonConvert.DeserializeObject<RootDeviceobject>(response.Content);
                    if (plantInformation.error_code == 0) {
                        if (plantInformation.data.count != 0)
                        {
                            foreach (var device in plantInformation.data.devices)
                            {
                                deviceInfo.PlantId = plant;
                                deviceInfo.DeviceId = device.device_id.Equals(null) ? 0 : device.device_id;
                                deviceInfo.Type = !device.type.Equals(0) ? device.type : 0;
                                deviceInfo.Model = !string.IsNullOrWhiteSpace(device.model) ? device.model : "";
                                deviceInfo.Manufacturer = !string.IsNullOrWhiteSpace(device.manufacturer) ? device.manufacturer : "";
                                deviceInfo.LastUpdateTime = device.last_update_time;
                                deviceInfo.DataloggerSn = device.datalogger_sn;
                                deviceInfo.DeviceOnlineStatus = device.lost;
                                deviceInfo.DeviceTypeStatus = device.status;
                                deviceInfo.DeviceSn = device.device_sn;

                                deviceInformation.Add(deviceInfo);
                            }
                        }
                    }
                   
                }
            }

            _growWatt.UpdateGrowWattDevicesInformation(deviceInformation);

        }

        public override void CheckDeviceFaults()
        {
            foreach (var item in deviceInformation.Where(x => !x.Type.Equals(3)).Distinct())
            {
                var client = new RestClient("http://server.growatt.com/v1/device/inverter/alarm?device_sn=" + item.DeviceSn + "");
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("Connection", "keep-alive");
                request.AddHeader("Cookie", "SERVERID=e4aa056c74c03eeaf1d467cfed0f8907|1565703517|1565703200; JSESSIONID=4F6CCA9C5C0603B660183D7FCED1A856");
                request.AddHeader("Accept-Encoding", "gzip, deflate");
                request.AddHeader("Host", "server.growatt.com");
                request.AddHeader("Postman-Token", "81f051c3-5872-4e96-a193-c5cb49a4183c,bb74208d-604a-4370-8c1f-e8de45d3220b");
                request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("Accept", "*/*");
                request.AddHeader("User-Agent", "PostmanRuntime/7.20.1");
                request.AddHeader("Token", "pep3pf2x82il6og55e151az7v4k2kfik");
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    var plantFaultsInformation = JsonConvert.DeserializeObject<RootDeviceFaultsobject>(response.Content);
                    if (plantFaultsInformation.error_code == 0)
                    {
                        if (plantFaultsInformation.data.count != 0)
                        {
                            foreach (var device in plantFaultsInformation.data.alarms)
                            {
                                var faultInfo = new GrowWattDeviceFaults();
                                faultInfo.DeviceSn = item.DeviceSn;
                                faultInfo.AlarmCode = !device.alarm_code.Equals(null) ? device.alarm_code : 0;
                                faultInfo.DeviceTypeStatus = !device.status.Equals(0) ? device.status : 0;
                                faultInfo.Starttime = !string.IsNullOrWhiteSpace(device.start_time) ? device.start_time : "";
                                faultInfo.Endtime = !string.IsNullOrWhiteSpace(device.end_time) ? device.end_time : "";
                                faultInfo.AlarmMessage = !string.IsNullOrWhiteSpace(device.alarm_message) ? device.alarm_message : "";

                                if (Convert.ToDateTime(faultInfo.Starttime).ToString("yyyyMMdd").Equals(DateTime.Now.ToString("yyyyMMdd")) ||
                                    Convert.ToDateTime(faultInfo.Endtime).ToString("yyyyMMdd").Equals(DateTime.Now.ToString("yyyyMMdd"))){
                                    faultsInformation.Add(faultInfo);
                                }
                            }
                        }
                    }
                }
            }
            if (faultsInformation.Any() && faultsInformation.Count() > 0)
            {
                _growWatt.AddDevicesFaultInformation(faultsInformation);
            }
            else {
                Console.WriteLine("No Faults Found Today");
            }
            
        }
    }
}
