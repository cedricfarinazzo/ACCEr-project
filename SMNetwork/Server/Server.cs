using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using SMNetwork;

namespace SMNetwork.Server
{
    public class Server
    {
        public Server(int port)
        {
            UPnP upnp = new UPnP(port);
            DataServer.Initialize(IPAddress.Any, port);
            this.Init();
        }

        public void Init()
        {
            DataServer._sock.Bind(new IPEndPoint(DataServer.Address, DataServer.Port));
            DataServer._sock.Listen(42);
        }

        public void Start()
        {
            Console.WriteLine("[S] Server Started.");
            Thread accept = new Thread(AcceptClients);
            accept.Priority = ThreadPriority.AboveNormal;
            accept.IsBackground = true;
            Thread tasks = new Thread(HandleTasks);
            tasks.Priority = ThreadPriority.Highest;
            tasks.IsBackground = true;
            Thread poll = new Thread(PollClients);
            poll.Priority = ThreadPriority.Highest;
            poll.IsBackground = true;

            try
            {
                accept.Start();
                tasks.Start();
                poll.Start();
                poll.Join();
                
            }
            catch (Exception e)
            {
                Console.WriteLine("[S] Exception during execution of the server");
                Console.WriteLine(e);
                throw;
            }
            return;
        }

        public void AcceptClients()
        {
            while (DataServer.Continue)
            {
                try
                {
                    Socket clientSocket = DataServer._sock.Accept();
                    Console.WriteLine("[S] Accept connection from " + clientSocket.RemoteEndPoint.ToString());
                    TcpClient client = new TcpClient
                    {
                        Client = clientSocket
                    };
                    DataServer.Clients.Add(new DataTcpClient(client));
                    
                }
                catch (Exception)
                {
                    break;
                }
            }
            return;
        }

        public void PollClients()
        {
            while (true)
            {
                int i = 0;
                while (i < DataServer.Clients.Count)
                {
                    try
                    {
                        var client = DataServer.Clients[i];
                        if (client == null || !client.Client.Connected)
                            DataServer.Clients.Remove(client);
                        else if (client.Available())
                        {
                            client.IsQueued = true;
                            DataServer.Tasks.Enqueue(client);
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    i++;
                }
            }
            return;
        }

        public void HandleTasks()
        {
            while (true)
            {
                if (DataServer.Tasks.Count == 0)
                {
                    continue;
                }

                DataTcpClient client = DataServer.Tasks.Dequeue();

                try
                {
                    Thread requestTh = new Thread(() => this.HandleRequests(client)) { IsBackground = true};
                    requestTh.Start();
                }
                catch (Exception e)
                {
                    client.IsQueued = false;
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
            return;
        }

        public void HandleRequests(DataTcpClient client)
        {

            Protocol msg = Receive(client.Client);
            Protocol resp;
            switch (msg.Type)
            {
                case MessageType.AskProfil:
                    Console.WriteLine("[SINFO][AskProfil] request from " + client.Client.Client.RemoteEndPoint.ToString());
                    resp = RequestServer.AskProfil(msg, client);
                    break;
                        
                case MessageType.AskProgress:
                    Console.WriteLine("[SINFO][AskProgress] request from " + client.Client.Client.RemoteEndPoint.ToString());
                    resp = RequestServer.AskProgress(msg, client);
                    break;
                    
                case MessageType.Connection:
                    Console.WriteLine("[SINFO][Connection] request from " + client.Client.Client.RemoteEndPoint.ToString());
                    resp = RequestServer.Connection(msg, client);
                    break;
                    
                case MessageType.Create:
                    Console.WriteLine("[SINFO][Create] request from " + client.Client.Client.RemoteEndPoint.ToString());
                    resp = RequestServer.Create(msg, client);
                    break;
                    
                case MessageType.UpdateData:
                    Console.WriteLine("[SINFO][UpdateData] request from " + client.Client.Client.RemoteEndPoint.ToString());
                    resp = RequestServer.UpdateData(msg, client);
                    break;

                default:
                    Console.WriteLine("[SINFO][Unknown] request from " + client.Client.Client.RemoteEndPoint.ToString());
                    resp = new Protocol(MessageType.Error)
                        { Message = "You did something, but I don't know what." };
                    break;
            }
            Send(client.Client, resp);
            client.IsQueued = false;
            return;
        }

        private Protocol Receive(TcpClient client)
        {
            var message = new List<byte>();
            NetworkStream stream = client.GetStream();

            while (stream.DataAvailable)
            {
                message.Add((byte) stream.ReadByte());
            }

            return Formatter.ToObject<Protocol>(message.ToArray());
        }

        private void Send(TcpClient client, Protocol protocol)
        {
            client.Client.Send(Formatter.ToByteArray(protocol));
            return;
        }
        
    }
}