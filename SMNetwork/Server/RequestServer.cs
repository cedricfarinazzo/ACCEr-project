using System;
using SMNetwork;

namespace SMNetwork.Server
{
    public static class RequestServer
    {
        public static Protocol Create(Protocol prot, DataTcpClient client)
        {
            return new Protocol(MessageType.Error);
        }
        
        public static Protocol Connection(Protocol prot, DataTcpClient client)
        {
            return new Protocol(MessageType.Error);
        }
        
        public static Protocol AskProgress(Protocol prot, DataTcpClient client)
        {
            return new Protocol(MessageType.Error);
        }
        
        public static Protocol AskProfil(Protocol prot, DataTcpClient client)
        {
            return new Protocol(MessageType.Error);
        }
        
        public static Protocol UpdateData(Protocol prot, DataTcpClient client)
        {
            return new Protocol(MessageType.Error);
        }
    }
}