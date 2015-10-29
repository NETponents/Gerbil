using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Globalization;
using System.Net;

namespace Gerbil
{
    namespace Attackers
    {
        public enum AttackerResult
        {
            Created,
            Initialized,
            FailedAuth,
            FailedConnection,
            Trying,
            Connected,
            Penetrated
        };
        public class AttackerNotInitializedException : Exception
        {
            
        }
        public class AttackerNoTargetFoundException : Exception
        {
            
        }
        public class AttackerAttemptsExhaustedException : Exception
        {
            
        }
        public class AttackerAlreadyPenetratedException : Exception
        {
            
        }
        public partial class Attacker
        {
            protected AttackerResult attackerStatus;
            
            /// <summary>
            /// Constructor for Attacker class
            /// </summary>
            public Attacker()
            {
                attackerStatus = AttackerResult.Created;
            }
            /// <summary>
            /// Performs initializing commands for attacker
            /// </summary>
            public virtual void init()
            {
                attackerStatus = AttackerResult.Initialized;
            }
            /// <summary>
            /// Attacks the given client, will only attempt once
            /// </summary>
            public virtual void stab()
            {
                if(attackerStatus == AttackerResult.Created)
                {
                    throw new AttackerNotInitializedException();
                }
                else if(attackerStatus ==  AttackerResult.FailedConnection)
                {
                    throw new AttackerNoTargetFoundException();
                }
                else if(attackerStatus == AttackerResult.FailedAuth)
                {
                    throw new AttackerAttemptsExhaustedException();
                }
                else if(attackerStatus == AttackerResult.Penetrated)
                {
                    throw new AttackerAlreadyPenetratedException();
                }
            }
            /// <summary>
            /// Deletes all evidence and closes connection to target
            /// </summary>
            public virtual void clean()
            {
                if(attackerStatus == AttackerResult.Created)
                {
                    throw new AttackerNotInitializedException();
                }
                else if(attackerStatus ==  AttackerResult.FailedConnection)
                {
                    throw new AttackerNoTargetFoundException();
                }
            }
        }
        public class WoLAttacker : Attacker
        {
            private string MACaddress;

            public WoLAttacker(string MacAddr)
                : base()
            {
                MACaddress = MacAddr;
            }
            public override AttackerResult stab()
            {
                //Send network adapter MAC address over UDP 16 times
                // Prep input parameters
                if(MACaddress.Contains(':'))
                {
                    MACaddress = MACaddress.Replace(":","");
                }
                ///////////////////////////////////////////////////////////////////////
                // Segments of code were copied from:                                //
                // http://www.codeproject.com/Articles/5315/Wake-On-Lan-sample-for-C //
                ///////////////////////////////////////////////////////////////////////
                WOLClass client = new WOLClass();
                client.Connect(new
                    IPAddress(0xffffffff),  //255.255.255.255  i.e broadcast
                    0x2fff); // port=12287 let's use this one 
                client.SetClientToBrodcastMode();
                //set sending bites
                int counter = 0;
                //buffer to be send
                byte[] bytes = new byte[1024];   // more than enough :-)
                //first 6 bytes should be 0xFF
                for (int y = 0; y < 6; y++)
                    bytes[counter++] = 0xFF;
                //now repeate MAC 16 times
                for (int y = 0; y < 16; y++)
                {
                    int i = 0;
                    for (int z = 0; z < 6; z++)
                    {
                        bytes[counter++] =
                            byte.Parse(MACaddress.Substring(i, 2),
                                NumberStyles.HexNumber);
                        i += 2;
                    }
                }
                //now send wake up packet
                int reterned_value = client.Send(bytes, 1024);
                return attackerStatus;
            }
            //we derive our class from a standard one
            private class WOLClass : UdpClient    
            {
                public WOLClass():base()
                { }
                //this is needed to send broadcast packet
                public void SetClientToBrodcastMode()    
                {
                    if(this.Active)
                        this.Client.SetSocketOption(SocketOptionLevel.Socket,
                            SocketOptionName.Broadcast,0);
                }
            }
            //now use this class
            //MAC_ADDRESS should  look like '013FA049'
        }
    }
}
