using System.Collections.Generic;
using DroneLibrary.Drones.BackToBaseStrategies;
using DroneLibrary.Drones.Data;
using DroneLibrary.FlightAreas;

namespace DroneLibrary.Drones
{
    public interface IDrone
    {
        public int Angle { get; }
        public Coordinate Position { get; }
        public Coordinate DroneBase { get; }
        public IEnumerable<DroneState> MovementHistory { get; }
        public IBackToBaseStrategy BackToBaseStrategy { get; set; }
        public IFlightArea FlightArea { get; }
        public bool Move(double speed);
        public void Turn(int angle);
        public bool GoBackToBase();
    }
}