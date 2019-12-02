using Dapper;
using Data.Contracts.GrowWatt;
using Data.Dapper;
using Data.DTO;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.GrowWatt
{
    public class GrowWattRepository : IGrowWattRepository
    {
        DapperManager dapper;
        public GrowWattRepository() {
            dapper = new DapperManager();
        }


        public IEnumerable<int> GetGrowWattPlants()
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@FromTable", "GrowWattPlantInformation");
                parameters.Add("@ColumnName", "PlantId");
                var plants = dapper.Get<GrowWattPlantInformation>(StoredProcedures.GetPlants, parameters, null, true, null, System.Data.CommandType.StoredProcedure);
                return plants.Select(x => x.PlantId);
            }
            catch (Exception ex) {
                ex.Data["MethodAndClass"] = "GetSunGrowPlants() in GrowWattRepository";
                throw ex;
            }
        }

        public void UpdateGrowWattDevicesInformation(IEnumerable<GrowWattDevice> devices)
        {
            try
            {
                if (devices.Any())
                {
                    Console.WriteLine("Updating GrowWatt Devices Information");
                    dapper.Execute<int>(StoredProcedures.AddOrUpdateGrowWattDeviceInformation, devices, null, true, null, System.Data.CommandType.StoredProcedure);
                    Console.WriteLine("GrowWatt Devices Information Updated");
                }
                else {
                    Console.WriteLine("GrowWatt Devices Information Not Found");
                }
                 
            }
            catch (Exception ex)
            {
                ex.Data["MethodAndClass"] = "UpdateGrowWattDevicesInformation() in GrowWattRepository";
                throw ex;
            }
        }

        public void AddDevicesFaultInformation(IEnumerable<GrowWattDeviceFaults> faults)
        {
            try
            {
                if (faults.Any()) {
                    Console.WriteLine("Adding GrowWatt Devices Fault Information");
                    dapper.Execute<int>(StoredProcedures.AddGrowWattDeviceFaults, faults, null, true, null, System.Data.CommandType.StoredProcedure);
                    Console.WriteLine("GrowWatt Devices Fault Information added");
                }
                else{
                    Console.WriteLine("No Faults Found for GrowWatt Devices");
                }
                
            }
            catch (Exception ex)
            {
                ex.Data["MethodAndClass"] = "AddDevicesFaultInformation() in GrowWattRepository";
                throw ex;
            }
        }
    }
}
