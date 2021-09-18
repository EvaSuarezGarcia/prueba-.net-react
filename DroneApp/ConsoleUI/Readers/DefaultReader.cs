using System;
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
            int lineNumber = 1;

            try
            {
                while ((line = StreamReader.ReadLine()) != null)
                {
                    State.Parse(line.Trim());
                    lineNumber++;
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Error parsing line nº {lineNumber}: {e.Message}");
            }

            if (!State.IsFinalState)
            {
                Console.WriteLine($"Warning: Input is missing data, some lines may have been ignored.");
            }
        }
    }
}