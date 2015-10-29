using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
        public class Attacker
        {
            private AttackerResult attackerStatus;
            
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
                    throw new AttackerTargetNotFoundException();
                }
                else if(attackerStatus == AttackerResult.FailedAuth)
                {
                    throw new AttackerAttemptsExhaustedException();
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
                    throw new AttackerTargetNotFoundException();
                }
            }
        }
    }
}
