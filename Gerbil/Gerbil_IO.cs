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
            /// <summary>
            /// Writes a line of text to the CLI.
            /// </summary>
            /// <param name="input">Text to write.</param>
            public static void writeln(string input)
            {
                write(input + "\n");
            }
            /// <summary>
            /// Writes a string of text to the CLI.
            /// </summary>
            /// <param name="input">Text to write.</param>
            public static void write(string input)
            {
                Console.Write(input);
            }
            /// <summary>
            /// Writes a blank line to the CLI.
            /// </summary>
            public static void blank()
            {
                write("\n");
            }
            /// <summary>
            /// Writes a number of blank lines to the CLI.
            /// </summary>
            /// <param name="iterations">Blank lines to insert.</param>
            public static void blank(int iterations)
            {
                for (int i = 0; i < iterations; i++)
                {
                    blank();
                }
            }
            /// <summary>
            /// Prints a formatted menu to the CLI.
            /// </summary>
            /// <param name="title">Title of menu.</param>
            /// <param name="options">List of options to display.</param>
            public static void printMenu(string title, params string[] options)
            {
                writeln(title);
                for (int i = 0; i < options.Length; i++)
                {
                    writeln(i + " - " + options[i]);
                }
            }
        }
        class In
        {
            /// <summary>
            /// Prompts the user for input using a formatted graphical menu.
            /// </summary>
            /// <param name="title">Title of menu.</param>
            /// <param name="options">List of options to display.</param>
            /// <returns>Choice selected by user. (-1 if none)</returns>
            public static int menu(string title, params string[] options)
            {
                Out.printMenu(title, options);
                int result = prompt<int>("Option");
                Out.blank();
                return result;
            }
            /// <summary>
            /// Prompts the user for input.
            /// </summary>
            /// <typeparam name="T">Type of variable to return.</typeparam>
            /// <param name="prompt">Prompt to display to user.</param>
            /// <returns>Input value by user.</returns>
            public static T prompt<T>(string prompt)
            {
                Out.write(prompt + ": ");
                T store = (T)Convert.ChangeType(Console.ReadLine(), typeof(T));
                return store;
            }
        }
    }
}
