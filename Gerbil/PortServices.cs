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
                    if(Directory.Exists(@"C:\\Gerbil\AI\memstore\ports\services\" + ports[i]))
                    {
                        //Records were found, retrieve the service names
                        string[] temp = Directory.GetDirectories(@"C:\\Gerbil\AI\memstore\ports\services\" + ports[i]);
                        foreach(string j in temp)
                        {
                            services.Add(j.Replace(".gerbil", ""));
                        }
                    }
                }
                return services.ToArray();
            }
        }
    }
}
