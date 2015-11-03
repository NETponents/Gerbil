﻿using System;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Gerbil;

namespace Gerbil
{
    namespace Gerbil_Scanners
    {
        class PortScanner
        {
            /// <summary>
            /// Scans a target on a specified port range for open TCP listeners.
            /// </summary>
            /// <param name="target">IP address or relative hostname to target.</param>
            /// <param name="startport">Port to start scanning on.</param>
            /// <param name="endport">Port to stop scanning on.</param>
            /// <returns>Array of open ports with active TCP listeners.</returns>
            public static int[] scan(string target, int startport, int endport, int timeout)
            {
                List<int> openports = new List<int>();
                for (int i = startport; i <= endport; i++)
                {
                    if(scan(target, i, timeout))
                    {
                        openports.Add(i);
                    }
                }
                return openports.ToArray();
            }
            /// <summary>
            /// Scans a target on a specific port for an open TCP listener.
            /// </summary>
            /// <param name="target">IP address or relative hostname to target.</param>
            /// <param name="port">Port to scan.</param>
            /// <returns>TCP listener found.</returns>
            public static bool scan(string target, int port, int timeout)
            {
                TcpClient TcpScan = new TcpClient();
                TcpScan.SendTimeout = timeout;
                try
                {
                    // Try to connect 
                    TcpScan.Connect(target, port);
                    // If there's no exception, we can say the port is open 
                    return true;
                }
                catch
                {
                    // An exception occured, thus the port is probably closed
                    return false;
                }
                finally
                {
                    TcpScan.Close();
                }
            }
        }
        class NetworkScanner
        {
            /// <summary>
            /// [Obsolete] Scans the network for online devices on the specified subnet.
            /// </summary>
            /// <param name="subnet">Subnet to scan. (Ex: 192.168.1.)</param>
            /// <returns>IP addresses of found devices.</returns>
            public static string[] getDevices(string subnet, int timeout)
            {
                List<string> devices = new List<string>();
                for(int i = 1; i < 255; i++)
                {
                    Ping pinger = new Ping();
                    PingReply reply = pinger.Send(subnet + i, timeout);
                    if(reply.Status != IPStatus.TimedOut)
                    {
                        devices.Add(subnet + i);
                        Gerbil_IO.Out.writeln("Found device: " + subnet + i);
                    }
                }
                return devices.ToArray();
            }
            /// <summary>
            /// Scans the network for online devices on the specified subnet.
            /// </summary>
            /// <param name="subnet">Subnet to scan. (Ex: 192.168.1.x)</param>
            /// <returns>IP addresses of found devices.</returns>
            public static string[] getDevices(string subnet, int timeout, int fieldMax)
            {
                List<string> devices = new List<string>();
                for (int i = 1; i < fieldMax; i++)
                {
                    Ping pinger = new Ping();
                    PingReply reply;
                    bool failed = false;
                    bool timedOut = false;
                    try
                    {
                        reply = pinger.Send(subnet.Replace("x",i.ToString()), timeout);
                        timedOut = (reply.Status == IPStatus.TimedOut);
                    }
                    catch
                    {
                        // Ping error, subnet does not exist.
                        failed = true;
                    }
                    if (!failed)
                    {
                        if (!timedOut)
                        {
                            devices.Add(subnet.Replace("x", i.ToString()));
                            Gerbil_IO.Out.writeln("Found device: " + subnet.Replace("x", i.ToString()));
                        }
                    }
                }
                return devices.ToArray();
            }
            private static bool isLocalAddress(string ipAddress)
            {
                //IP comparison
                bool result = false;
                //IP comparison
                String strHostName = string.Empty;
                // Getting Ip address of local machine...
                // First get the host name of local machine.
                strHostName = Dns.GetHostName();
                Console.WriteLine("Local Machine's Host Name: " + strHostName);
                // Then using host name, get the IP address list..
                IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
                IPAddress[] addr = ipEntry.AddressList;

                for (int i = 0; i < addr.Length; i++)
                {
                    if(addr[i].ToString() == ipAddress)
                    {
                        result = true;
                    }
                }
                return result;
            }
            private static bool isLocalAddress(IPAddress ipAddress)
            {
                bool result = false;
                //IP comparison
                String strHostName = string.Empty;
                // Getting Ip address of local machine...
                // First get the host name of local machine.
                strHostName = Dns.GetHostName();
                Console.WriteLine("Local Machine's Host Name: " + strHostName);
                // Then using host name, get the IP address list..
                IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
                IPAddress[] addr = ipEntry.AddressList;

                for (int i = 0; i < addr.Length; i++)
                {
                    if(addr[i] == ipAddress)
                    {
                        result = true;
                    }
                }
                return result;
            }
        }
    }
}
