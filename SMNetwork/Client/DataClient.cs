using System.Net;
using System.Net.Sockets;
using SMNetwork;

namespace SMNetwork.Client
{
    public static class DataClient
    {
        public static string Address { get; set; }
        public static int Port { get; set; }

        public static TcpClient Client { get; set; }
        
        public static string Token { get; set; }
        
        public static DataUser User { get; set; }
        
        public static void Initialize(string hosAddress, int port)
        {
            Address = hosAddress;
            Port = port;
            Socket _sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Client = new TcpClient() {Client = _sock};
            Token = "";
            User = null;
        }
    }
}