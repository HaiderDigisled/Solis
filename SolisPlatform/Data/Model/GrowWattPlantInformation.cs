using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class GrowWattPlantInformation
    {
        public int Status { get; set; }
        public string Locale { get; set; }
        public float TotalEnergy { get; set; }
        public string OperatorCompany { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string CurrentPower { get; set; }
        public string CreateDate { get; set; }
        public string ImageUrl { get; set; }
        public int PlantId { get; set; }
        public string Name { get; set; }
        public string Installer { get; set; }
        public int GrowWattUserId { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public double PeakPower { get; set; }
    }
}
