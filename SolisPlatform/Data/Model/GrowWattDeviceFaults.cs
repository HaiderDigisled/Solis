using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class GrowWattDeviceFaults 
    {
        public int AlarmCode { get; set; }
        public int DeviceTypeStatus { get; set; }
        public string Endtime { get; set; }
        public string Starttime { get; set; }
        public string AlarmMessage { get; set; }
        public string DeviceSn { get; set; }
    }
}
