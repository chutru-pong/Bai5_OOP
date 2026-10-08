using System;
 
namespace DeviceManagement
{
    public class Computer : Device
    {

        private int ramGB;
        private string cpuType;
        private bool dedicatedGPU;

        public Computer(string deviceID, string deviceName, int usageYear, decimal purchasePrice, int ramGB, string cpuType, bool dedicatedGPU, DeviceStatus status = DeviceStatus.Active)
            : base(deviceID, deviceName, usageYear, purchasePrice, status)
        {
            this.ramGB = ramGB;
            this.cpuType = cpuType;
            this.dedicatedGPU = dedicatedGPU;
        }

        public override decimal CalculateAnnualMaintenanceCost()
        {
            decimal cost = 0;
            cost += purchasePrice * 0.05m;
            if (dedicatedGPU)
                cost += purchasePrice * 0.02m;
            if (usageYear > 5)
                cost += purchasePrice * 0.01m;
            return cost;
        }

        public override string ToString()
        {
            return base.ToString() + " - RAM: " + ramGB + "GB - CPU: " + cpuType + " - Đồ họa rời: " + dedicatedGPU;
        }
    }
}