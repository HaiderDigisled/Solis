using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class RecurringJobDTO
    {
        public string BaseUrl { get; set; }
        public string Method { get; set; }
        public string CronExpression { get; set; }
        public string Model { get; set; }
        public string RequestType { get; set; }
        public string Name { get; set; }
    }
}
