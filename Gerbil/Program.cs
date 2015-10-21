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
            if (Directory.Exists("./store"))
            {
                Console.WriteLine("Found AI temp storage folder.");
            }
            else
            {
                Console.WriteLine("ERROR: AI temp storage folder not found, please reinstall Gerbil to fix.");
                //Application.Exit();
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
            Console.Write("\nEnter start port: ");
            int sPort = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nEnter end port: ");
            int ePort = Convert.ToInt32(Console.ReadLine());
            //TODO: Verify input string
            //int[] openPorts = Gerbil_Scanners.PortScanner.scan(target, sPort, ePort);
            //for (int i = 0; i < openPorts.Length; i++)
            //{
            //    Console.WriteLine(openPorts[i]);
            //}
            for(int i = sPort; i <= ePort; i++)
            {
                Console.Write(i + ": ");
                if(Gerbil_Scanners.PortScanner.isOpen(target, i))
                {
                    Console.WriteLine("open");
                }
                else
                {
                    Console.WriteLine("closed");
                }
            }
        }
    }
}
