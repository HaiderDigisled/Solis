using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Ranking
    {
        public int id { get; set; }
        public int PlantId { get; set; }
        public decimal RankingPercentage { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public int Rank { get; set; }
    }
}
