using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class RestAPIDTOs
    {
    }
    public class RootDeviceobject
    {
        public DataDevice data { get; set; }
        public int error_code { get; set; }
        public string error_msg { get; set; }
    }

    public class DataDevice
    {
        public int count { get; set; }
        public Device[] devices { get; set; }
    }
    public class Device
    {
        public string device_sn { get; set; }
        public string last_update_time { get; set; }
        public string model { get; set; }
        public bool lost { get; set; }
        public int status { get; set; }
        public string manufacturer { get; set; }
        public int? device_id { get; set; }
        public string datalogger_sn { get; set; }
        public int type { get; set; }
    }

    public class RootDeviceFaultsobject
    {
        public RootDeviceFaultsData data { get; set; }
        public int error_code { get; set; }
        public string error_msg { get; set; }
    }

    public class RootDeviceFaultsData
    {
        public string sn { get; set; }
        public int count { get; set; }
        public Alarm[] alarms { get; set; }
    }

    public class Alarm
    {
        public int alarm_code { get; set; }
        public int status { get; set; }
        public string end_time { get; set; }
        public string start_time { get; set; }
        public string alarm_message { get; set; }
    }


}
