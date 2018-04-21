using System.Net.Sockets;
using SMNetwork;

namespace SMNetwork.Server
{
    public class DataTcpClient
    {
        public DataUser User { get; set; }
        
        public TcpClient Client { get; }
        
        public bool IsQueued { get; set; }
        
        public DataTcpClient(TcpClient client)
        {
            this.Client = client;
            this.IsQueued = false;
        }

        public bool Available()
        {
            if (this.IsQueued)
            {
                return false;
            }

            return Client.Available > 0;
        }
    }
}