using System;

namespace SMNetwork.Client
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Client SMClient = new Client();
            Console.WriteLine("Connected: " + SMClient.Connect("cedric.farinazzo@epita.fr", "1234AZER"));
            Console.WriteLine("Token: \"" + DataClient.Token + "\"");
            Console.WriteLine("Logout: " + SMClient.Logout());
        }
    }
}