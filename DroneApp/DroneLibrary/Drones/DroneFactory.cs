using DroneLibrary.Drones.BackToBaseStrategies;
using DroneLibrary.FlightAreas;

namespace DroneLibrary.Drones
{
    public abstract class DroneFactory
    {
        public abstract IDrone CreateDrone(int angle, IFlightArea flightArea, Coordinate position, Coordinate droneBase, IBackToBaseStrategy backToBaseStrategy);

        public abstract IDrone CreateDrone(int angle, IFlightArea flightArea, Coordinate position, IBackToBaseStrategy backToBaseStrategy);

        public abstract IDrone CreateDrone(int angle, IFlightArea flightArea, Coordinate position, Coordinate droneBase);

        public abstract IDrone CreateDrone(int angle, IFlightArea flightArea, Coordinate position);
    }
}