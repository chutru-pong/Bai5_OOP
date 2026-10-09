using System;

namespace DeviceManagement
{
    public class LabRoom
    {
        private string roomId;
        private string roomName;
        private int capacity;
        private Device[] devices;
        private int count;

        public LabRoom(string roomId, string roomName, int capacity)
        {
            if (roomId == "")
            {
                throw new ArgumentException("Mã phòng không rỗng");
            }
            if (roomName == "")
            {
                throw new ArgumentException("Tên phòng không rỗng");
            }
            if (capacity <= 0)
            {
                throw new ArgumentException("Sức chứa phải > 0");
            }

            this.roomId = roomId;
            this.roomName = roomName;
            this.capacity = capacity;
            this.devices = new Device[capacity];
            this.count = 0;
        }

        public Device FindDevice(string deviceId)
        {
            if (deviceId == "")
            {
                return null;
            }

            for (int i = 0; i < count; i++)
            {
                if (devices[i].DeviceID == deviceId)
                {
                    return devices[i];
                }
            }
            return null;
        }

        public void AddDevice(Device device)
        {
            if (device == null)
            {
                throw new ArgumentException("Thiết bị không được null");
            }

            if (FindDevice(device.DeviceID) != null)
            {
                throw new ArgumentException("Thiết bị này đã tồn tại trong phòng");
            }

            if (count >= capacity)
            {
                throw new ArgumentException("Phòng đã chứa tối đa");
            }

            devices[count] = device;
            count++;
        }

        public bool RemoveDevice(string deviceId)
        {
            for (int i = 0; i < count; i++)
            {
                if (devices[i].DeviceID == deviceId)
                {
                    for (int j = i; j < count - 1; j++)
                    {
                        devices[j] = devices[j + 1];
                    }
                    devices[count - 1] = null;
                    count--;
                    return true;
                }
            }
            return false;
        }

        public decimal CalculateAnnualMaintenanceCost()
        {
            decimal totalCost = 0;
            for (int i = 0; i < count; i++)
            {
                totalCost += devices[i].CalculateAnnualMaintenanceCost();
            }
            return totalCost;
        }

        public Device[] GetDevicesRequiringMaintenance()
        {
            int currentYear = 2026;
            int matchCount = 0;
            for (int i = 0; i < count; i++)
            {
                if (devices[i].Status == Device.DeviceStatus.UnderMaintenance || (currentYear - devices[i].UsageYear) > 5)
                {
                    matchCount++;
                }
            }

            Device[] result = new Device[matchCount];
            int index = 0;
            for (int i = 0; i < count; i++)
            {
                if (devices[i].Status == Device.DeviceStatus.UnderMaintenance || (currentYear - devices[i].UsageYear) > 5)
                {
                    result[index] = devices[i];
                    index++;
                }
            }

            return result;
        }

        public override string ToString()
        {
            return "Mã phòng: " + roomId + " - Tên phòng: " + roomName + " - Sức chứa: " + capacity + " - Số thiết bị: " + count;
        }
    }
}
