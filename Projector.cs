using System;

namespace DeviceManagement
{
    public class Projector : Device
    {
        protected int lumens;
        protected int lampHoursUsed;

        public Projector(
            string deviceID,
            string deviceName,
            int usageYear,
            decimal purchasePrice,
            int lumens,
            int lampHoursUsed,
            DeviceStatus status = DeviceStatus.Active)
            : base(deviceID, deviceName, usageYear, purchasePrice, status)
        {
            if (lumens <= 0)
            {
                throw new ArgumentException("Lumens lớn hơn 0");
            }

            if (lampHoursUsed < 0)
            {
                throw new ArgumentException("Số giờ sử dụng không âm");
            }

            this.lumens = lumens;
            this.lampHoursUsed = lampHoursUsed;
        }

        public override decimal CalculateAnnualMaintenanceCost()
        {
            decimal Cost = 0;
            Cost += purchasePrice * 0.03m;
            if (lampHoursUsed > 3000)
                Cost += 1500000m;
            return Cost;
        }

        public override string ToString()
        {
            return base.ToString() + " - Lumens: {lumens} - Số giờ sử dụng: {lampHoursUsed}";
        }
    }
}
