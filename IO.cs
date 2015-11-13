using System;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerbil
{
    namespace IO
    {
        public class Out
        {
            public static Queue<string> msgHold;
            public static bool awaitingInput;
            public static void init()
            {
                Out.msgHold = new Queue<string>();
                Out.awaitingInput = false;
            }
            /// <summary>
            /// Writes a line of labeled text to the CLI.
            /// </summary>
            /// <param name="input">Text to write.</param>
            public static void writeln(string sender, string input)
            {
                write(String.Format("[{0}] {1}\n", sender, input));
            }
            public static void rawWriteln(string input)
            {
                write(input + "\n");
            }
            /// <summary>
            /// Writes a string of text to the CLI.
            /// </summary>
            /// <param name="input">Text to write.</param>
            public static void write(string input)
            {
                if(Out.awaitingInput)
                {
                    Out.msgHold.Enqueue(input);
                }
                else
                {
                    Console.Write(input);
                }
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
                rawWriteln(title);
                for (int i = 0; i < options.Length; i++)
                {
                    rawWriteln(i + " - " + options[i]);
                }
            }
            public static void emptyQueue()
            {
                while(msgHold.Count > 0)
                {
                    write(Out.msgHold.Dequeue());
                }
            }
        }
        public class In
        {
            /// <summary>
            /// Prompts the user for input using a formatted graphical menu.
            /// </summary>
            /// <param name="title">Title of menu.</param>
            /// <param name="options">List of options to display.</param>
            /// <returns>Zero-indexed choice selected by user. (-1 if none)</returns>
            public static int menu(string title, params string[] options)
            {
                Out.printMenu(title, options);
                Out.rawWriteln("-1 to cancel.");
                int result = 0;
                while (true)
                {
                    result = prompt<int>("Option");
                    if(result >= -1 && result < options.Length)
                    {
                        return result;
                    }
                    Out.rawWriteln("Invalid input, enter a valid menu choice.");
                }
            }
            /// <summary>
            /// Prompts the user for input.
            /// </summary>
            /// <typeparam name="T">Type of variable to return.</typeparam>
            /// <param name="prompt">Prompt to display to user.</param>
            /// <returns>Input value by user.</returns>
            public static T prompt<T>(string prompt)
            {
                return prompt<T>(prompt, ':');
            }
            /// <summary>
            /// Prompts the user for input.
            /// </summary>
            /// <typeparam name="T">Type of variable to return.</typeparam>
            /// <param name="prompt">Prompt to display to user.</param>
            /// <param name="promptKey">Prompt char to display.</param>
            /// <returns>Input value by user.</returns>
            public static T prompt<T>(string prompt, char promptKey)
            {
                while (true)
                {
                    Out.write(prompt + promptKey + " ");
                    Out.awaitingInput = true;
                    string inval = Console.ReadLine();
                    try
                    {
                        T store = (T)Convert.ChangeType(inval, typeof(T));
                        Out.awaitingInput = false;
                        Out.emptyQueue();
                        return store;
                    }
                    catch
                    {
                        Out.awaitingInput = false;
                        Out.rawWriteln("Invalid input. Please enter a valid input.");
                        Out.awaitingInput = true;
                    }
                }
            }
            /// <summary>
            /// Prompts the user to perfom a dangerous or uncertain task.
            /// </summary>
            /// <param name="module">Name of module seeking permission.</param>
            /// <param name="action">Action being taken by the module.</param>
            /// <returns>Action allowed.</returns>
            public static bool securePrompt(string module, string action)
            {
                Out.writeln("Protection Service", String.Format("Module {0} is attempting to {1}.", module, action));
                int result = menu("Allow action?", "Yes", "No");
                if(result == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
