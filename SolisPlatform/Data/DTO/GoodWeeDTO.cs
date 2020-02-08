using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class GoodWeeDTO
    {
        public int code { get; set; }
        public string msg { get; set; }
        public dynamic data { get; set; }
    }

    public class GoodWeeLoginDTO {

        public int code { get; set; }
        public string msg { get; set; }
        public Creds data { get; set; }

       
    }

    public class Creds {
        public string token { get; set; }
        public int expired { get; set; }
    }

    public class GoodWeePlantGeneration {
        public int code { get; set; }
        public string msg { get; set; }
        public PlantGenerationData data { get; set; }
    }

    public class PlantGenerationData {
        public string date { get; set; }
        public string currency { get; set; }
        public List<PVGeneration> pv_generation { get; set; }
        public List<PVGeneration> self_use { get; set; }
        public List<PVGeneration> buy { get; set; }
        public List<PVGeneration> sell { get; set; }
        public List<PVGeneration> consumption { get; set; }
        public List<PVGeneration> income { get; set; }
        public List<PVGeneration> self_useratio { get; set; }
        public List<PVGeneration> output { get; set; }

    }

    public class PVGeneration {
        public string x { get; set; }
        public int y { get; set; }
    }

}
