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
        class PortLookup
        {
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
            public static void createService(string serviceName, int portNumber)
            {
                Directory.CreateDirectory(Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), "Documents", "Gerbil", "memstore", "ports", "services", portNumber.ToString()));
                File.WriteAllText(Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), "Documents", "Gerbil", "memstore", "ports", "services", portNumber.ToString()) + @"\" + serviceName + ".gerbil", "version=0.1");
            }
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
                createService("NETBIOS", 137);
                createService("NETBIOS", 138);
                createService("NETBIOS", 139);
                createService("IMAP", 143);
                createService("SQL", 156);
                createService("SNMP", 161);
                createService("SNMPTRAP", 162);
            }
            public static void launch(string[] args)
            {
                if(args[1] == "add")
                {
                    createService(args[2], Convert.ToInt32(args[3]));
                }
                else if(args[1] == "remove")
                {
                    removeService(args[2], Convert.ToInt32(args[3]));
                }
            }
        }
    }
}
