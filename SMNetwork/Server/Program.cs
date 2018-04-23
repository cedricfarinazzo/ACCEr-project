using System;

namespace SMNetwork.Server
{
    internal class Program
    {
        private static int Port { get; set; }
        private static string UidDatabase { get; set; }
        private static string PassDatabase { get; set; }

        public static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("  ---------------------------");
            Console.WriteLine();
            Console.Write("      ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Shadow Miner Server");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("  ---------------------------");
            Console.WriteLine();
            Port = 4247;
            UidDatabase = "php-accer";
            PassDatabase = "Nn6=4aev5";
            Server SMserver = new Server(Port, UidDatabase, PassDatabase);
            SMserver.Start();
        }
    }
}