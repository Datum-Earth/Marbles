using MarbleTracker.Core.Service;
using System;
using System.Runtime.CompilerServices;

namespace MarbleTracker.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MarbleTracker CLI v.0.0.1");

            while (true)
            {
                
                Console.WriteLine("> Awaiting input. Enter 'help' for available commands.");

                var input = Console.ReadLine();

                if (input.ToLower() == "help")
                {
                    Console.WriteLine("do-test | Arguments: --input / -i (\"*\") | Usage: Returns input back to user.");
                } else
                {
                    try
                    {
                        ExecuteCommand(input);
                    } catch
                    {
                        Console.WriteLine("Invalid command.");
                    }
                }
            }
        }

        static void ExecuteCommand(string input)
        {
            var service = new ServiceBuilder(ServiceStrategy.Standard);

            Console.WriteLine(service.GetExecutor().Execute(input));
        }
    }
}
