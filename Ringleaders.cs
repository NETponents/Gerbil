using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Gerbil
{
    namespace Ringleaders
    {
        /// <summary>
        /// Template class for stab attack managers
        /// </summary>
        public partial class Ringleader
        {
            protected IPAddress target;
            protected int devicePort;
            
            /// <summary>
            /// Constructor for Ringleader class
            /// </summary>
            /// <param name="attackPoint">IP address of target</param>
            public Ringleader(IPAddress attackPoint)
            {
                target = attackPoint;
            }
            /// <summary>
            /// Constructor for Ringleader class
            /// </summary>
            /// <param name="attackPoint">IP address of target</param>
            public Ringleader(string attackPoint)
            {
                target = IPAddress.Parse(attackPoint);
            }
            /// <summary>
            /// Constructor for Ringleader class
            /// </summary>
            /// <param name="attackPoint">IP address of target</param>
            /// <param name="port">Port to attack</param>
            public Ringleader(IPAddress attackPoint, int port)
            {
                target = attackPoint;
                devicePort = port;
            }
            /// <summary>
            /// Constructor for Ringleader class
            /// </summary>
            /// <param name="attackPoint">IP address of target</param>
            /// <param name="port">Port to attack</param>
            public Ringleader(string attackPoint, int port)
            {
                target = IPAddress.Parse(attackPoint);
                devicePort = port;
            }
            /// <summary>
            /// Launches a stab attack
            /// </summary>
            /// <param name="args">Parameters to pass to the ringleader</param>
            /// <returns>Status value</returns>
            public virtual int Attack(params string[] args)
            {
                return 0;
            }
        }
        namespace Routers
        {
            /// <summary>
            /// Stab attack manager for attacking consumer-grade routers
            /// </summary>
            public class DumbRouterRingleader : Ringleader
            {
                /// <summary>
                /// Public constructor for DumbRouterRingleader class
                /// </summary>
                /// <param name="target">IP address of router to attack</param>
                public DumbRouterRingleader(string target)
                    : base(target)
                {
                    // TODO: probe router to confirm that it exists
                }
                /// <summary>
                /// Public constructor for DumbRouterRingleader class
                /// </summary>
                /// <param name="target">IP address of router to attack</param>
                public DumbRouterRingleader(IPAddress target)
                    : base(target)
                {
                    // TODO: probe router to confirm that it exists
                }
                public override int Attack(params string[] args)
                {
                    // TODO: comb HTTP response
                    // TODO: Initialize password cracker
                    return base.Attack(args);
                }
            }
        }
    }
}
