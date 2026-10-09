using System;

namespace DeviceManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // 2 máy tính, 1 máy có GPU rời
            // Computer(deviceID, deviceName, usageYear,purchasePrice, ramGB, cpuType,dedicatedGPU,  DeviceStatus)
            Computer pc1 = new Computer("PC01", "Dell OptiPlex 7090", 2022, 15000000m, 16, "Core i5", false, Device.DeviceStatus.Active);
            Computer pc2 = new Computer("PC02", "ASUS ROG Gaming", 2018, 30000000m, 32, "Core i7", true, Device.DeviceStatus.UnderMaintenance);

            // 2 máy in, 1 máy in > 100.000 trang
            // Printer(deviceID, deviceName, usageYear,purchasePrice, type, pageCount, hasNetwork)
            Printer pr1 = new Printer("PR01", "Canon LBP2900", 2023, 4000000m, "laser", 20000, false);
            NetworkPrinter pr2 = new NetworkPrinter("PR02", "HP LaserJet Pro", 2019, 12000000m, "laser", 120000, true, "192.168.1.50");

            // bóng đèn sử dụng > 3.000 giờ
            // Projector(deviceID, deviceName, usageYear,purchasePrice,lumens, lampHoursUsed)
            Projector pj1 = new Projector("PJ01", "Sony VPL-EX570", 2020, 10000000m, 4000, 3500);

            // 2 phòng thực hành
            // LabRoom(roomId, roomName, capacity)
            LabRoom lab1 = new LabRoom("LAB01", "Phòng Máy Tính 1", 30);
            LabRoom lab2 = new LabRoom("LAB02", "Phòng Đa Phương Tiện 2", 20);

            //1 Thêm thiết bị vào phòng
            lab1.AddDevice(pc1);
            lab1.AddDevice(pc2);
            lab1.AddDevice(pr1);
            lab2.AddDevice(pr2);
            lab2.AddDevice(pj1);
            Console.WriteLine("Đã thêm PC01, PC02, PR01 vào phòng 1.");
            Console.WriteLine("Đã thêm PR02, PJ01 vào phòng 2.");

            //2 Thử thêm thiết bị bị trùng mã
            Console.WriteLine("\n Thêm thiết bị trùng mã PC01 vào LAB01:");
            try
            {
                Computer pcTrung = new Computer("PC01", "Máy tính trùng mã", 2024, 12000000m, 8, "Core i3", false);
                lab1.AddDevice(pcTrung);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //3 In danh sách thiết bị trong từng phòng
            Console.WriteLine("\nDanh sách thiết bị trong từng phòng:");
            Console.WriteLine(lab1.ToString());
            Console.WriteLine(lab2.ToString());

            //4 Tính tổng chi phí bảo trì dự kiến của mỗi phòng
            Console.WriteLine("\nTổng chi phí bảo trì dự kiến của mỗi phòng:");
            Console.WriteLine("Chi phí phòng 1: " + lab1.CalculateAnnualMaintenanceCost().ToString("N0"));
            Console.WriteLine("Chi phí phòng 2: " + lab2.CalculateAnnualMaintenanceCost().ToString("N0"));

            // Liệt kê thiết bị cần bảo trì
            Console.WriteLine("\nLiệt kê thiết bị cần bảo trì:");
            Device[] bt1 = lab1.GetDevicesRequiringMaintenance();
            Console.WriteLine("Phòng 1, " + bt1.Length + " thiết bị:");
            for (int i = 0; i < bt1.Length; i++)
            {
                Console.WriteLine("  + " + bt1[i].ToString());
            }

            Device[] bt2 = lab2.GetDevicesRequiringMaintenance();
            Console.WriteLine("Phòng 2, " + bt2.Length + " thiết bị:");
            for (int i = 0; i < bt2.Length; i++)
            {
                Console.WriteLine("  + " + bt2[i].ToString());
            }

            //6 Kết nối mạng cho các đối tượng thực thi INetworkable
            Console.WriteLine("\n Kết nối mạng cho các đối tượng thực thi INetworkable:");
            pc1.Connect("192.168.1.101");
            pc2.Connect("192.168.1.102");
            pr2.Connect("192.168.1.103");
            Console.WriteLine("Đã gọi Connect() cho PC01, PC02 và PR02.");

            //7 Duyệt các thiết bị mạng thông qua kiểu INetworkable
            Console.WriteLine("\n Duyệt thiết bị mạng thông qua kiểu INetworkable:");
            Device[] allDevices = new Device[] { pc1, pc2, pr1, pr2, pj1 };
            for (int i = 0; i < allDevices.Length; i++)
            {
                if (allDevices[i] is INetworkable)
                {
                    INetworkable net = (INetworkable)allDevices[i];
                    Console.WriteLine(allDevices[i].ToString());
                    Console.WriteLine("   IP: " + net.IpAddress + " | Status: " + (net.IsConnected ? "Connected" : "Disconnected"));
                }
            }
        }
    }
}
