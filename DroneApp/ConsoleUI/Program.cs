using System;
using System.IO;
using Autofac;
using ConsoleUI.Readers;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = ContainerConfig.Configure();

            if (args.Length == 0)
            {
                Console.WriteLine("Please specify the path to the input file.");
                return;
            }

            var inputFile = args[0];

            using (var scope = container.BeginLifetimeScope())
            {
                var reader = scope.Resolve<Reader>(
                        new TypedParameter(typeof(StreamReader), new StreamReader(inputFile)));
                reader.Read();
            }
        }
    }
}
