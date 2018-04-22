using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using SMNetwork;

namespace SMNetwork.Server
{
    public static class DataServer
    {
        public static IPAddress Address { get; set; }
        public static int Port { get; set; }
        
        public static bool Continue { get; set; }

        public static List<DataTcpClient> Clients { get; set; }
        public static Queue<DataTcpClient> Tasks { get; set; }

        public static Socket _sock { get; set; }
        
        public static DBManager Database { get; set; }
        
        public static void Initialize(IPAddress hosAddress, int port, string uidDatabase, string passDatabase)
        {
            Address = hosAddress;
            Port = port;
            Continue = true;
            Clients = new List<DataTcpClient>();
            Tasks = new Queue<DataTcpClient>();
            _sock = new Socket(hosAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            Database = new DBManager(uidDatabase, passDatabase);
        }


    }
}