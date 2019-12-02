using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts.GrowWatt
{
    public interface IGrowWattRepository
    {
        IEnumerable<int> GetGrowWattPlants();
        void UpdateGrowWattDevicesInformation(IEnumerable<GrowWattDevice> devices);
        void AddDevicesFaultInformation(IEnumerable<GrowWattDeviceFaults> faults);
    }
}
