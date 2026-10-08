using System;

namespace DeviceManagement 
{
    public class Printer : Device
    {
        private string printType;
        private int pageCount;
        private bool hasNetworkSupport;

        public Printer(
            string deviceID,
            string deviceName,
            int usageYear,
            decimal purchasePrice,
            string printType,
            int pageCount,
            bool hasNetworkSupport,
            DeviceStatus status = DeviceStatus.Active)
            : base(deviceID, deviceName, usageYear, purchasePrice, status)
        {
            if (printType != "laser" && printType != "ink")
            {
                throw new ArgumentException("Loại máy in là laser hoặc phun");
            }
            if (pageCount < 0)
            {
                throw new ArgumentException("Số trang không âm");
            }

            this.printType = printType;
            this.pageCount = pageCount;
            this.hasNetworkSupport = hasNetworkSupport;
        }

    public CalculateAnnualMaintenanceCost()
    {
        decimal Cost = 0;
        Cost += purchasePrice * 0.04m;
        if (pageCount > 100000)
            Cost += 500000m;
        if (printType == "ink")
            Cost += 300000m;
        return Cost;
    }

    public override string ToString()
    {
        return base.ToString() + " - Loại: {printType} - Số trang: {pageCount} - Có mạng: {hasNetworkSupport}";
    }

    }


}