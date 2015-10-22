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
            if (Directory.Exists(Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), "Documents", "Gerbil")))
            {
                Console.WriteLine("Found AI temp storage folder.");
            }
            else
            {
                Directory.CreateDirectory(Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), "Documents", "Gerbil", "memstore", "ports", "services"));
                Gerbil_PortServices.PortLookup.initServices();
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
                    case "portservice":
                        Gerbil_PortServices.PortLookup.launch(input.Split(' '));
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
            Console.WriteLine("-------------------");
            Console.WriteLine("|      Modes      |");
            Console.WriteLine("| 0 - All         |");
            Console.WriteLine("| 1 - IP          |");
            Console.WriteLine("| 2 - IP + Port   |");
            Console.WriteLine("| 3 - IP + Ports  |");
            Console.WriteLine("| 4 - Training    |");
            Console.WriteLine("-------------------");
            Console.Write("Mode: ");
            int mode = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            switch(mode)
            {
                case 0:
                    Pathfinder.begin();
                    break;
                case 1:
                    Console.Write("Target IP address: ");
                    string ip = Console.ReadLine();
                    Console.WriteLine();
                    Pathfinder.begin(ip);
                    break;
                case 2:
                    Console.Write("Target IP address: ");
                    string ip2 = Console.ReadLine();
                    Console.WriteLine();
                    Console.Write("Port: ");
                    int port = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                    Pathfinder.begin(ip2, port);
                    break;
                case 3:
                    Console.Write("Target IP address: ");
                    string ip3 = Console.ReadLine();
                    Console.WriteLine();
                    Console.Write("Start port: ");
                    int porta = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                    Console.Write("End port: ");
                    int portb = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                    Pathfinder.begin(ip3, porta, portb);
                    break;
                case 4:
                    Console.Write("Target IP address: ");
                    string ip4 = Console.ReadLine();
                    Console.WriteLine();
                    Console.Write("Start port: ");
                    int porta2 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                    Console.Write("End port: ");
                    int portb2 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                    Pathfinder.begin(ip4, porta2, portb2, true);
                    break;
                default:
                    Console.WriteLine("ERROR: Unrecognized option.");
                    break;
            }
        }
    }
}
