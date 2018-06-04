using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using SMNetwork;

namespace SMNetwork.Server
{
    public static class RequestServer
    {
        #region ConnectAndCreate

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
                        "INSERT INTO user(login, name, firstname, email, pass, description, date_register) VALUES(@login, @name, @firstname, @mail, @pass, @descript, NOW())";
                    MySqlParameter[] parameters_insert = new MySqlParameter[6];
                    parameters_insert[0] = new MySqlParameter("@login", DBManager.Escape(prot.User.Login));
                    parameters_insert[1] = new MySqlParameter("@name", DBManager.Escape(prot.User.Lastname));
                    parameters_insert[2] = new MySqlParameter("@firstname", DBManager.Escape(prot.User.Firstname));
                    parameters_insert[3] = new MySqlParameter("@mail", DBManager.Escape(prot.Email));
                    parameters_insert[4] = new MySqlParameter("@pass", Hash.Sha512(prot.Password));
                    parameters_insert[5] = new MySqlParameter("@descript", DBManager.Escape(prot.User.Description));
                    if (DataServer.Database.Insert(query_insert, parameters_insert))
                    {
                        int time = (int)DateTime.Now.Ticks;
                        string query_select = "SELECT * FROM user WHERE email = @mail";
                        List<Dictionary<string, string>> resList = DataServer.Database.Select(query_select, parameters);
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
                        return new Protocol(MessageType.Error) {Message = "Server Error: Code 2"};
                    }
                    else
                    {
                        return new Protocol(MessageType.Error) {Message = "Server Error: Code 1"};
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
                string password = Hash.Sha512(prot.Password);
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
        
        #endregion

        #region GetData

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
                    MySqlParameter[] parameters_progress = new MySqlParameter[1];
                    parameters_progress[0] = new MySqlParameter("@ID", ID);
                    if (DataServer.Database.Count(query_progress_count, parameters_progress) == 1)
                    {
                        Dictionary<string, string> result =
                            DataServer.Database.Select(query_progress, parameters_progress)[0];
                        result.Remove("ID");
                        result.Remove("ID_user");
                        return new Protocol(MessageType.Response){Progress = JsonConvert.SerializeObject(result)};
                    }
                    else
                    {
                        Dictionary<string, string> data = new Dictionary<string, string>();
                        data.Add("SoloStats", "0");
                        data.Add("MultiStats", "0");
                        data.Add("LastTime", DateTime.Now.ToString());
                        return new Protocol(MessageType.Response){Progress = JsonConvert.SerializeObject(data)};
                    }
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
                    string query_user = "SELECT * FROM user WHERE email = @mail";
                    string query_user_count = "SELECT COUNT(*) FROM user WHERE email = @mail";
                    MySqlParameter[] parameters_user = new MySqlParameter[1];
                    parameters_user[0] = new MySqlParameter("@mail", prot.Email);
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
        
        #endregion

        #region UpdateData

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
                    MySqlParameter[] parameters_user = new MySqlParameter[1];
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

        public static Protocol UpdatePassword(Protocol prot, DataTcpClient client)
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
                   MySqlParameter[] parameters_user = new MySqlParameter[1];
                   parameters_user[0] = new MySqlParameter("@ID", ID);
                   if (DataServer.Database.Count(query_user, parameters_user) == 1)
                   {
                       string pass = prot.Password;
                       string newpass = prot.Message;
                       string query_check_pass = "SELECT COUNT(*) FROM user WHERE ID = @ID AND pass = @pass";
                       MySqlParameter[] parameters_check_pass = new MySqlParameter[2];
                       parameters_check_pass[0] = new MySqlParameter("@ID", ID);
                       parameters_check_pass[1] = new MySqlParameter("@pass", Hash.Sha512(pass));
                       if (DataServer.Database.Count(query_check_pass, parameters_check_pass) == 1)
                       {
                           string query_update = 
                               "UPDATE user SET pass = @pass WHERE ID = @ID";
                           MySqlParameter[] parameters_update = new MySqlParameter[2];
                           parameters_update[0] = new MySqlParameter("@pass", Hash.Sha512(newpass));
                           parameters_update[1] = new MySqlParameter("@ID", ID);
                           if (DataServer.Database.Update(query_update, parameters_update))
                           {
                               return new Protocol(MessageType.Response) {Message = "success"};
                           }
                           return new Protocol(MessageType.Error) {Message = "Server Error"};
                       }
                       return new Protocol(MessageType.Error) {Message = "Bad password"};
                   }
                   return new Protocol(MessageType.Error) {Message = "Bad request"};
               }
               else
               {
                   return new Protocol(MessageType.Error) {Message = "Bad token"};
               }
           }
           catch (Exception e)
           {
               return new Protocol(MessageType.Error) {Message = "Error"};
           }
        }

        public static Protocol UpdateProgress(Protocol prot, DataTcpClient client)
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
                if (prot.Progress == "")
                {
                    throw new Exception();
                }

                Dictionary<string, string> data =
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(prot.Progress);
                
                if (DataServer.Database.Count(query_token, parameters) == 1)
                {
                    string query_progress_count = "SELECT COUNT(*) FROM Game WHERE ID_user = @ID";
                    MySqlParameter[] parameters_progress = new MySqlParameter[1];
                    parameters_progress[0] = new MySqlParameter("@ID", ID);
                    MySqlParameter[] params_progress = new MySqlParameter[4];
                    params_progress[0] = new MySqlParameter("@ID_user", ID);
                    params_progress[1] = new MySqlParameter("@solo", data["SoloStats"]);
                    params_progress[2] = new MySqlParameter("@multi", data["MultiStats"]);
                    params_progress[3] = new MySqlParameter("@date", DateTime.Now.ToString());
                    bool result;
                    if (DataServer.Database.Count(query_progress_count, parameters_progress) == 1)
                    {//update
                        string query_progress = 
                            "UPDATE Game SET SoloStats = @solo, MultiStats = @multi, LastTime = @date WHERE ID_user = @ID_user";
                        result = DataServer.Database.Update(query_progress, params_progress);
                    }
                    else
                    {//insert
                        string query_progress =
                            "INSERT INTO Game(ID_user, SoloStats, MultiStats, LastTime) VALUES(@ID_user, @solo, @multi, @date)";
                        result = DataServer.Database.Insert(query_progress, params_progress);
                    }

                    if (result)
                    {
                        return new Protocol(MessageType.Response) {Message = "success"};
                    }
                    else
                    {
                        return new Protocol(MessageType.Error) {Message = "failed"};
                    }
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
        
        #endregion

        #region Image

        public static Protocol GetImage(Protocol prot, DataTcpClient client)
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
                    string query_user = "SELECT * FROM user WHERE email = @mail";
                    string query_user_count = "SELECT COUNT(*) FROM user WHERE email = @mail";
                    MySqlParameter[] parameters_user = new MySqlParameter[1];
                    parameters_user[0] = new MySqlParameter("@mail", prot.Email);
                    if (DataServer.Database.Count(query_user_count, parameters_user) == 1)
                    {
                        Dictionary<string, string> result =
                            DataServer.Database.Select(query_user, parameters_user)[0];
                        int idavatar = int.Parse(result["avatar_path"]);
                        byte[] img = null;
                        try
                        {
                            string query_img = "SELECT * FROM image WHERE ID = @id";
                            MySqlParameter[] parameters_img = new MySqlParameter[1];
                            parameters_img[0] = new MySqlParameter("@id", idavatar);
                            Dictionary<string, string> result_img =
                                DataServer.Database.Select(query_img, parameters_img)[0];
                            string path = result_img["path"];
                            img = ConvertImage.ImageToByteArray(ConvertImage.FromFile(path));
                        }
                        catch (Exception)
                        {
                            return new Protocol(MessageType.Error) {Message = "invalid request"};
                        }
                        return new Protocol(MessageType.Response)
                        {
                            Email = result["email"], 
                            ImageBytes = img
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
        
        public static Protocol SendImage(Protocol prot, DataTcpClient client)
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
                    MySqlParameter[] parameters_user = new MySqlParameter[1];
                    parameters_user[0] = new MySqlParameter("@ID", ID);
                    if (DataServer.Database.Count(query_user, parameters_user) == 1 && prot.User != null)
                    {
                        if (prot.ImageBytes == null)
                        {
                            return new Protocol(MessageType.Error) {Message = "Empty image"};
                        }
                        Image img = ConvertImage.ByteArrayToImage(prot.ImageBytes);
                        string newpath = "/var/www/accer/data/image/" + (new Random()).Next().ToString() +
                                         DateTime.Now.Ticks.ToString() + ".png";
                        string hash = Hash.Sha1(newpath);
                        ConvertImage.SaveFile(img, newpath);
                        string insert_img = "INSERT INTO image(hash, path, extension) VALUES(@hash, @path, @ext)";
                        MySqlParameter[] parameters_img = new MySqlParameter[3];
                        parameters_img[0] = new MySqlParameter("@hash", hash);
                        parameters_img[1] = new MySqlParameter("@path", newpath);
                        parameters_img[2] = new MySqlParameter("@ext", "png");
                        if (DataServer.Database.Insert(insert_img, parameters_img))
                        {
                            string query_imgid = "SELECT * FROM image WHERE path = @path AND hash = @hash";
                            MySqlParameter[] parameters_imgid = new MySqlParameter[2];
                            parameters_imgid[0] = new MySqlParameter("@path", newpath);
                            parameters_imgid[1] = new MySqlParameter("@hash", hash);
                            Dictionary<string, string> result_imgid =
                                DataServer.Database.Select(query_imgid, parameters_imgid)[0];
                            int idimg = int.Parse(result_imgid["ID"]);
                            string query_update = "UPDATE user SET avatar_path = @idimg WHERE ID = @ID";
                            MySqlParameter[] parameters_update = new MySqlParameter[2];
                            parameters_update[0] = new MySqlParameter("@idimg", idimg);
                            parameters_update[1] = new MySqlParameter("@ID", ID);
                            if (DataServer.Database.Update(query_update, parameters_update))
                            {
                                return new Protocol(MessageType.Response) {Message = "success"};
                            }
                            return new Protocol(MessageType.Error) {Message = "Server Error"};
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
        
        #endregion

        #region Map

        public static Protocol GetMapList(Protocol prot, DataTcpClient client)
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
                    string query_user = "SELECT * FROM user WHERE email = @mail";
                    string query_user_count = "SELECT COUNT(*) FROM user WHERE email = @mail";
                    MySqlParameter[] parameters_user = new MySqlParameter[1];
                    parameters_user[0] = new MySqlParameter("@mail", prot.Email);
                    if (DataServer.Database.Count(query_user_count, parameters_user) == 1)
                    {
                        try
                        {
                            string query_map = "SELECT ID, name, date FROM gamemap";
                            List<Dictionary<string, string>> data =
                                DataServer.Database.Select(query_map, new MySqlParameter[0]);
                            return new Protocol(MessageType.Response)
                            {
                                Message = Formatter.ToJson(data)
                            };
                        }
                        catch (Exception)
                        {
                            return new Protocol(MessageType.Error) {Message = "invalid request"};
                        }
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
        
        public static Protocol GetMapId(Protocol prot, DataTcpClient client)
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
                    string query_user = "SELECT * FROM user WHERE email = @mail";
                    string query_user_count = "SELECT COUNT(*) FROM user WHERE email = @mail";
                    MySqlParameter[] parameters_user = new MySqlParameter[1];
                    parameters_user[0] = new MySqlParameter("@mail", prot.Email);
                    if (DataServer.Database.Count(query_user_count, parameters_user) == 1)
                    {
                        try
                        {
                            int id_map = int.Parse(prot.Message);
                            string query_map = "SELECT ID, name, date, mapjsonzip FROM gamemap WHERE ID = @ID";
                            MySqlParameter[] params_map = new MySqlParameter[1];
                            params_map[0] = new MySqlParameter("@ID", id_map);
                            List<Dictionary<string, string>> data =
                                DataServer.Database.Select(query_map, new MySqlParameter[0]);
                            if (data.Count != 1)
                            {
                                throw new Exception();
                            }
                            return new Protocol(MessageType.Response)
                            {
                                Message = Formatter.ToJson(data[0]),
                                MApJsonZip = data[0]["mapjsonzip"]
                            };
                        }
                        catch (Exception)
                        {
                            return new Protocol(MessageType.Error) {Message = "invalid request"};
                        }
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
        
        public static Protocol SendMap(Protocol prot, DataTcpClient client)
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
                    MySqlParameter[] parameters_user = new MySqlParameter[1];
                    parameters_user[0] = new MySqlParameter("@ID", ID);
                    if (DataServer.Database.Count(query_user, parameters_user) == 1)
                    {
                        try
                        {
                            if (prot.MApJsonZip == "" && prot.Message == "")
                            {
                                throw new Exception();
                            }

                            string mapname = prot.Message;
                            string mapjsonzip = prot.MApJsonZip;
                            string query_insert_map = "INSERT INTO gamemap(ID_user, name, mapjsonzip, date) VALUES(@ID_user, @name, @json, NOW())";
                            MySqlParameter[] params_map = new MySqlParameter[3];
                            params_map[0] = new MySqlParameter("@ID_user", ID);
                            params_map[1] = new MySqlParameter("@name", mapname);
                            params_map[2] = new MySqlParameter("@json", mapjsonzip);
                            if (DataServer.Database.Insert(query_insert_map, params_map))
                            {
                                return new Protocol(MessageType.Response) {Message = "success"};
                            }
                            else
                            {
                                return new Protocol(MessageType.Error) {Message = "Error"};
                            }
                        }
                        catch (Exception)
                        {
                            return new Protocol(MessageType.Error) {Message = "Server Error"};
                        }
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
        
        #endregion

        #region tools

        private static string GenToken(int ID, string login, string email, int timestamp)
        {
            return ID.ToString() + "=+=-*" + Hash.Sha512(login + email + timestamp);
        }
        
        private static bool VerifToken(string dbtoken, string authtoken)
        {
            return dbtoken == authtoken;
        }
        
        private static int GetIdbyToken(string token)
        {
            return int.Parse(token.Split('=')[0]);
        }

        #endregion
    }
}