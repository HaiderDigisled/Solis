using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class APISuccessResponses
    {
        public int id { get; set; }
        public string plantId { get; set; }
        public string Provider { get; set; }
        public string APIMethod { get; set; }
        public string response { get; set; }
        public string CreatedOn { get; set; }
        public int Hour { get; set; }
        public int Mapped { get; set; }

    }
}
