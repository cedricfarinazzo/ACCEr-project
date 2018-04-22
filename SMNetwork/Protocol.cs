using System;
using System.Collections.Generic;

namespace SMNetwork
{
    public enum MessageType
    {
        Response,
        Error,      
        Connection,
        Logout,
        Create,
        AskProgress,
        AskProfil,
        UpdateData,
    }
    
    [Serializable]
    public class Protocol
    {
        public Protocol(MessageType type)
        {
            this.Type = type;
            this.Message = "";
            this.User = null;
            this.Email = null;
            this.Password = null;
            this.Token = "";

        }
        
        public MessageType Type { get; set; }
        public string Message { get; set; }
        public DataUser User { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string Progress { get; set; }
        
    }
}