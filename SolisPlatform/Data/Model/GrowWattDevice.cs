using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class GrowWattDevice
    {
        public string DeviceSn { get; set; }
        public string LastUpdateTime { get; set; }
        public string Model { get; set; }
        public bool DeviceOnlineStatus { get; set; }
        public int DeviceTypeStatus { get; set; }
        public string Manufacturer { get; set; }
        public int? DeviceId { get; set; }
        public string DataloggerSn { get; set; }
        public int Type { get; set; }
        public string PlantId { get; set; }
    }
}
