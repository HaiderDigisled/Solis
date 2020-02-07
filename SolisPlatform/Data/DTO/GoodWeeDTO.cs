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
}
