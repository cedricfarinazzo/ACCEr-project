using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using SMNetwork;

namespace SMNetwork.Server
{
    public static class RequestServer
    {
        public static Protocol Create(Protocol prot, DataTcpClient client)
        {
            string query = "SELECT COUNT(*) FROM user WHERE email = @mail";
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
                        int time = (int)DateTime.Now.Ticks;
                        List<Dictionary<string, string>> resList = DataServer.Database.Select(query, parameters);
                        int ID = int.Parse(resList[0]["ID"]);
                        string email = DBManager.Escape(prot.Email);
                        string login = DBManager.Escape(prot.User.Login);
                        string token = GenToken(ID, login, email, time);
                        string query_token =
                            "INSERT INTO gamesess(ID_user, token, expire) VALUES(@ID, @token, @expire)";
                        MySqlParameter[] parameters_token = new MySqlParameter[3];
                        parameters_token[0] = new MySqlParameter("@ID", ID);
                        parameters_token[1] = new MySqlParameter("@token", token);
                        parameters_token[2] = new MySqlParameter("@expire", time);
                        if (DataServer.Database.Insert(query_token, parameters_token))
                        {
                            return new Protocol(MessageType.Response) {Token = token};
                        }
                        return new Protocol(MessageType.Error) {Message = "Server Error"};
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
            catch (Exception)
            {
                return new Protocol(MessageType.Error) {Message = "Server Error"};
            }
        }
        
        public static Protocol Connection(Protocol prot, DataTcpClient client)
        {
            try
            {
                string email = prot.Email;
                string password = Hash.Create(prot.Password);
                string query = "SELECT * FROM user WHERE email = @mail AND pass = @pass";
                string query_count = "SELECT COUNT(*) FROM user WHERE email = @mail AND pass = @pass";
                MySqlParameter[] parameters = new MySqlParameter[2];
                parameters[0] = new MySqlParameter("@mail", DBManager.Escape(email));
                parameters[1] = new MySqlParameter("@pass", password);
                if (DataServer.Database.Count(query_count, parameters) == 1)
                {
                    Dictionary<string,string> result = DataServer.Database.Select(query, parameters)[0];
                    int time = (int)DateTime.Now.Ticks;
                    string login = result["login"];
                    int ID = int.Parse(result["ID"]);
                    string token = GenToken(ID, login, email, time);
                    string query_token =
                        "INSERT INTO gamesess(ID_user, token, expire) VALUES(@ID, @token, @expire)";
                    MySqlParameter[] parameters_token = new MySqlParameter[3];
                    parameters_token[0] = new MySqlParameter("@ID", ID);
                    parameters_token[1] = new MySqlParameter("@token", token);
                    parameters_token[2] = new MySqlParameter("@expire", time);
                    if (DataServer.Database.Insert(query_token, parameters_token))
                    {
                        return new Protocol(MessageType.Response) {Token = token};
                    }
                    return new Protocol(MessageType.Error) {Message = "Server Error"};
                }
                else
                {
                    return new Protocol(MessageType.Error) {Message = "Bad email or password"};
                }
            }
            catch (Exception)
            {
                return new Protocol(MessageType.Error) {Message = "Server Error"};
            }
        }
        
        public static Protocol AskProgress(Protocol prot, DataTcpClient client)
        {
            try
            {
                if (prot.Token == "")
                {
                    return new Protocol(MessageType.Error) {Message = "Empty token"};
                }
                int ID = GetIdbyToken(prot.Token);
                string query_token = "SELECT COUNT(*) FROM gamesess WHERE ID_user = @ID AND token = @token";
                MySqlParameter[] parameters = new MySqlParameter[2];
                parameters[0] = new MySqlParameter("@ID", ID);
                parameters[1] = new MySqlParameter("@token", DBManager.Escape(prot.Token));
                if (DataServer.Database.Count(query_token, parameters) == 1)
                {
                    string query_progress = "SELECT * FROM Game WHERE ID_user = @ID";
                    string query_progress_count = "SELECT COUNT(*) FROM Game WHERE ID_user = @ID";
                    MySqlParameter[] parameters_progress = new MySqlParameter[2];
                    parameters_progress[0] = new MySqlParameter("@ID", ID);
                    if (DataServer.Database.Count(query_progress_count, parameters_progress) == 1)
                    {
                        Dictionary<string, string> result =
                            DataServer.Database.Select(query_progress, parameters_progress)[0];
                        result.Remove("ID");
                        result.Remove("ID_user");
                        return new Protocol(MessageType.Response){Progress = JsonConvert.SerializeObject(result)};
                    }
                    return new Protocol(MessageType.Error) {Message = "Bad request"};
                }
                else
                {
                    return new Protocol(MessageType.Error) {Message = "Bad token"};
                }
            }
            catch (Exception)
            {
                return new Protocol(MessageType.Error) {Message = "Error"};
            }
        }
        
        public static Protocol AskProfil(Protocol prot, DataTcpClient client)
        {
            try
            {
                if (prot.Token == "")
                {
                    return new Protocol(MessageType.Error) {Message = "Empty token"};
                }
                int ID = GetIdbyToken(prot.Token);
                string query_token = "SELECT COUNT(*) FROM gamesess WHERE ID_user = @ID AND token = @token";
                MySqlParameter[] parameters = new MySqlParameter[2];
                parameters[0] = new MySqlParameter("@ID", ID);
                parameters[1] = new MySqlParameter("@token", DBManager.Escape(prot.Token));
                if (DataServer.Database.Count(query_token, parameters) == 1)
                {
                    string query_user = "SELECT * FROM user WHERE ID = @ID";
                    string query_user_count = "SELECT COUNT(*) FROM user WHERE ID = @ID";
                    MySqlParameter[] parameters_user = new MySqlParameter[2];
                    parameters_user[0] = new MySqlParameter("@ID", ID);
                    if (DataServer.Database.Count(query_user_count, parameters_user) == 1)
                    {
                        Dictionary<string, string> result =
                            DataServer.Database.Select(query_user, parameters_user)[0];
                        DataUser user = new DataUser();
                        user.Description = result["description"];
                        user.Firstname = result["firstname"];
                        user.ID = result["ID"];
                        user.Lastname = result["name"];
                        user.Login = result["login"];
                        return new Protocol(MessageType.Response)
                        {
                            Email = result["email"], 
                            User = user
                        };
                    }
                    return new Protocol(MessageType.Error) {Message = "Bad request"};
                }
                else
                {
                    return new Protocol(MessageType.Error) {Message = "Bad token"};
                }
            }
            catch (Exception)
            {
                return new Protocol(MessageType.Error) {Message = "Error"};
            }
        }
        
        public static Protocol UpdateData(Protocol prot, DataTcpClient client)
        {
            try
            {
                if (prot.Token == "")
                {
                    return new Protocol(MessageType.Error) {Message = "Empty token"};
                }
                int ID = GetIdbyToken(prot.Token);
                string query_token = "SELECT COUNT(*) FROM gamesess WHERE ID_user = @ID AND token = @token";
                MySqlParameter[] parameters = new MySqlParameter[2];
                parameters[0] = new MySqlParameter("@ID", ID);
                parameters[1] = new MySqlParameter("@token", DBManager.Escape(prot.Token));
                if (DataServer.Database.Count(query_token, parameters) == 1)
                {
                    string query_user = "SELECT COUNT(*) FROM user WHERE ID = @ID";
                    MySqlParameter[] parameters_user = new MySqlParameter[2];
                    parameters_user[0] = new MySqlParameter("@ID", ID);
                    if (DataServer.Database.Count(query_user, parameters_user) == 1 && prot.User != null)
                    {
                        DataUser user = prot.User;
                        string query_update = 
                            "UPDATE user SET name = @name, firstname = @firstname, login = @login, description = @description WHERE ID = @ID";
                        MySqlParameter[] parameters_update = new MySqlParameter[5];
                        parameters_update[0] = new MySqlParameter("@name", prot.User.Lastname);
                        parameters_update[1] = new MySqlParameter("@firstname", prot.User.Firstname);
                        parameters_update[2] = new MySqlParameter("@login", prot.User.Login);
                        parameters_update[3] = new MySqlParameter("@description", prot.User.Description);
                        parameters_update[4] = new MySqlParameter("@ID", ID);
                        if (DataServer.Database.Update(query_update, parameters_update))
                        {
                            return new Protocol(MessageType.Response) {Message = "success"};
                        }
                        
                        return new Protocol(MessageType.Error) {Message = "Server Error"};
                        
                    }
                    return new Protocol(MessageType.Error) {Message = "Bad request"};
                }
                else
                {
                    return new Protocol(MessageType.Error) {Message = "Bad token"};
                }
            }
            catch (Exception)
            {
                return new Protocol(MessageType.Error) {Message = "Error"};
            }
        }

        public static Protocol Logout(Protocol prot, DataTcpClient client)
        {
            try
            {
                if (prot.Token == "")
                {
                    return new Protocol(MessageType.Error) {Message = "Empty token"};
                }
                int ID = GetIdbyToken(prot.Token);
                string query = "DELETE FROM gamesess WHERE ID_user = @ID AND token = @token";
                MySqlParameter[] parameters = new MySqlParameter[2];
                parameters[0] = new MySqlParameter("@ID", ID);
                parameters[1] = new MySqlParameter("@token", prot.Token);
                if (DataServer.Database.Delete(query, parameters))
                {
                    return new Protocol(MessageType.Response) {Message = "success"};
                }
                return new Protocol(MessageType.Response) {Message = "failed"};
                
            }
            catch (Exception)
            {
                return new Protocol(MessageType.Error) {Message = "Error"};
            }
        }

        private static string GenToken(int ID, string login, string email, int timestamp)
        {
            return ID.ToString() + "=+=-*" + Hash.Create(login + email + timestamp);
        }

        private static bool VerifToken(string dbtoken, string authtoken)
        {
            return dbtoken == authtoken;
        }

        private static int GetIdbyToken(string token)
        {
            return int.Parse(token.Split('=')[0]);
        }
    }
}