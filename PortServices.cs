using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Gerbil
{
    namespace Gerbil_PortServices
    {
        public class PortLookup
        {
            /// <summary>
            /// Gets services found on specified active ports.
            /// </summary>
            /// <param name="ports">Ports to look up.</param>
            /// <returns>List of known services.</returns>
            public static string[] getServices(int[] ports)
            {
                List<string> services = new List<string>();
                for (int i = 0; i < ports.Length; i++ )
                {
                    // Look up available services on port
                    if (Directory.Exists(Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), "Documents", "Gerbil", "memstore", "ports", "services") + @"\" + ports[i]))
                    {
                        //Records were found, retrieve the service names
                        string[] temp = Directory.GetFiles(Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), "Documents", "Gerbil", "memstore", "ports", "services") + @"\" + ports[i]);
                        foreach(string j in temp)
                        {
                            services.Add(Path.GetFileNameWithoutExtension(j));
                        }
                    }
                }
                return services.ToArray();
            }
            public static int[] getPorts()
            {
               /**DEBUGBLOCK
                List<string> ports = new List<string>(Directory.EnumerateDirectories(Environment.ExpandEnvironmentVariables("%userprofile%") + @"\Documents\Gerbil\memstore\ports\services"));
                */
                List<int> foundPorts = new List<int>();
                /** DEBUGBLOCK (CONTINUED)
                foreach(string i in ports)
                {
                    foundPorts.Add(Convert.ToInt32(Path.GetDirectoryName(i)));
                    
                }*/
                ////////////////
                // DEBUG STUB //
                foundPorts.Add(7);
                foundPorts.Add(9);
                foundPorts.Add(17);
                foundPorts.Add(20);
                foundPorts.Add(21);
                foundPorts.Add(22);
                foundPorts.Add(23);
                foundPorts.Add(25);
                foundPorts.Add(42);
                foundPorts.Add(43);
                foundPorts.Add(53);
                foundPorts.Add(67);
                foundPorts.Add(68);
                foundPorts.Add(69);
                foundPorts.Add(80);
                foundPorts.Add(81);
                foundPorts.Add(110);
                foundPorts.Add(113);
                foundPorts.Add(123);
                foundPorts.Add(137);
                foundPorts.Add(138);
                foundPorts.Add(139);
                foundPorts.Add(143);
                foundPorts.Add(156);
                foundPorts.Add(161);
                foundPorts.Add(162);
                ////////////////
                return foundPorts.ToArray();
            }
            /// <summary>
            /// Adds a service to the known services dictionary.
            /// </summary>
            /// <param name="serviceName">Name of service.</param>
            /// <param name="portNumber">Port used by service.</param>
            public static void createService(string serviceName, int portNumber)
            {
                Directory.CreateDirectory(Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), "Documents", "Gerbil", "memstore", "ports", "services", portNumber.ToString()));
                File.WriteAllText(Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), "Documents", "Gerbil", "memstore", "ports", "services", portNumber.ToString()) + @"\" + serviceName + ".gerbil", "version=0.1");
            }
            /// <summary>
            /// Removes a service from the known service dictionary.
            /// </summary>
            /// <param name="serviceName">Name of service.</param>
            /// <param name="portNumber">Port used by service.</param>
            public static void removeService(string serviceName, int portNumber)
            {
                try
                {
                    File.Delete(Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), "Documents", "Gerbil", "memstore", "ports", "services", portNumber.ToString()) + @"\" + serviceName + ".gerbil");
                }
                catch
                {
                    //File could not be deleted, ignore for now.
                }
            }
            /// <summary>
            /// Populates the known services dictionary with a list of IANA-registered network services.
            /// </summary>
            public static void initServices()
            {
                createService("Echo", 7);
                createService("WOL", 9);
                createService("QuoteOfTheDay", 17);
                createService("FTP", 20);
                createService("FTP", 21);
                createService("SSH", 22);
                createService("Telnet", 23);
                createService("SMTP", 25);
                createService("WINS", 42);
                createService("WHOIS", 43);
                createService("DNS", 53);
                createService("BOOTP", 67);
                createService("BOOTP", 68);
                createService("TFTP", 69);
                createService("HTTP", 80);
                createService("HTTPS", 81);
                createService("POP3", 110);
                createService("IDENT", 113);
                createService("NTP", 123);
                createService("RPC", 135);
                createService("NETBIOS", 137);
                createService("NETBIOS", 138);
                createService("NetLogon", 138);
                createService("NETBIOS", 139);
                createService("IMAP", 143);
                createService("SQL", 156);
                createService("SNMP", 161);
                createService("SNMPTRAP", 162);
                createService("LDAP", 389);
                createService("SMB", 445);
                createService("CIFS", 445);
                createService("NetLogonR", 445);
                createService("SamR", 445);
                createService("SvrSvc", 445);
                createService("LPD", 515);
                createService("IPP", 631);
                createService("iSCSI", 3260);
            }
            /// <summary>
            /// Gerbil service launcher handler method.
            /// </summary>
            /// <param name="args">Launch arguments.</param>
            public static void launch(params string[] args)
            {
                if(args[1] == "add")
                {
                    createService(args[2], Convert.ToInt32(args[3]));
                }
                else if(args[1] == "remove")
                {
                    removeService(args[2], Convert.ToInt32(args[3]));
                }
                else if(args[1] == "restore")
                {
                    initServices();
                }
            }
        }
    }
}
