﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;
using SMNetwork;

namespace SMNetwork.Client
{
    public static class Network
    {

        public static void Connect(string address, int port)
        {
            DataClient.Initialize(address, port);
            DataClient.Client.Client.Connect(DataClient.Address, DataClient.Port);
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
        
        public static string AskProgress(string token)
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

            return receiveMessage.Progress;
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
        
    }
}