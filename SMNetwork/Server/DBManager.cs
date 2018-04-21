using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace SMNetwork.Server
{
    public class DBManager
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DBManager(string uid, string password)
        {
            this.uid = uid;
            this.password = password;
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            this.server = "localhost";
            this.database = "accer";
            string connectionString;
            connectionString = "SERVER=" + this.server + ";" + "DATABASE=" + 
                               this.database + ";" + "UID=" + this.uid + ";" + "PASSWORD=" + this.password + ";";

            this.connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Insert statement
        public void Insert(string query, List<MySqlParameter> parameters)
        {
            if(this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, this.connection);
                foreach (MySqlParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
                    

                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Update statement
        public void Update()
        {
        }

        //Delete statement
        public void Delete()
        {
        }

        //Select statement
        public List<string>[] Select()
        {
        }

        //Count statement
        public int Count()
        {
        }

        //Backup
        public void Backup()
        {
        }

        //Restore
        public void Restore()
        {
        }
    }
}