using System;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerbil
{
    namespace Gerbil_IO
    {
        class Out
        {
            public static void writeln(string input)
            {
              Console.WriteLine(input);
            }
            public static void write(string input)
            {
              Console.Write(input);
            }
            public static void blank()
            {
              Console.Write("\n");
            }
            public static void blank(int iterations)
            {
              for(int i = 0; i < iterations; i++)
              {
                blank();
              }
            }
            private static void printMenu(string title, params string[] options)
            {
              writeln(title);
              for(int i = 0; i < options.Length; i++)
              {
                writeln(i + " - " + options[i]);
              }
            }
        }
        class In
        {
          public static int menu(string title, params string[] options)
          {
            Out.printMenu(title, options);
            int result = prompt("Option");
            return result;
          }
          public static int prompt(string prompt)
          {
            Out.write(prompt + ": ");
            int store = Console.readLine();
            return store;
          }
        }
    }
}
