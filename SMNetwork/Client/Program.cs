using System;

namespace SMNetwork.Client
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Client SMClient = new Client();
            Console.WriteLine(SMClient.Connect("cedric.farinazzo@epita.fr", "1234AZER"));
        }
    }
}