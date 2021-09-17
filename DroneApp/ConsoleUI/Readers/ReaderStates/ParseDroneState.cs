using System;
using System.Configuration;
using ConsoleUI.Data;
using ConsoleUI.DroneClients;

namespace ConsoleUI.Readers.ReaderStates
{
    public class ParseDroneState : ReaderState
    {
        public ParseDroneState(Reader reader, IDroneClient droneClient) : base(reader, droneClient)
        {
            RegexValidator = new RegexStringValidator(@"^[0-9]+ [0-9]+ [NESW]$");
        }

        public override void Parse(string text)
        {
            RegexValidator.Validate(text);

            string[] parts = text.Split();
            DroneState initialState = new DroneState(Int32.Parse(parts[0]), Int32.Parse(parts[1]),
                ParseDroneOrientation(parts[2]));

            Reader.State = new ParseActionsState(Reader, DroneClient, initialState);
        }

        private DroneOrientation ParseDroneOrientation(string letter)
        {
            DroneOrientation orientation;

            switch (letter)
            {
                case "N":
                    orientation = DroneOrientation.North;
                    break;
                case "S":
                    orientation = DroneOrientation.South;
                    break;
                case "W":
                    orientation = DroneOrientation.West;
                    break;
                case "E":
                    orientation = DroneOrientation.East;
                    break;
                default:
                    throw new ArgumentException($"Orientation not recognized: {letter}");
            }

            return orientation;
        }

    }
}