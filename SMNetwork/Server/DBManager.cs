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
        public bool Insert(string query, MySqlParameter[] parameters)
        {
            try
            {
                if(this.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, this.connection);
                    cmd.Prepare();
                    foreach (MySqlParameter param in parameters)
                    {
                        cmd.Parameters.Add(param);
                    }
                        
                
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return true;
                }
                throw new Exception("DBmanager.Insert: Connection failed to mysql server");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }       
        }

        //Update statement
        public bool Update(string query, MySqlParameter[] parameters)
        {
            try
            {
                if(this.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, this.connection);
                    cmd.Prepare();
                    foreach (MySqlParameter param in parameters)
                    {
                        cmd.Parameters.Add(param);
                    }
                        
                
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return true;
                }
                throw new Exception("DBmanager.Update: Connection failed to mysql server");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }       
        }

        //Delete statement
        public bool Delete(string query, MySqlParameter[] parameters)
        {
            try
            {
                if(this.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, this.connection);
                    cmd.Prepare();
                    foreach (MySqlParameter param in parameters)
                    {
                        cmd.Parameters.Add(param);
                    }
                        
                
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return true;
                }
                throw new Exception("DBmanager.Delete: Connection failed to mysql server");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }       
        }

        //Select statement
        public List<Dictionary<string,string>> Select(string query, MySqlParameter[] parameters)
        {
            try
            {
                //Open connection
                if (this.OpenConnection())
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Prepare();
                    foreach (MySqlParameter param in parameters)
                    {
                        cmd.Parameters.Add(param);
                    }
                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();


                    List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
                
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        int nb_data = dataReader.FieldCount;
                        Dictionary<string,string> data_dico = new Dictionary<string, string>();
                        for (int i = 0; i < nb_data; i++)
                        {
                            data_dico.Add((string) dataReader.GetName(i), (string) dataReader[dataReader.GetName(i)]);
                        }
                        data.Add(data_dico);
                    }

                    //close Data Reader
                    dataReader.Close();

                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed
                    return data;
                }
                else
                {
                    throw new Exception("DBmanager.Select: Connection failed to mysql server");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        //Count statement
        public int Count(string query, MySqlParameter[] parameters)
        {
            try
            {
                //Open connection
                if (this.OpenConnection())
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Prepare();
                    foreach (MySqlParameter param in parameters)
                    {
                        cmd.Parameters.Add(param);
                    }
                    
                    int Count = int.Parse(cmd.ExecuteScalar()+"");
                    

                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed
                    return Count;
                }
                else
                {
                    throw new Exception("DBmanager.Count: Connection failed to mysql server");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Prevent SQL injection. Remove for more fun.
        /// </summary>
        /// <param name="str">The string to transform.</param>
        /// <returns>The transformed string.</returns>
        public static string Escape(string str)
        {
            return str.Replace('\'', '"');
        }
        
        /*
        //Backup
        public void Backup()
        {
        }

        //Restore
        public void Restore()
        {
        }*/
    }
}