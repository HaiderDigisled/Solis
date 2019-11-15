using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class GraphDTO
    {
        public class GrowWattGraphDTO
        {
            public Data data;
            public int error_code { get; set; }
            public string error_msg { get; set; }
        }

        public class RealTime
        {
            public int error_code { get; set; }
            public List<Power> power { get; set; }
        }

        public class Data
        {
            public int count { get; set; }
            public string time_unit { get; set; }
            public List<dateEnergy> energys { get; set; }
            public List<Power> powers { get; set; }
        }

        public class Power
        {
            public string time { get; set; }
            public string power { get; set; }
        }

        public class dateEnergy
        {
            public string date { get; set; }
            public string energy { get; set; }
        }

        public class SunGrowGraphDTO
        {
            public string req_serial_num { get; set; }
            public string result_code { get; set; }
            public ResultData result_data;
            public string result_msg { get; set; }
        }

        public class ResultData
        {
            public List<string> actual_energy { get; set; }
            public List<string> plan_energy { get; set; }
            public string actual_energy_unit { get; set; }
            public string min_date_id { get; set; }
            public string plan_energy_unit { get; set; }
            public string time_flag { get; set; }
        }


    }
    public class PlantVendor
    {
        public int Plant { get; set; }
        public string Vendor { get; set; }
    }
}
