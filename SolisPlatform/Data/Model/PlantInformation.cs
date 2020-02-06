using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class PlantInformation
    {
        public string PlantId { get; set; }

        public string Provider { get; set; }

        public string Address { get; set; }

        public decimal Capacity { get; set; } 
        
        public string CreatedTime { get; set; }

        public int PlantType { get; set; }

        public decimal TotalEnergy { get; set; }

        public decimal TodayEnergy { get; set; }

        public decimal HourlyEnergy { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

    }
}
