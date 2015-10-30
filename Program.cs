using System;
using System.IO;
using System.Net.Sockets;
using Gerbil.Gerbil_IO;

namespace Gerbil
{
    class Gerbil_Core
    {
        /// <summary>
        /// Entry point of program. Handles initial initialization and CLI/arg mode redirects.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        static void Main(string[] args)
        {
            Out.writeln("Gerbil v0.0.1 Alpha");
            Out.writeln("Copyright 2015 under the GPL V3 License");
            Out.writeln("NETponents or its authors assume no responsibility for this program or its actions.");
            Out.writeln("Starting up...");
            if (Directory.Exists(Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), "Documents", "Gerbil")))
            {
                Out.writeln("Found AI temp storage folder.");
            }
            else
            {
                Directory.CreateDirectory(Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), "Documents", "Gerbil", "memstore", "ports", "services"));
                Gerbil_PortServices.PortLookup.initServices();
            }
            //TODO: initialize settings file
            if (args.Length > 0)
            {
                cliLaunch(args);
                Environment.Exit(0);
            }
            else
            {
                while (true)
                {
                    string input = In.prompt<string>("Gerbil", '>');
                    cliLaunch(input.Split());
                }
            }
        }
        /// <summary>
        /// Launches a Gerbil service.
        /// </summary>
        /// <param name="input">Launch arguments.</param>
        private static void cliLaunch(params string[] input)
        {
            switch (input[0])
            {
                case "exit":
                    Environment.Exit(0);
                    break;
                case "config":
                    //TODO: forward config command
                    break;
                case "portservice":
                    Gerbil_PortServices.PortLookup.launch(input);
                    break;
                case "start":
                    startAttack();
                    break;
                case "help":
                    Out.writeln("Common Gerbil CLI commands:");
                    Out.writeln("'start' - Switch program from CLI to attack mode.");
                    Out.writeln("'config' - NOT IMPLEMENTED.");
                    Out.writeln("'portservice add|remove SERVICE PORTNUMBER' - Adds or removes services to the port conversion dictionary.");
                    Out.writeln("'portservice restore' - Resets the port conversion dictionary to it's default listings.");
                    Out.writeln("'about' - Prints information about current instance of Gerbil.");
                    Out.writeln("'sattack ipaddress attacktype' - Attacks a device using a specific attack mode. (NOT RECOMMENDED)");
                    Out.writeln("'exit' - Closes the Gerbil CLI.");
                    break;
                case "about":
                    Out.writeln("Gerbil v0.0.1 alpha");
                    Out.writeln("Development release");
                    Out.writeln("Engine: Gerbil v0.1a");
                    Out.writeln("Version: GitHub DRv0.0.1a");
                    Out.writeln("Copyright 2015 NETponents under GPL V3 License.");
                    break;
                default:
                    Out.writeln("ERROR: Command not found.");
                    break;
            }
        }
        /// <summary>
        /// Launches wizard that retrieves attack options, then launches appropriate attack mode.
        /// </summary>
        private static void startAttack()
        {
            int mode = In.menu("Modes", "All", "IP", "IP + Port", "IP + Port Range", "Training");
            switch(mode)
            {
                case 0:
                    string subnet = In.prompt<string>("Target subnet (x|y|z Ex: 192.168.1.xxx 10.zz.yy.xxx)");
                    int timeout = In.prompt<int>("Max timeout for ping");
                    Pathfinder.begin_auto(subnet, timeout);
                    break;
                case 1:
                    string ip = In.prompt<string>("Target IP address");
                    int timeout2 = In.prompt<int>("Max timeout for ping");
                    Pathfinder.begin(ip, timeout2);
                    break;
                case 2:
                    string ip2 = In.prompt<string>("Target IP address");
                    int port = In.prompt<int>("Target port");
                    Pathfinder.begin(ip2, port);
                    break;
                case 3:
                    string ip3 = In.prompt<string>("Target IP address");
                    int porta = In.prompt<int>("Start port");
                    int portb = In.prompt<int>("End port");
                    int timeout3 = In.prompt<int>("Max timeout for ping");
                    Pathfinder.begin(ip3, porta, portb, timeout3);
                    break;
                case 4:
                    string ip4 = In.prompt<string>("Target IP address");
                    int porta2 = In.prompt<int>("Start port");
                    int portb2 = In.prompt<int>("End port");
                    int timeout4 = In.prompt<int>("Max timeout for ping");
                    Pathfinder.begin(ip4, porta2, portb2, timeout4, true);
                    break;
                case 5:
                    Out.writeln("Attack canceled.");
                    break;
                default:
                    Out.writeln("ERROR: Unrecognized option.");
                    break;
            }
        }
    }
}
