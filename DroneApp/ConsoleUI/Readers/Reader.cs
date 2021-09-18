using System;
using System.IO;
using ConsoleUI.DroneClients;

namespace ConsoleUI.Readers
{
    public abstract class Reader : IDisposable
    {
        public ReaderState State { get; set; }

        public StreamReader StreamReader { get; }

        public Reader(StreamReader streamReader, IDroneClient droneClient)
        {
            StreamReader = streamReader;
        }

        public abstract void Read();

        public void Dispose()
        {
            if (StreamReader != null)
            {
                StreamReader.Dispose();
            }
        }
    }
}