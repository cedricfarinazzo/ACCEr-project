using System;
using System.Drawing;
using System.Net;
using System.Runtime.Versioning;
using System.Threading;
using SMNetwork;

namespace SMNetwork.Client
{    
    public class Client
    {
        public Client(string address = "accer.ddns.net", int port = 4247)
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
                DataClient.Email = email;
                return true;
            }
            catch (Exception)
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
                DataClient.Email = email;
                return true;
            }
            catch (Exception)
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
            catch (Exception)
            {
                return null;
            }
        }

        public DataUser AskProfil(string email)
        {
            try
            {
                return Network.AskProfil(email, DataClient.Token);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataUser AskMyProfil()
        {
            DataClient.User = AskProfil(DataClient.Email);
            return DataClient.User;
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
                return Network.UpdateData(DataClient.Token, DataClient.User);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Image AskImage(string email)
        {
            try
            {
                return ConvertImage.ByteArrayToImage(Network.GetImage(email, DataClient.Token));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Image AskMyImage()
        {
            return AskImage(DataClient.Email);
        }

        public bool UpdateImage(Image img)
        {
            try
            {
                return Network.SendImage(DataClient.Email, DataClient.Token, ConvertImage.ImageToByteArray(img));
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpadatePassword(string oldpass, string newpass)
        {
            try
            {
                return Network.UpadatePassword(DataClient.Token, oldpass, newpass);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Logout()
        {
            bool result = Network.Logout(DataClient.Token);
            DataClient.Token = "";
            return result;
        }
    }
}