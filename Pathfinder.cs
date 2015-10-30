﻿using System;
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
        private static int maxAddressField(char placeholder, string subnet)
        {
            int counter = charCount(placeholder, subnet);
            if(counter > 3)
            {
                throw new Exception();
            }
            else if(counter == 3)
            {
                return 255;
            }
            else if(counter == 2)
            {
                return 99;
            }
            else if(counter == 1)
            {
                return 9;
            }
            else
            {
                throw new Exception();
            }
        }
        private static int charCount(char character, string scanner)
        {
            int counter = 0;
            for (int i = 0; i < scanner.Length; i++)
            {
                if (scanner.ToCharArray()[i] == character)
                {
                    counter++;
                }
            }
            return counter;
        }
        private static string replaceFirst(char indicator, string scanner)
        {
            int index = scanner.IndexOf(indicator);
            int end = scanner.LastIndexOf(indicator);
            string variableField = scanner.Substring(index, end - index + 1);
            scanner = scanner.Replace(variableField, indicator.ToString());
            return scanner;
        }
        /// <summary>
        /// Launches an attack on the entire network.
        /// </summary>
        public static void begin_auto(string subnet, int timeout)
        {
            // Scan for devices on network
            Out.writeln("Scanning for devices...");
            List<string> devices = new List<string>();
            if(subnet.Contains("z"))
            {
                int zCount = maxAddressField('z', subnet);
                int yCount = maxAddressField('y', subnet);
                int xCount = maxAddressField('x', subnet);
                subnet = replaceFirst('x', replaceFirst('y', replaceFirst('z', subnet)));

                for(int z = 0; z <= zCount; z++)
                {
                    string zMod = subnet.Replace("z", z.ToString());
                    for(int y = 0; y <= yCount; y++)
                    {
                        string yMod = zMod.Replace("y", y.ToString());
                        Out.writeln("Searching " + yMod + " subnet...");
                        foreach (string i in Gerbil_Scanners.NetworkScanner.getDevices(yMod, timeout, xCount))
                        {
                            devices.Add(i);
                        }
                    }
                }
            }
            else if(subnet.Contains("y"))
            {
                int yCount = maxAddressField('y', subnet);
                int xCount = maxAddressField('x', subnet);
                subnet = replaceFirst('x', replaceFirst('y', subnet));

                for (int y = 0; y <= yCount; y++)
                {
                    string yMod = subnet.Replace("y", y.ToString());
                    Out.writeln("Searching " + yMod + " subnet...");
                    foreach (string i in Gerbil_Scanners.NetworkScanner.getDevices(yMod, timeout, xCount))
                    {
                        devices.Add(i);
                    }
                }
            }
            else
            {
                Out.writeln("Searching " + subnet + " subnet...");
                int xCount = maxAddressField('x', subnet);
                subnet = replaceFirst('x', subnet);

                foreach (string i in Gerbil_Scanners.NetworkScanner.getDevices(subnet, timeout, xCount))
                {
                    devices.Add(i);
                }
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
                Gerbil_Engine.NetworkResult osr = Gerbil_Engine.GerbilRunner.guessOS(openServices, true);
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
            Gerbil_Engine.NetworkResult osr = Gerbil_Engine.GerbilRunner.guessOS(openServices, true);
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
