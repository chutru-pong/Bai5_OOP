using System;

namespace DeviceManagement
{
    public interface INetworkable
    {
        string IpAddress {get; }
        bool IsConnected {get; }
        void Connect(string IpAddress);
        void DisConnect();
    }
}