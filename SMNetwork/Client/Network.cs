using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;
using Newtonsoft.Json;
using SMNetwork;

namespace SMNetwork.Client
{
    public static class Network
    {

        public static void Connect(string address, int port)
        {
            DataClient.Initialize(address, port);
            try
            {
                DataClient.Client.Client.Connect(DataClient.Address, DataClient.Port);
            }
            catch (Exception e)
            {
                DataClient.Client.Client.Connect(DataClient.IpAddressEntry.AddressList, port);
            }
        }
        
        private static Protocol ReceiveMessage()
        {
            while (DataClient.Client.Client.Connected && DataClient.Client.Client.Available <= 1)
            {
            }

            if (!DataClient.Client.Client.Connected)
                throw new Exception("Disconnected from server.");
            var message = new List<byte>();
            var stream = DataClient.Client.GetStream();

#pragma warning disable CS0652
            while (stream.DataAvailable)
                message.Add((byte) stream.ReadByte());
#pragma warning restore CS0652

            var msg = Formatter.ToObject<Protocol>(message.ToArray());
            return msg;
        }

        public static string Connection(string email, string password)
        {
            Protocol reqProtocol = new Protocol(MessageType.Connection)
            {
                Email = email,
                Password = password
            };
            byte[] buffer = Formatter.ToByteArray(reqProtocol);
            DataClient.Client.Client.Send(buffer, SocketFlags.None);
            Protocol receiveMessage = ReceiveMessage();

            if (receiveMessage.Type != MessageType.Response)
            {
                return null;
            }

            return receiveMessage.Token;
        }

        public static string Create(DataUser user, string email, string password)
        {
            Protocol reqProtocol = new Protocol(MessageType.Create)
            {
                User = user,
                Email = email,
                Password = password
            };
            byte[] buffer = Formatter.ToByteArray(reqProtocol);
            DataClient.Client.Client.Send(buffer, SocketFlags.None);
            Protocol receiveMessage = ReceiveMessage();
            Console.WriteLine(receiveMessage.Message);
            if (receiveMessage.Type != MessageType.Response)
            {
                return null;
            }

            return receiveMessage.Token;
        }
        
        public static Dictionary<string, string> AskProgress(string token)
        {
            Protocol reqProtocol = new Protocol(MessageType.AskProgress)
            {
                Token = token
            };
            byte[] buffer = Formatter.ToByteArray(reqProtocol);
            DataClient.Client.Client.Send(buffer, SocketFlags.None);
            Protocol receiveMessage = ReceiveMessage();
            if (receiveMessage.Type != MessageType.Response)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<Dictionary<string, string>>(receiveMessage.Progress);
        }
        
        public static bool UpdateProgress(string token, Dictionary<string, string> data)
        {
            Protocol reqProtocol = new Protocol(MessageType.UpdateData)
            {
                Token = token,
                Progress = JsonConvert.SerializeObject(data)
            };
            byte[] buffer = Formatter.ToByteArray(reqProtocol);
            DataClient.Client.Client.Send(buffer, SocketFlags.None);
            Protocol receiveMessage = ReceiveMessage();
            if (receiveMessage.Type != MessageType.Response)
            {
                return false;
            }

            return true;
        }
        
        public static DataUser AskProfil(string email, string token)
        {
            Protocol reqProtocol = new Protocol(MessageType.AskProfil)
            {
                Token = token,
                Email = email
            };
            byte[] buffer = Formatter.ToByteArray(reqProtocol);
            DataClient.Client.Client.Send(buffer, SocketFlags.None);
            Protocol receiveMessage = ReceiveMessage();
            
            if (receiveMessage.Type != MessageType.Response)
            {
                return null;
            }

            return receiveMessage.User;
        }

        public static bool UpdateData(string token, DataUser user)
        {
            Protocol reqProtocol = new Protocol(MessageType.UpdateData)
            {
                User = user,
                Token = token
            };
            byte[] buffer = Formatter.ToByteArray(reqProtocol);
            DataClient.Client.Client.Send(buffer, SocketFlags.None);
            Protocol receiveMessage = ReceiveMessage();
            
            if (receiveMessage.Type != MessageType.Response)
            {
                return false;
            }

            return receiveMessage.Message == "success";
        }

        public static bool Logout(string token)
        {
            Protocol reqProtocol = new Protocol(MessageType.Logout);
            reqProtocol.Token = token;
            byte[] buffer = Formatter.ToByteArray(reqProtocol);
            DataClient.Client.Client.Send(buffer, SocketFlags.None);
            Protocol receiveMessage = ReceiveMessage();
            
            if (receiveMessage.Type != MessageType.Response)
            {
                return false;
            }

            return receiveMessage.Message == "success";
        }

        public static byte[] GetImage(string email, string token)
        {
            Protocol reqProtocol = new Protocol(MessageType.GetImage)
            {
                Token = token,
                Email = email
            };
            byte[] buffer = Formatter.ToByteArray(reqProtocol);
            DataClient.Client.Client.Send(buffer, SocketFlags.None);
            Protocol receiveMessage = ReceiveMessage();
            
            if (receiveMessage.Type != MessageType.Response)
            {
                return null;
            }

            return receiveMessage.ImageBytes;
        }

        public static bool SendImage(string email, string token, byte[] imageBytes)
        {
            Protocol reqProtocol = new Protocol(MessageType.SendImage)
            {
                Token = token,
                Email = email,
                ImageBytes = imageBytes
            };
            byte[] buffer = Formatter.ToByteArray(reqProtocol);
            DataClient.Client.Client.Send(buffer, SocketFlags.None);
            Protocol receiveMessage = ReceiveMessage();
            
            if (receiveMessage.Type != MessageType.Response)
            {
                return false;
            }

            return receiveMessage.Message == "success";
        }

        public static bool UpadatePassword(string token, string pass, string newpass)
        {
            Protocol reqProtocol = new Protocol(MessageType.UpdatePassword)
            {
                Token = token,
                Password = pass,
                Message = newpass
            };
            byte[] buffer = Formatter.ToByteArray(reqProtocol);
            DataClient.Client.Client.Send(buffer, SocketFlags.None);
            Protocol receiveMessage = ReceiveMessage();
            
            if (receiveMessage.Type != MessageType.Response)
            {
                return false;
            }

            return receiveMessage.Message == "success";
        }
        
        public static List<Dictionary<string, string>> AskMapList(string token)
        {
            Protocol reqProtocol = new Protocol(MessageType.GetMapList)
            {
                Token = token
            };
            byte[] buffer = Formatter.ToByteArray(reqProtocol);
            DataClient.Client.Client.Send(buffer, SocketFlags.None);
            Protocol receiveMessage = ReceiveMessage();
            if (receiveMessage.Type != MessageType.Response)
            {
                return new List<Dictionary<string, string>>();
            }

            return JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(receiveMessage.Progress);
        }
        
        public static Dictionary<string, string> AskMapId(string token, int id)
        {
            Protocol reqProtocol = new Protocol(MessageType.GetMapId)
            {
                Token = token,
                Message = id.ToString()
            };
            byte[] buffer = Formatter.ToByteArray(reqProtocol);
            DataClient.Client.Client.Send(buffer, SocketFlags.None);
            Protocol receiveMessage = ReceiveMessage();
            if (receiveMessage.Type != MessageType.Response)
            {
                return new Dictionary<string, string>();
            }
            Dictionary<string, string> data = JsonConvert.DeserializeObject<Dictionary<string, string>>(receiveMessage.Progress);
            data.Add("mapjsonzip", receiveMessage.MApJsonZip);
            return data;
        }
        
        public static bool SendMap(string token, string json, string name)
        {
            Protocol reqProtocol = new Protocol(MessageType.SendMap)
            {
                Token = token,
                MApJsonZip = json,
                Message = name
            };
            byte[] buffer = Formatter.ToByteArray(reqProtocol);
            DataClient.Client.Client.Send(buffer, SocketFlags.None);
            Protocol receiveMessage = ReceiveMessage();
            if (receiveMessage.Type != MessageType.Response)
            {
                return false;
            }

            return true;
        }
        
    }
}