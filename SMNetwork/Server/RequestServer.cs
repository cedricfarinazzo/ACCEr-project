using System;
using MySql.Data.MySqlClient;
using SMNetwork;

namespace SMNetwork.Server
{
    public static class RequestServer
    {
        public static Protocol Create(Protocol prot, DataTcpClient client)
        {
            string query = "SELECT * FROM user WHERE email = @mail";
            MySqlParameter[] parameters = new MySqlParameter[1];
            parameters[0] = new MySqlParameter("@mail", DBManager.Escape(prot.Email));
            try
            {
                if (prot.User == null)
                {
                    return new Protocol(MessageType.Error) {Message = "Empty Data"};
                }
                int result = DataServer.Database.Count(query, parameters);
                if (result == 0)
                {
                    string query_insert =
                        "INSERT INTO user(login, name, firstname, email, pass, date_register) VALUES(@login, @name, @firstname, @mail, @pass, NOW())";
                    MySqlParameter[] parameters_insert = new MySqlParameter[5];
                    parameters_insert[0] = new MySqlParameter("@login", DBManager.Escape(prot.User.Login));
                    parameters_insert[1] = new MySqlParameter("@name", DBManager.Escape(prot.User.Lastname));
                    parameters_insert[2] = new MySqlParameter("@firstname", DBManager.Escape(prot.User.Firstname));
                    parameters_insert[3] = new MySqlParameter("@mail", DBManager.Escape(prot.Email));
                    parameters_insert[4] = new MySqlParameter("@pass", Hash.Create(prot.Password));

                    if (DataServer.Database.Insert(query_insert, parameters_insert))
                    {
                        
                    }
                    else
                    {
                        return new Protocol(MessageType.Error) {Message = "Server Error"};
                    }
                }
                else
                {
                    return new Protocol(MessageType.Error) {Message = "This email already exists"};
                }
            }
            catch (Exception e)
            {
                return new Protocol(MessageType.Error) {Message = "Server Error"};
            }
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

        public static Protocol Logout(Protocol prot, DataTcpClient client)
        {
            return new Protocol(MessageType.Error);
        }

        private static string GenToken(int ID, string login, string email, int timestamp)
        {
            return ID.ToString() + "=+=-*" + Hash.Create(login + email + timestamp);
        }
    }
}