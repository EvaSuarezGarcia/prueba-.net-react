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

            var inputFile = "SampleInputs/sample.txt";

            if (args.Length > 0)
            {
                inputFile = args[0];
            }

            using (var scope = container.BeginLifetimeScope())
            {
                var reader = scope.Resolve<Reader>(
                        new TypedParameter(typeof(StreamReader), new StreamReader(inputFile)));
                reader.Read();
            }
        }
    }
}
