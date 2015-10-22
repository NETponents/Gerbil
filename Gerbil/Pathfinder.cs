using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gerbil.Gerbil_IO;

namespace Gerbil
{
    class Pathfinder
    {
        public static void begin()
        {
            // Scan for devices on network
            Out.writeln("Scanning for devices...");
            List<string> devices = new List<string>();
            /// 192.168.1.x
            Out.writeln("Searching 192.168.1.x subnet...");
            string[] sub1 = Gerbil_Scanners.NetworkScanner.getDevices("192.168.1.");
            foreach (string i in sub1)
            {
                Out.writeln("Found device:" + i);
                devices.Add(i);
            }
            /// 10.0.0.x
            Out.writeln("Searching 10.0.0.x subnet...");
            string[] sub2 = Gerbil_Scanners.NetworkScanner.getDevices("10.0.0.");
            foreach(string i in sub2)
            {
                Out.writeln("Found device:" + i);
                devices.Add(i);
            }
            // Loop system scan on all responding systems
            foreach(string address in devices)
            {
                // Scan device for open ports
                Out.writeln("Probing ports...");
                int[] openPorts = Gerbil_Scanners.PortScanner.scan(address, 0, 1000);
                if (openPorts.Length > 0)
                {
                    for (int i = 0; i < openPorts.Length; i++)
                    {
                        Out.writeln("Found port: " + openPorts[i]);
                    }
                }
                else
                {
                    Out.writeln("No open ports found for the specified host and port range.");
                    continue;
                }
                // Get list of services
                Out.writeln("Looking up port definitions...");
                string[] openServices = Gerbil_PortServices.PortLookup.getServices(openPorts);
                if (openServices.Length > 0)
                {
                    Out.writeln("Found service: ");
                    foreach (string i in openServices)
                    {
                        Out.writeln(i);
                    }
                }
                else
                {
                    Out.writeln("No known services found in AI store. Add them manually using 'portservice add serviceName portNumber'");
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
            Out.writeln("Probing ports...");
            int[] openPorts = Gerbil_Scanners.PortScanner.scan(ipAddress, 0, 1000);
            if (openPorts.Length > 0)
            {
                for (int i = 0; i < openPorts.Length; i++)
                {
                    Out.writeln("Found port: " + openPorts[i]);
                }
            }
            else
            {
                Out.writeln("No open ports found for the specified host and port range.");
                return;
            }
            // Get list of services
            Out.writeln("Looking up port definitions...");
            string[] openServices = Gerbil_PortServices.PortLookup.getServices(openPorts);
            if (openServices.Length > 0)
            {
                Out.writeln("Found service: ");
                foreach (string i in openServices)
                {
                    Out.writeln(i);
                }
            }
            else
            {
                Out.writeln("No known services found in AI store. Add them manually using 'portservice add serviceName portNumber'");
                return;
            }
            // Generate server information using AI engine
            // Finalize using SNMP
            // Launch attacks
        }
        public static void begin(string ipAddress, int port)
        {
            // Scan device for open ports
            Out.writeln("Probing port...");
            if (Gerbil_Scanners.PortScanner.scan(ipAddress, port))
            {
                Out.writeln("No open ports found for the specified host and port range.");
                return;
            }
            // Get list of services
            Out.writeln("Looking up port definitions...");
            int[] openPorts = { port };
            string[] openServices = Gerbil_PortServices.PortLookup.getServices(openPorts);
            if (openServices.Length > 0)
            {
                Out.writeln("Found service: ");
                foreach (string i in openServices)
                {
                    Out.writeln(i);
                }
            }
            else
            {
                Out.writeln("No known services found in AI store. Add them manually using 'portservice add serviceName portNumber'");
                return;
            }
            // Generate server information using AI engine
            // Finalize using SNMP
            // Launch attacks
        }
        public static void begin(string ipAddress, int sPort, int ePort)
        {
            // Scan device for open ports
            Out.writeln("Probing ports...");
            int[] openPorts = Gerbil_Scanners.PortScanner.scan(ipAddress, sPort, ePort);
            if (openPorts.Length > 0)
            {
                for (int i = 0; i < openPorts.Length; i++)
                {
                    Out.writeln("Found port: " + openPorts[i]);
                }
            }
            else
            {
                Out.writeln("No open ports found for the specified host and port range.");
                return;
            }
            // Get list of services
            Out.writeln("Looking up port definitions...");
            string[] openServices = Gerbil_PortServices.PortLookup.getServices(openPorts);
            if (openServices.Length > 0)
            {
                Out.writeln("Found service: ");
                foreach (string i in openServices)
                {
                    Out.writeln(i);
                }
            }
            else
            {
                Out.writeln("No known services found in AI store. Add them manually using 'portservice add serviceName portNumber'");
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
            Out.writeln("Probing ports...");
            int[] openPorts = Gerbil_Scanners.PortScanner.scan(ipAddress, sPort, ePort);
            if (openPorts.Length > 0)
            {
                for (int i = 0; i < openPorts.Length; i++)
                {
                    Out.writeln("Found port: " + openPorts[i]);
                }
            }
            else
            {
                Out.writeln("No open ports found for the specified host and port range.");
                return;
            }
            // Get list of services
            Out.writeln("Looking up port definitions...");
            string[] openServices = Gerbil_PortServices.PortLookup.getServices(openPorts);
            if (openServices.Length > 0)
            {
                Out.writeln("Found service: ");
                foreach (string i in openServices)
                {
                    Out.writeln(i);
                }
            }
            else
            {
                Out.writeln("No known services found in AI store. Add them manually using 'portservice add serviceName portNumber'");
                return;
            }
            // Generate server information using AI engine
            // Finalize using SNMP
            // Launch attacks
        }
    }
}
