using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public static class StoredProcedures
    {
        public const string GetVendors = "GetVendors";
        public const string GetPlants = "V1_GetPlantIds";
        public const string PopulateAPISuccessResponses = "InsertAPISuccessResponse";
        public const string CalculateRanking = "CalculateRanking";
        public const string AddOrUpdateGrowWattDeviceInformation = "AddOrUpdateGrowWattDeviceInformation";
        public const string AddGrowWattDeviceFaults = "AddGrowWattDeviceFaults";
    }
}
