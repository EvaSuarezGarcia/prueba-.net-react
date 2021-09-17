using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ConsoleUI.Data;
using ConsoleUI.Writers;
using DroneLibrary.Facades;
using DroneLibrary.Facades.SimpleData;

[assembly: InternalsVisibleTo("ConsoleUI.Tests")]

namespace ConsoleUI.DroneClients
{
    public class LibraryDroneClient : IDroneClient
    {
        public Writer Writer { get; }
        protected internal SimpleFacade _droneFacade = new SimpleFacade();
        protected internal int? _flightAreaId = null;

        public LibraryDroneClient(Writer writer)
        {
            Writer = writer;
        }

        public void CreateRectangularFlightArea(int maxX, int maxY)
        {
            if (_flightAreaId != null)
            {
                throw new InvalidOperationException("A flight area already exists. No more flight areas are allowed.");
            }

            try
            {
                _flightAreaId = _droneFacade.CreateRectangularFlightArea(maxX, maxY);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Writer.WriteAreaCreationError(e.Message);
            }
        }

        public void FlyDrone(DroneState initialState, IEnumerable<DroneAction> actions)
        {
            if (_flightAreaId == null)
            {
                throw new InvalidOperationException("No flight area exists. Create a flight area before flying a drone.");
            }

            // Transform data understood by this app to data understood by the library
            SimpleDroneOrientation orientation = ToLibraryOrientation(initialState.Orientation, nameof(initialState));
            IEnumerable<SimpleDroneAction> libraryActions = ToLibraryActions(actions, nameof(actions));
            SimpleDroneResult result;

            try
            {
                result = _droneFacade.FlyDrone(orientation, (int)_flightAreaId,
                    initialState.PositionX, initialState.PositionY, libraryActions);
            }
            catch (ArgumentException e)
            {
                Writer.WriteDroneCreationError(e.Message);
                return;
            }

            // Write results
            Writer.WriteFinalDroneState(new DroneState(result.EndInfo.PositionX, result.EndInfo.PositionY,
                FromLibraryOrientation(result.EndInfo.Orientation)));

            if (!result.FinishedCorrectly)
            {
                Writer.WriteDronePathError();
            }

            if (!result.WentBackToBase)
            {
                Writer.WriteDroneBackToBaseError(new DroneState(result.CurrentInfo.PositionX, result.CurrentInfo.PositionY,
                    FromLibraryOrientation(result.CurrentInfo.Orientation)));
            }
        }

        private SimpleDroneOrientation ToLibraryOrientation(DroneOrientation orientation, string paramName)
        {
            SimpleDroneOrientation resultOrientation;
            switch (orientation)
            {
                case DroneOrientation.East:
                    resultOrientation = SimpleDroneOrientation.East;
                    break;
                case DroneOrientation.North:
                    resultOrientation = SimpleDroneOrientation.North;
                    break;
                case DroneOrientation.West:
                    resultOrientation = SimpleDroneOrientation.West;
                    break;
                case DroneOrientation.South:
                    resultOrientation = SimpleDroneOrientation.South;
                    break;
                default:
                    throw new ArgumentException("Provided drone orientation is not valid", paramName);
            }

            return resultOrientation;
        }

        private DroneOrientation FromLibraryOrientation(SimpleDroneOrientation? orientation)
        {
            DroneOrientation resultOrientation;
            switch (orientation)
            {
                case null:
                    resultOrientation = DroneOrientation.Unknown;
                    break;
                case SimpleDroneOrientation.East:
                    resultOrientation = DroneOrientation.East;
                    break;
                case SimpleDroneOrientation.North:
                    resultOrientation = DroneOrientation.North;
                    break;
                case SimpleDroneOrientation.West:
                    resultOrientation = DroneOrientation.West;
                    break;
                case SimpleDroneOrientation.South:
                    resultOrientation = DroneOrientation.South;
                    break;
                default:
                    throw new ArgumentException("Provided drone orientation is not valid");
            }

            return resultOrientation;
        }

        private IEnumerable<SimpleDroneAction> ToLibraryActions(IEnumerable<DroneAction> actions, string paramName)
        {
            IList<SimpleDroneAction> resultActions = new List<SimpleDroneAction>();

            foreach (var action in actions)
            {
                switch (action)
                {
                    case DroneAction.Move:
                        resultActions.Add(SimpleDroneAction.Move);
                        break;
                    case DroneAction.TurnLeft:
                        resultActions.Add(SimpleDroneAction.TurnLeft);
                        break;
                    case DroneAction.TurnRight:
                        resultActions.Add(SimpleDroneAction.TurnRight);
                        break;
                    default:
                        throw new ArgumentException($"Detected invalid action: {action}", paramName);
                }
            }

            return resultActions;
        }
    }
}