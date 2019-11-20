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
                    if (Response.result_data.time_flag != null)
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
            catch (Exception ex) {
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
            catch (Exception ex) {
                ex.Data["MethodAndClass"] = "GrowWatt() in EnergyGraphMapper";
                throw ex;
            }
            
            return graph;
        }
    }
}
