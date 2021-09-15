using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DroneLibrary.Drones;
using DroneLibrary.Facades.SimpleData;
using DroneLibrary.FlightAreas;

[assembly: InternalsVisibleTo("DroneLibrary.Tests")]

namespace DroneLibrary.Facades
{
    public class SimpleFacade
    {
        #region Fields

        internal IList<IFlightArea> _flightAreas = new List<IFlightArea>();
        internal IList<IDrone> _drones = new List<IDrone>();
        internal DroneFactory _droneFactory = new DefaultDroneFactory();

        #endregion

        #region Methods

        public int CreateRectangularFlightArea(int maxX, int maxY)
        {
            _flightAreas.Add(new RectangularFlightArea(maxX, maxY));
            return _flightAreas.Count - 1;
        }

        public int AddDrone(SimpleDroneOrientation orientation, int flightAreaId, int positionX, int positionY)
        {
            var flightArea = GetElementFromList<IFlightArea>(_flightAreas, flightAreaId, nameof(flightAreaId));
            _drones.Add(_droneFactory.CreateDrone(flightArea, new Coordinate(positionX, positionY), (int)orientation));
            return _drones.Count - 1;
        }

        public void TurnDroneLeft(int droneId)
        {
            var drone = GetElementFromList<IDrone>(_drones, droneId, nameof(droneId));
            TurnDroneLeft(drone);
        }

        private void TurnDroneLeft(IDrone drone)
        {
            drone.Turn(90);
        }

        public void TurnDroneRight(int droneId)
        {
            var drone = GetElementFromList<IDrone>(_drones, droneId, nameof(droneId));
            TurnDroneRight(drone);
        }

        private void TurnDroneRight(IDrone drone)
        {
            drone.Turn(-90);
        }

        public bool MoveDrone(int droneId)
        {
            var drone = GetElementFromList<IDrone>(_drones, droneId, nameof(droneId));
            return MoveDrone(drone);
        }

        private bool MoveDrone(IDrone drone)
        {
            return drone.Move(1);
        }

        public SimpleDroneInfo GetDroneInfo(int droneId)
        {
            var drone = GetElementFromList<IDrone>(_drones, droneId, nameof(droneId));
            return GetDroneInfo(drone);
        }

        private SimpleDroneInfo GetDroneInfo(IDrone drone)
        {
            var positionX = (int)Math.Round(drone.Position.X);
            var positionY = (int)Math.Round(drone.Position.Y);
            SimpleDroneOrientation? orientation = null;

            // It should be always defined, so if it isn't we should log it
            if (Enum.IsDefined(typeof(SimpleDroneOrientation), drone.Angle))
            {
                orientation = (SimpleDroneOrientation)drone.Angle;
            }

            return new SimpleDroneInfo(positionX, positionY, orientation);
        }

        public bool SendDroneBack(int droneId)
        {
            var drone = GetElementFromList<IDrone>(_drones, droneId, nameof(droneId));
            return SendDroneBack(drone);
        }

        private bool SendDroneBack(IDrone drone)
        {
            return drone.GoBackToBase();
        }

        public SimpleDroneResult FlyDrone(SimpleDroneOrientation orientation, int flightAreaId,
            int positionX, int positionY, IEnumerable<SimpleDroneAction> actions)
        {
            var droneId = AddDrone(orientation, flightAreaId, positionX, positionY);
            var drone = GetElementFromList<IDrone>(_drones, droneId, nameof(droneId));
            var finishedCorrectly = true;
            var moved = true;

            foreach (var action in actions)
            {
                switch (action)
                {
                    case SimpleDroneAction.Move:
                        moved = MoveDrone(drone);
                        break;
                    case SimpleDroneAction.TurnLeft:
                        TurnDroneLeft(drone);
                        break;
                    case SimpleDroneAction.TurnRight:
                        TurnDroneRight(drone);
                        break;
                }

                if (!moved)
                {
                    finishedCorrectly = false;
                    break;
                }
            }

            var endInfo = GetDroneInfo(drone);
            var wentBackToBase = SendDroneBack(drone);
            var currentInfo = GetDroneInfo(drone);

            return new SimpleDroneResult(finishedCorrectly, endInfo, wentBackToBase, currentInfo);
        }

        private T GetElementFromList<T>(IList<T> list, int index, string paramName)
        {
            T element;
            try
            {
                element = list[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentException($"Element with ID {index} does not exist", paramName);
            }

            return element;
        }

        #endregion
    }
}