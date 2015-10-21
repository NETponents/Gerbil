using System;
using System.IO;
using System.Net.Sockets;

namespace Gerbil
{
    class Gerbil_Core
    {
        static void Main()
        {
            Console.WriteLine("Gerbil v0.0.1 Alpha");
            Console.WriteLine("Copyright 2015 under the GPL V3 License");
            Console.WriteLine("NETponents or its authors assume no responsibility for this program or its actions.");
            Console.WriteLine("Starting up...");
            if (Directory.Exists(@"C:\\Gerbil"))
            {
                Console.WriteLine("Found AI temp storage folder.");
            }
            else
            {
                Directory.CreateDirectory(@"C:\\Gerbil\AI\memstore\ports\services");
            }
            //TODO: initialize settings file
            while (true)
            {
                Console.Write("Gerbil> ");
                string input = "";
                input = Console.ReadLine();
                switch (input.Split(' ')[0])
                {
                    case "exit":
                        Environment.Exit(0);
                        break;
                    case "config":
                        //TODO: forward config command
                        break;
                    case "start":
                        startAttack();
                        break;
                    default:
                        Console.WriteLine("ERROR: Command not found.");
                        break;
                }
            }
        }
        private static void startAttack()
        {
            Console.Write("Enter target IP address: ");
            string target = Console.ReadLine();
            Console.Write("Enter start port: ");
            int sPort = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter end port: ");
            int ePort = Convert.ToInt32(Console.ReadLine());
            //TODO: Verify input string
            Console.WriteLine("Probing ports...");
            int[] openPorts = Gerbil_Scanners.PortScanner.scan(target, sPort, ePort);
            if (openPorts.Length > 0)
            {
                Console.WriteLine("Open ports:");
                for (int i = 0; i < openPorts.Length; i++)
                {
                    Console.WriteLine(openPorts[i]);
                }
            }
            else
            {
                Console.WriteLine("No open ports found for the specified host and port range.");
            }
            Console.WriteLine("Looking up port definitions...");
            string[] openServices = Gerbil_PortServices.PortLookup.getServices(openPorts);
            if (openServices.Length > 0)
            {
                Console.WriteLine("Known services:");
                foreach (string i in openServices)
                {
                    Console.WriteLine(i);
                }
            }
            else
            {
                Console.WriteLine("No known services found in AI store. Enter training mode or add them manually through the CLI.");
            }
        }
    }
}
