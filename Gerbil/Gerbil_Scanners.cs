using System;
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
            public static int[] scan(string target, int startport, int endport)
            {
                List<int> openports = new List<int>();
                for (int i = startport; i <= endport; i++)
                {
                    if(isOpen(target, i))
                    {
                        openports.Add(i);
                    }
                }
                return openports.ToArray();
            }
            public static bool isOpen(string target, int port)
            {
                TcpClient TcpScan = new TcpClient();
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
    }
}
