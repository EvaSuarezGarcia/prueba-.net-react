using System.IO;
using ConsoleUI.DroneClients;
using ConsoleUI.Readers.ReaderStates;

namespace ConsoleUI.Readers
{
    public class DefaultReader : Reader
    {
        public DefaultReader(StreamReader streamReader, IDroneClient droneClient) : base(streamReader, droneClient)
        {
            State = new ParseAreaState(this, droneClient);
        }

        public override void Read()
        {
            string line;
            while ((line = StreamReader.ReadLine()) != null)
            {
                State.Parse(line);
            }
        }
    }
}