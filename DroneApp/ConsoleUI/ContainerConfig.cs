using System;
using System.IO;
using Autofac;
using ConsoleUI.DroneClients;
using ConsoleUI.Readers;
using ConsoleUI.Writers;

namespace ConsoleUI
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DefaultWriter>().As<Writer>()
                .WithParameter(new TypedParameter(typeof(StreamWriter), new StreamWriter(Console.OpenStandardOutput())));
            builder.RegisterType<LibraryDroneClient>().As<IDroneClient>();
            builder.RegisterType<DefaultReader>().As<Reader>();

            return builder.Build();
        }
    }
}