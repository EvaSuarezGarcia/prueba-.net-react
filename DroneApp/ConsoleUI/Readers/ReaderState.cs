using System.Configuration;
using ConsoleUI.DroneClients;

namespace ConsoleUI.Readers
{
    public abstract class ReaderState
    {
        public Reader Reader { get; }
        public IDroneClient DroneClient { get; }
        public RegexStringValidator RegexValidator { get; protected set; }

        protected ReaderState(Reader reader, IDroneClient droneClient)
        {
            Reader = reader;
            DroneClient = droneClient;
        }

        public abstract void Parse(string text);
    }
}