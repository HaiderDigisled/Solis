using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class RankingCalculationViewDTO
    {
        public int id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public string PlantId { get; set; }
        public int VendorType { get; set; }
        public decimal GridStationRate { get; set; }
        public decimal SunHours { get; set; }
        public string RateMonthDateTime { get; set; }
        public decimal Energy { get; set; }
        public decimal PlantCapacity { get; set; }
        public string PlantType { get; set; }
    }
}
