using System;
using System.Net;
using System.Runtime.Versioning;
using System.Threading;
using SMNetwork;

namespace SMNetwork.Client
{    
    public class Client
    {
        public Client(string address, int port)
        {
            Network.Connect(address, port);
        }

        public bool Create(string login, string firstname, string lastname, string email, string password, string description = "")
        {
            try
            {
                DataUser user = new DataUser();
                user.Login = login;
                user.Firstname = firstname;
                user.Lastname = lastname;
                user.ID = null;
                user.Description = description;
                string result = Network.Create(user, email, password);
                if (result == null)
                {
                    throw new Exception();
                }

                DataClient.Token = result;
                DataClient.User = user;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Connect(string email, string pass)
        {
            try
            {
                string result = Network.Connection(email, pass);
                if (result == null)
                {
                    throw new Exception();
                }

                DataClient.Token = result;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public string AskProgress()
        {
            try
            {
                string result = Network.AskProgress(DataClient.Token);
                if (result == null)
                {
                    throw new Exception();
                }

                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DataUser AskProfil(string email)
        {
            try
            {
                return Network.AskProfil(email);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool UpdateData(string login = null, string firstname = null, string lastname = null, string description = null)
        {
            if (DataClient.User == null)
            {
                return false;
            }

            login = login ?? DataClient.User.Login;
            firstname = firstname ?? DataClient.User.Firstname;
            lastname = lastname ?? DataClient.User.Lastname;
            description = description ?? DataClient.User.Description;

            DataClient.User.Login = login;
            DataClient.User.Firstname = firstname;
            DataClient.User.Lastname = lastname;
            DataClient.User.Description = description;

            try
            {
                string result = Network.UpdateData(DataClient.Token, DataClient.User);
                return result == "success";
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
    }
}