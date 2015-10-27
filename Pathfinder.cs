using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gerbil.Gerbil_IO;

namespace Gerbil
{
    class Pathfinder
    {
        /// <summary>
        /// Launches an attack on the entire network.
        /// </summary>
        public static void begin_auto(string subnet, int timeout)
        {
            // Scan for devices on network
            Out.writeln("Scanning for devices...");
            List<string> devices = new List<string>();
            Out.writeln("Searching " + subnet + "x subnet...");
            string[] sub1 = Gerbil_Scanners.NetworkScanner.getDevices(subnet, timeout);
            foreach (string i in sub1)
            {
                devices.Add(i);
            }
            // Loop system scan on all responding systems
            foreach(string address in devices)
            {
                Out.blank();
                // Scan device for open ports
                Out.writeln("Probing known ports on " + address + "...");
                int[] knownPorts = Gerbil_PortServices.PortLookup.getPorts();
                List<int> tempFoundPorts = new List<int>();
                foreach(int i in knownPorts)
                {
                    if (Gerbil_Scanners.PortScanner.scan(address, i, timeout))
                    {
                        tempFoundPorts.Add(i);
                        Out.writeln(i + ": OPEN");
                    }
                    else
                    {
                        Out.writeln(i + ": CLOSED");
                    }
                }
                int[] openPorts = tempFoundPorts.ToArray();
                if(openPorts.Length == 0)
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
                if(openServices.Contains("NETBIOS"))
                {
                    string devName = "";
                    devName = Dns.GetHostEntry(address).HostName;
                    Out.writeln("NETBIOS Name: " + devName);
                }
                // Forward found services to the AI engine and get server OS
                //TODO: forward training mode parameter
                Gerbil_Engine.OSResult osr = Gerbil_Engine.GerbilRunner.guessOS(openServices, true);
                float ct = osr.getCertainty();
                ct = ct * 1000.0f;
                Out.writeln("OS Guess: " + osr.getName());
                Out.writeln(String.Format("Certainty: {0:F2}%", osr.getCertainty()));
                // Guess more data based on running services
                // HTTP
                if(openServices.Contains("HTTP"))
                {

                }
                // Launch attacks
            }
        }
        /// <summary>
        /// Launches an attack on a specific IP address.
        /// </summary>
        /// <param name="ipAddress">IP address or relative hostname to target.</param>
        public static void begin(string ipAddress, int timeout)
        {
            // Scan device for open ports
            Out.writeln("Probing ports...");
            int[] openPorts = Gerbil_Scanners.PortScanner.scan(ipAddress, 0, 1000, timeout);
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
            if (openServices.Contains("NETBIOS"))
            {
                string devName = "";
                devName = Dns.GetHostEntry(ipAddress).HostName;
                Out.writeln("NETBIOS Name: " + devName);
            }
            // Forward found services to the AI engine and get server OS
            //TODO: forward training parameter
            Gerbil_Engine.OSResult osr = Gerbil_Engine.GerbilRunner.guessOS(openServices, true);
            float ct = osr.getCertainty();
            ct = ct * 10.0f;
            Out.writeln("OS Guess: " + osr.getName());
            Out.writeln(String.Format("Certainty: {0:F2}%", osr.getCertainty()));
            // Guess more data based on running services
            // HTTP
            if (openServices.Contains("HTTP"))
            {

            }
            // Launch attacks
        }
        /// <summary>
        /// Launches an attack on a specific machine and port.
        /// </summary>
        /// <param name="ipAddress">IP address or relative hostname of target.</param>
        /// <param name="port">Port to scan for vulnerable services.</param>
        public static void begin(string ipAddress, int port, int timeout)
        {
            // Scan device for open ports
            Out.writeln("Probing port...");
            if (Gerbil_Scanners.PortScanner.scan(ipAddress, port, timeout))
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
        /// <summary>
        /// Launches an attack on a specific machine and port range.
        /// </summary>
        /// <param name="ipAddress">IP address or relative hostname to target.</param>
        /// <param name="sPort">Port to start scanning on.</param>
        /// <param name="ePort">Port to stop scanning on.</param>
        public static void begin(string ipAddress, int sPort, int ePort, int timeout)
        {
            // Scan device for open ports
            Out.writeln("Probing ports...");
            int[] openPorts = Gerbil_Scanners.PortScanner.scan(ipAddress, sPort, ePort, timeout);
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
        /// <summary>
        /// Launches an attack on a specific machine and port range in training mode.
        /// </summary>
        /// <param name="ipAddress">IP address or local hostname to target.</param>
        /// <param name="sPort">Port to start scanning on.</param>
        /// <param name="ePort">Port to stop scanning on.</param>
        /// <param name="training">Training mode.</param>
        public static void begin(string ipAddress, int sPort, int ePort, int timeout, bool training)
        {
            //TODO: add training mode method calls
            
            // Scan device for open ports
            Out.writeln("Probing ports...");
            int[] openPorts = Gerbil_Scanners.PortScanner.scan(ipAddress, sPort, ePort, timeout);
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
