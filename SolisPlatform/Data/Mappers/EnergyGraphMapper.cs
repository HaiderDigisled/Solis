using Data.DTO;
using Data.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.DTO.GraphDTO;

namespace Data.Mappers
{
    public class EnergyGraphMapper
    {
        public IEnumerable<EnergyGraph> Map(List<APISuccessResponses> responses, string Provider)
        {
            List<EnergyGraph> graph = null;
            switch (Provider)
            {
                case "SunGrow":
                    graph = SunGrow(responses, Provider);
                    break;
                case "GrowWatt":
                    graph = GrowWatt(responses, Provider);
                    break;
                case "GoodWee":
                    graph = GoodWee(responses, Provider);
                    break;
            }
            return graph;
        }

        public List<EnergyGraph> SunGrow(List<APISuccessResponses> responses, string Provider)
        {
            List<EnergyGraph> graph = new List<EnergyGraph>();
            try
            {
                foreach (var resp in responses)
                {
                    Console.WriteLine($"Processing Plant : {resp.plantId}");
                    var Response = JsonConvert.DeserializeObject<SunGrowGraphDTO>(resp.response);
                    if (Response.result_data != null && Response.result_data.time_flag != null)
                    {
                        if (Response.result_data.time_flag.Equals("2"))
                        {
                            foreach (var x in Response.result_data.actual_energy.Select((energy, day) => new { day, energy }))
                            {
                                int _day = 1 + x.day;
                                graph.Add(new EnergyGraph()
                                {
                                    Day = String.Concat(DateTime.Now.Year, "-", DateTime.Now.Month, "-", _day >= 10 ? _day.ToString() : string.Concat("0", _day.ToString())),
                                    Energy = Convert.ToDecimal(x.energy.Equals("--") ? "0.00" : x.energy),
                                    Month = "NA",
                                    plantid = Convert.ToInt32(resp.plantId),
                                    Provider = "SunGrow",
                                    timeunit = "day",
                                    fetchDate = DateTime.Now,
                                    Year = DateTime.Now.Year.ToString()
                                });
                            }
                        }
                        if (Response.result_data.time_flag.Equals("1"))
                        {
                            foreach (var x in Response.result_data.actual_energy.Select((energy, month) => new { month, energy }))
                            {
                                int _month = 1 + x.month;
                                graph.Add(new EnergyGraph()
                                {
                                    Month = String.Concat(DateTime.Now.Year.ToString(), "-", _month >= 10 ? _month.ToString() : String.Concat("0", _month.ToString())),
                                    Energy = Convert.ToDecimal(x.energy.Equals("--") ? "0.00" : x.energy),
                                    Day = "NA",
                                    plantid = Convert.ToInt32(resp.plantId),
                                    Provider = "SunGrow",
                                    timeunit = "month",
                                    fetchDate = DateTime.Now,
                                    Year = DateTime.Now.Year.ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data["MethodAndClass"] = "SunGrow() in EnergyGraphMapper";
                throw ex;
            }

            return graph;
        }

        public List<EnergyGraph> GrowWatt(List<APISuccessResponses> responses, string Provider)
        {
            List<EnergyGraph> graph = new List<EnergyGraph>();
            try
            {
                foreach (var resp in responses)
                {
                    Console.WriteLine($"Processing Plant : {resp.plantId}");
                    var Response = JsonConvert.DeserializeObject<GrowWattGraphDTO>(resp.response);
                    if (Response.error_code.Equals(0) && Response.data.time_unit.Equals("day"))
                    {
                        Response.data.energys.ForEach((x) =>
                        {
                            graph.Add(new EnergyGraph()
                            {
                                Day = x.date,
                                Energy = Convert.ToDecimal(x.energy),
                                Month = "NA",
                                plantid = Convert.ToInt32(resp.plantId),
                                Provider = "GrowWatt",
                                timeunit = Response.data.time_unit,
                                fetchDate = DateTime.Now,
                                Year = x.date.Split('-')[0]
                            });
                        });
                    }
                    if (Response.error_code.Equals(0) && Response.data.time_unit.Equals("month"))
                    {
                        Response.data.energys.ForEach((x) =>
                        {
                            graph.Add(new EnergyGraph()
                            {
                                Month = x.date,
                                Energy = Convert.ToDecimal(x.energy),
                                Day = "NA",
                                plantid = Convert.ToInt32(resp.plantId),
                                Provider = "GrowWatt",
                                timeunit = Response.data.time_unit,
                                fetchDate = DateTime.Now,
                                Year = x.date.Split('-')[0]
                            });
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data["MethodAndClass"] = "GrowWatt() in EnergyGraphMapper";
                throw ex;
            }

            return graph;
        }

        public List<EnergyGraph> GoodWee(List<APISuccessResponses> responses, string Provider)
        {

            List<EnergyGraph> graph = new List<EnergyGraph>();
            IDictionary<string, IDictionary<string, decimal>> plants = new Dictionary<string, IDictionary<string, decimal>>();
            foreach (var resp in responses)
            {
                var response = JsonConvert.DeserializeObject<GoodWeePlantGeneration>(resp.response);
                if (response.code == 0)
                {
                    if (resp.APIMethod.Contains("Day"))
                    {
                        foreach (var days in response.data.pv_generation)
                        {
                            if (plants.TryGetValue(resp.plantId, out IDictionary<string, decimal> powerGeneration))
                            { // plant already present
                                // check the generation day and add the day if not present
                                if (!powerGeneration.TryGetValue(days.x, out decimal power))
                                {
                                    powerGeneration.Add(days.x, days.y);
                                }
                            }
                            else
                            {
                                IDictionary<string, decimal> power = new Dictionary<string, decimal>();
                                power.Add(days.x, days.y);
                                plants.Add(resp.plantId, power);
                            }
                        }

                        foreach (var plant in plants)
                        {
                            foreach (var power in plant.Value)
                            {
                                graph.Add(new EnergyGraph()
                                {
                                    Month = "NA",
                                    Energy = Convert.ToDecimal(power.Value),
                                    Day = power.Key,
                                    plantid = Convert.ToInt32(plant.Key),
                                    Provider = "GoodWee",
                                    timeunit = "day",
                                    fetchDate = DateTime.Now,
                                    Year = power.Key.Split('-')[0]
                                });
                            }
                        }

                    }
                    if (resp.APIMethod.Contains("Month"))
                    {
                        foreach (var months in response.data.pv_generation)
                        {
                            if (plants.TryGetValue(resp.plantId, out IDictionary<string, decimal> powerGeneration))
                            { // plant already present
                                // check the generation day and add the day if not present
                                if (!powerGeneration.TryGetValue(months.x, out decimal power))
                                {
                                    powerGeneration.Add(months.x, months.y);
                                }
                            }
                            else
                            {
                                IDictionary<string, decimal> power = new Dictionary<string, decimal>();
                                power.Add(months.x, months.y);
                                plants.Add(resp.plantId, power);
                            }
                        }

                        foreach (var plant in plants)
                        {
                            foreach (var power in plant.Value)
                            {
                                graph.Add(new EnergyGraph()
                                {
                                    Month = power.Key,
                                    Energy = Convert.ToDecimal(power.Value),
                                    Day = "NA",
                                    plantid = Convert.ToInt32(plant.Key),
                                    Provider = "GoodWee",
                                    timeunit = "month",
                                    fetchDate = DateTime.Now,
                                    Year = power.Key.Split('-')[0]
                                });
                            }
                        }
                    }
                }

            }

            return graph;
        }
    }
}
