using System;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

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
            /// Scans the network for online devices on the specified subnet.
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
                    }
                }
                return devices.ToArray();
            }
        }
    }
}
