using System;
using System.IO;
using System.IO.File;

namespace Gerbil
{
  class Gerbil_Core
  {
    static void Main() 
    {
      Console.WriteLine("Gerbil v0.0.1 Alpha");
      Console.WriteLine("Copyright 2015 under the GPL V3 License");
      Console.WriteLine("NETponents or its authors assume no responsibility for this program or its actions.");
      Console.WriteLine("Starting up...");
      if(Directory.Exists("./store");
      {
        Console.WriteLine("Found AI temp storage folder.");
      }
      else
      {
        Console.WriteLine("ERROR: AI temp storage folder not found, please reinstall Gerbil to fix.");
        Application.Exit();
      }
      //TODO: initialize settings file
      while(true)
      {
        Console.Write("Gerbil> ");
        string input = "";
        input = Console.readLine();
        switch(input.split(" ")[0])
        {
          case "exit":
            Application.Exit();
            break;
          case "config":
            //TODO: forward config command
            break;
          case "start":
            startAttack()
            break;
          default:
            Console.WriteLine("ERROR: Command not found.");
            break;
        }
      }
    }
    private static void startAttack()
    {
      Console.Write("Enter target IP address: ");
      string target = "";
      target = Console.readLine();
      //TODO: Convert target to IPAddress
    }
  }
}
