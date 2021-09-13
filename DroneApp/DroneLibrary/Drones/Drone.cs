using System;
using System.Collections.Generic;
using DroneLibrary.Drones.BackToBaseStrategies;
using DroneLibrary.Drones.Data;
using DroneLibrary.FlightAreas;

namespace DroneLibrary.Drones
{
    public class Drone : IDrone
    {
        #region Properties

        private int _angle;
        public int Angle
        {
            get => _angle;
            set
            {
                // Reduce angle
                var newAngle = value % 360;
                // Force it to be in the positive remainder
                _angle = (newAngle + 360) % 360;
            }
        }

        public Coordinate Position { get; private set; }

        public Coordinate DroneBase { get; }

        private IList<DroneState> _movementHistory = new List<DroneState>();
        public IEnumerable<DroneState> MovementHistory
        {
            get => _movementHistory;
        }

        public IBackToBaseStrategy BackToBaseStrategy { get; set; }

        public IFlightArea FlightArea { get; }

        #endregion

        #region Ctors

        public Drone(int angle, IFlightArea flightArea, Coordinate position, IBackToBaseStrategy backToBaseStrategy)
            : this(angle, flightArea, position, position, backToBaseStrategy)
        {
        }

        public Drone(int angle, IFlightArea flightArea, Coordinate position, Coordinate droneBase, IBackToBaseStrategy backToBaseStrategy)
        {
            Validation.IsNotNull<IBackToBaseStrategy>(nameof(backToBaseStrategy), backToBaseStrategy);
            Validation.IsNotNull<IFlightArea>(nameof(flightArea), flightArea);

            if (!flightArea.IsValidPositionInArea(position))
            {
                throw new ArgumentException("Starting position must be inside the flight area", nameof(position));
            }

            Angle = angle;
            Position = position;
            DroneBase = droneBase;
            BackToBaseStrategy = backToBaseStrategy;
            FlightArea = flightArea;
            _movementHistory.Add(new DroneState(Position, new DroneMovement(Angle, 0)));
        }

        #endregion

        #region Methods

        public bool GoBackToBase()
        {
            IEnumerable<DroneMovement> movements = BackToBaseStrategy.GoBackToBase(this);
            foreach (var movement in movements)
            {
                Turn(movement.Angle);
                var couldMove = Move(movement.Speed);

                // Stop moving if something went wrong
                if (!couldMove)
                {
                    return false;
                }
            }
            // Check if we actually arrived at the base
            return Position.AproxEqual(DroneBase);
        }

        public bool Move(double speed)
        {
            bool newPositionIsValid = true;
            if (speed != 0)
            {
                double newX = Position.X + speed * Math.Cos(Angle * Math.PI / 180);
                double newY = Position.Y + speed * Math.Sin(Angle * Math.PI / 180);
                Coordinate newPosition = new Coordinate(newX, newY);

                newPositionIsValid = FlightArea.IsValidPositionInArea(newPosition);
                if (newPositionIsValid)
                {
                    Position = newPosition;
                    _movementHistory.Add(new DroneState(Position, new DroneMovement(0, speed)));
                }
            }

            return newPositionIsValid;
        }

        public void Turn(int angle)
        {
            if (angle != 0)
            {
                Angle += angle;
                _movementHistory.Add(new DroneState(Position, new DroneMovement(angle, 0)));
            }
        }

        #endregion
    }
}