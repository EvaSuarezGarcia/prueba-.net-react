using System;
using System.Collections.Generic;
using System.Configuration;
using ConsoleUI.Data;
using ConsoleUI.DroneClients;

namespace ConsoleUI.Readers.ReaderStates
{
    public class ParseActionsState : ReaderState
    {
        public DroneState InitialState { get; }

        public ParseActionsState(Reader reader, IDroneClient droneClient, DroneState initialState)
            : base(reader, droneClient)
        {
            RegexValidator = new RegexStringValidator(@"^[LRM]+$");
            InitialState = initialState;
        }

        public override void Parse(string text)
        {
            RegexValidator.Validate(text);

            IList<DroneAction> actions = new List<DroneAction>();
            foreach (var letter in text)
            {
                actions.Add(ParseDroneAction(letter));
            }
            DroneClient.FlyDrone(InitialState, actions);

            Reader.State = new ParseDroneState(Reader, DroneClient);
        }

        private DroneAction ParseDroneAction(char letter)
        {
            DroneAction action;

            switch (letter)
            {
                case 'R':
                    action = DroneAction.TurnRight;
                    break;
                case 'L':
                    action = DroneAction.TurnLeft;
                    break;
                case 'M':
                    action = DroneAction.Move;
                    break;
                default:
                    throw new ArgumentException($"Action not recognized: {letter}");
            }

            return action;
        }
    }
}