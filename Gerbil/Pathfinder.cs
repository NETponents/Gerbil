using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerbil
{
    class Pathfinder
    {
        public static void begin()
        {
            // Scan for devices on network
            Console.WriteLine("Scanning for devices...");
            List<string> devices = new List<string>();
            /// 192.168.1.x
            Console.WriteLine("Searching 192.168.1.x subnet...");
            string[] sub1 = Gerbil_Scanners.NetworkScanner.getDevices("192.168.1.");
            foreach (string i in sub1)
            {
                Console.WriteLine("Found device:" + i);
                devices.Add(i);
            }
            /// 10.0.0.x
            Console.WriteLine("Searching 10.0.0.x subnet...");
            string[] sub2 = Gerbil_Scanners.NetworkScanner.getDevices("10.0.0.");
            foreach(string i in sub2)
            {
                Console.WriteLine("Found device:" + i);
                devices.Add(i);
            }
            // Loop system scan on all responding systems
            foreach(string address in devices)
            {
                // Scan device for open ports
                Console.WriteLine("Probing ports...");
                int[] openPorts = Gerbil_Scanners.PortScanner.scan(address, 0, 1000);
                if (openPorts.Length > 0)
                {
                    for (int i = 0; i < openPorts.Length; i++)
                    {
                        Console.WriteLine("Found port: " + openPorts[i]);
                    }
                }
                else
                {
                    Console.WriteLine("No open ports found for the specified host and port range.");
                    continue;
                }
                // Get list of services
                Console.WriteLine("Looking up port definitions...");
                string[] openServices = Gerbil_PortServices.PortLookup.getServices(openPorts);
                if (openServices.Length > 0)
                {
                    Console.WriteLine("Found service: ");
                    foreach (string i in openServices)
                    {
                        Console.WriteLine(i);
                    }
                }
                else
                {
                    Console.WriteLine("No known services found in AI store. Add them manually using 'portservice add serviceName portNumber'");
                    continue;
                }
                // Generate server information using AI engine
                // Finalize using SNMP
                // Launch attacks
            }
        }
        public static void begin(string ipAddress)
        {
            // Scan device for open ports
            Console.WriteLine("Probing ports...");
            int[] openPorts = Gerbil_Scanners.PortScanner.scan(ipAddress, 0, 1000);
            if (openPorts.Length > 0)
            {
                for (int i = 0; i < openPorts.Length; i++)
                {
                    Console.WriteLine("Found port: " + openPorts[i]);
                }
            }
            else
            {
                Console.WriteLine("No open ports found for the specified host and port range.");
                return;
            }
            // Get list of services
            Console.WriteLine("Looking up port definitions...");
            string[] openServices = Gerbil_PortServices.PortLookup.getServices(openPorts);
            if (openServices.Length > 0)
            {
                Console.WriteLine("Found service: ");
                foreach (string i in openServices)
                {
                    Console.WriteLine(i);
                }
            }
            else
            {
                Console.WriteLine("No known services found in AI store. Add them manually using 'portservice add serviceName portNumber'");
                return;
            }
            // Generate server information using AI engine
            // Finalize using SNMP
            // Launch attacks
        }
        public static void begin(string ipAddress, int port)
        {
            // Scan device for open ports
            Console.WriteLine("Probing port...");
            if (Gerbil_Scanners.PortScanner.scan(ipAddress, port))
            {
                Console.WriteLine("No open ports found for the specified host and port range.");
                return;
            }
            // Get list of services
            Console.WriteLine("Looking up port definitions...");
            int[] openPorts = { port };
            string[] openServices = Gerbil_PortServices.PortLookup.getServices(openPorts);
            if (openServices.Length > 0)
            {
                Console.WriteLine("Found service: ");
                foreach (string i in openServices)
                {
                    Console.WriteLine(i);
                }
            }
            else
            {
                Console.WriteLine("No known services found in AI store. Add them manually using 'portservice add serviceName portNumber'");
                return;
            }
            // Generate server information using AI engine
            // Finalize using SNMP
            // Launch attacks
        }
        public static void begin(string ipAddress, int sPort, int ePort)
        {
            // Scan device for open ports
            Console.WriteLine("Probing ports...");
            int[] openPorts = Gerbil_Scanners.PortScanner.scan(ipAddress, sPort, ePort);
            if (openPorts.Length > 0)
            {
                for (int i = 0; i < openPorts.Length; i++)
                {
                    Console.WriteLine("Found port: " + openPorts[i]);
                }
            }
            else
            {
                Console.WriteLine("No open ports found for the specified host and port range.");
                return;
            }
            // Get list of services
            Console.WriteLine("Looking up port definitions...");
            string[] openServices = Gerbil_PortServices.PortLookup.getServices(openPorts);
            if (openServices.Length > 0)
            {
                Console.WriteLine("Found service: ");
                foreach (string i in openServices)
                {
                    Console.WriteLine(i);
                }
            }
            else
            {
                Console.WriteLine("No known services found in AI store. Add them manually using 'portservice add serviceName portNumber'");
                return;
            }
            // Generate server information using AI engine
            // Finalize using SNMP
            // Launch attacks
        }
        public static void begin(string ipAddress, int sPort, int ePort, bool training)
        {
            //TODO: add training mode method calls
            
            // Scan device for open ports
            Console.WriteLine("Probing ports...");
            int[] openPorts = Gerbil_Scanners.PortScanner.scan(ipAddress, sPort, ePort);
            if (openPorts.Length > 0)
            {
                for (int i = 0; i < openPorts.Length; i++)
                {
                    Console.WriteLine("Found port: " + openPorts[i]);
                }
            }
            else
            {
                Console.WriteLine("No open ports found for the specified host and port range.");
                return;
            }
            // Get list of services
            Console.WriteLine("Looking up port definitions...");
            string[] openServices = Gerbil_PortServices.PortLookup.getServices(openPorts);
            if (openServices.Length > 0)
            {
                Console.WriteLine("Found service: ");
                foreach (string i in openServices)
                {
                    Console.WriteLine(i);
                }
            }
            else
            {
                Console.WriteLine("No known services found in AI store. Add them manually using 'portservice add serviceName portNumber'");
                return;
            }
            // Generate server information using AI engine
            // Finalize using SNMP
            // Launch attacks
        }
    }
}
