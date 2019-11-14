using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class SunGrowPlantInformation
    {
        public int PowerStationId { get; set; }
        public int? PowerStationCountryId { get; set; }
        public int is_bank_ps { get; set; }
        public string ZipCode { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingZipCode { get; set; }
        public string ShareType { get; set; }
        public int PowerStationType { get; set; }
        public string PowerStationTimezone { get; set; }
        public double? Longitude { get; set; }
        public double? DesignCapacity { get; set; }
        public double? ParamIncome { get; set; }
        public double? ParamCo2 { get; set; }
        public double? Param_Tree { get; set; }
        public double? ParamSo2 { get; set; }
        public double? ParamNox { get; set; }
        public double? ParamMeter { get; set; }
        public double? ParamCoal { get; set; }
        public double? ParamPowder { get; set; }
        public double? ParamWater { get; set; }
        public double? JoinYearInitElec { get; set; }
        public double? TotalInitElec { get; set; }
        public double? TotalInitProfit { get; set; }
        public double? TotalInitCo2Accelerate { get; set; }
        public double? Latitude { get; set; }
        public double? GprsLongitude { get; set; }
        public double? GprsLatitude { get; set; }
        public double? MapLongitude { get; set; }
        public double? MapLatitude { get; set; }
        public double? WgsLongitude { get; set; }
        public double? WgsLatitude { get; set; }
        public double? GcjLongitude { get; set; }
        public double? GcjLatitude { get; set; }
        public string InstallDate { get; set; }
        public string Location { get; set; }
        public string AreaId { get; set; }
        public int ValidFlag { get; set; }
        public string PowerStationName { get; set; }
        public string PowerStationShortName { get; set; }
        public string PowerStationHolder { get; set; }
        public string Producer { get; set; }
        public DateTime BuildDate { get; set; }
        public int? AreaType { get; set; }
        public bool IsTuv { get; set; }
        public DateTime RecoreCreateTime { get; set; }
        public string OperateYear { get; set; }
        public string OperationBusName { get; set; }
        public DateTime ExpectInstallDate { get; set; }
        public DateTime SafeStartDate { get; set; }
        public int ArrearsStatus { get; set; }
        public int PowerStationStatus { get; set; }
        public int PowerStationFaultStatus { get; set; }
        public int FaultCount { get; set; }
        public int AlarmCount { get; set; }
        public int PowerStationIsNotInit { get; set; }
        public int PowerStationHealthStatus { get; set; }
        public string PvPower { get; set; }
        public string PvEnergy { get; set; }
        public string EsPower { get; set; }
        public string EsEnergy { get; set; }
        public string EsDisenergy { get; set; }
        public string EsTotalEnergy { get; set; }
        public string EsTotalDisenergy { get; set; }
        public string DesignCapacityUnit { get; set; }
        public int DesignCapacityVirgin { get; set; }
        public double? InstalledPowerVirgin { get; set; }
        public int FaultDevCount { get; set; }
        public int AlarmDevCount { get; set; }
        public int OfflineDevCount { get; set; }
        public int FaultAlarmOfflineDevCount { get; set; }
    }
}
