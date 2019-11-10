using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class EnergyGraph
    {
        public int plantid { get; set; }

        public string Provider { get; set; }

        public decimal Energy { get; set; }

        public string timeunit { get; set; }

        public DateTime fetchDate { get; set; }

        public string Day { get; set; }

        public string Month { get; set; }

        public decimal EnergySaving { get; set; }

        public string Year { get; set; }
    }
}
