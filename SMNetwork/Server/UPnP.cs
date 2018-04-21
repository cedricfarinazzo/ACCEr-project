using System;
using Mono.Nat;
using SMNetwork;

namespace SMNetwork.Server
{
    public class UPnP
    {
        protected int port;
        
        public UPnP(int port)
        {
            NatUtility.DeviceFound += DeviceFound;
            NatUtility.DeviceLost += DeviceLost;
            NatUtility.StartDiscovery();
        }
        
        private void DeviceFound(object sender, DeviceEventArgs args)
        {
            INatDevice device = device = args.Device;
            device.CreatePortMap(new Mapping(Mono.Nat.Protocol.Tcp, this.port, this.port));
 
            foreach (Mapping portMap in device.GetAllMappings())
            {
                Console.WriteLine(portMap.ToString());
            }
 
            Console.WriteLine(device.GetExternalIP().ToString());
        }
 
        private void DeviceLost(object sender, DeviceEventArgs args)
        {
            INatDevice device = args.Device;           
            device.DeletePortMap(new Mapping(Mono.Nat.Protocol.Tcp, this.port, this.port));
        }
    }
}