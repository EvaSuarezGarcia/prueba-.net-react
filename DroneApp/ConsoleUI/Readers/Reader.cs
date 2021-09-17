using System.IO;
using ConsoleUI.DroneClients;

namespace ConsoleUI.Readers
{
    public abstract class Reader
    {
        public ReaderState State { get; set; }

        public StreamReader StreamReader { get; }

        public Reader(StreamReader streamReader, IDroneClient droneClient)
        {
            StreamReader = streamReader;
        }

        public abstract void Read();
    }
}