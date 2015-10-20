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
    }
  }
}
