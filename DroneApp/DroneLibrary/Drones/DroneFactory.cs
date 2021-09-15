using DroneLibrary.Drones.BackToBaseStrategies;
using DroneLibrary.FlightAreas;

namespace DroneLibrary.Drones
{
    public abstract class DroneFactory
    {
        public abstract IDrone CreateDrone(IFlightArea flightArea, Coordinate position, int angle, Coordinate droneBase, IBackToBaseStrategy backToBaseStrategy);

        public abstract IDrone CreateDrone(IFlightArea flightArea, Coordinate position, int angle, IBackToBaseStrategy backToBaseStrategy);

        public abstract IDrone CreateDrone(IFlightArea flightArea, Coordinate position, int angle, Coordinate droneBase);

        public abstract IDrone CreateDrone(IFlightArea flightArea, Coordinate position, int angle);
    }
}