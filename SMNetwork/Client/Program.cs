using System;
using Newtonsoft.Json;

namespace SMNetwork.Client
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Client SMClient = new Client();
            Console.WriteLine();
            //Console.WriteLine(SMClient.Create("test43", "test42", "test42", "test42@gmail.com", "azerty", "Je suis un test"));
            Console.WriteLine(SMClient.Connect("test42@gmail.com", "azerty"));
            Console.WriteLine("Token: \"" + DataClient.Token + "\"");
            Console.WriteLine(JsonConvert.SerializeObject(SMClient.AskProgress()));
            /*
            Console.WriteLine(SMClient.AskProfil("cedric.farinazzo@epita.fr"));
            Console.WriteLine(SMClient.AskProfil("antoine.claudel@hotmail.fr"));
            Console.WriteLine(SMClient.AskProfil("edgar.grizzi@epita.fr"));
            Console.WriteLine(SMClient.AskProfil("test@gmail.com"));*/
            Console.WriteLine(SMClient.AskMyProfil());
            Console.WriteLine(JsonConvert.SerializeObject(SMClient.AskMapList()));
            Console.WriteLine(JsonConvert.SerializeObject(SMClient.AskMapId(0)));
            Console.WriteLine(SMClient.SendMap("firsttest", "tftyfytfytftytf"));
            Console.WriteLine("Logout: " + SMClient.Logout());
            
        }
    }
}