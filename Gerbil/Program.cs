using System;
using System.IO;
using System.Net.Sockets;
using Gerbil.Gerbil_IO;

namespace Gerbil
{
    class Gerbil_Core
    {
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
                    string input = In.prompt<string>("Gerbil");
                    cliLaunch(input.Split());
                }
            }
        }
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
                default:
                    Out.writeln("ERROR: Command not found.");
                    break;
            }
        }
        private static void startAttack()
        {
            int mode = In.menu("Modes", "All", "IP", "IP + Port", "IP + Port Range", "Training");
            switch(mode)
            {
                case 0:
                    Pathfinder.begin();
                    break;
                case 1:
                    string ip = In.prompt<string>("Target IP address");
                    Pathfinder.begin(ip);
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
                    Pathfinder.begin(ip3, porta, portb);
                    break;
                case 4:
                    string ip4 = In.prompt<string>("Target IP address");
                    int porta2 = In.prompt<int>("Start port");
                    int portb2 = In.prompt<int>("End port");
                    Pathfinder.begin(ip4, porta2, portb2, true);
                    break;
                default:
                    Out.writeln("ERROR: Unrecognized option.");
                    break;
            }
        }
    }
}
