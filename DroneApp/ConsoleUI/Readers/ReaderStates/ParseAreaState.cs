using System;
using System.Configuration;
using ConsoleUI.DroneClients;

namespace ConsoleUI.Readers.ReaderStates
{
    public class ParseAreaState : ReaderState
    {
        public ParseAreaState(Reader reader, IDroneClient droneClient) : base(reader, droneClient)
        {
            RegexValidator = new RegexStringValidator(@"^[0-9]+ [0-9]+$");
            IsFinalState = false;
        }

        public override void Parse(string text)
        {
            RegexValidator.Validate(text);

            string[] parts = text.Split();
            DroneClient.CreateRectangularFlightArea(Int32.Parse(parts[0]), Int32.Parse(parts[1]));

            Reader.State = new ParseDroneState(Reader, DroneClient);
        }
    }
}