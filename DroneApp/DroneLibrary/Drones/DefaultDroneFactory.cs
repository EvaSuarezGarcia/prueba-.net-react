using DroneLibrary.Drones.BackToBaseStrategies;
using DroneLibrary.FlightAreas;

namespace DroneLibrary.Drones
{
    public class DefaultDroneFactory : DroneFactory
    {
        public override IDrone CreateDrone(int angle, IFlightArea flightArea, Coordinate position, Coordinate droneBase, IBackToBaseStrategy backToBaseStrategy)
        {
            return new Drone(angle, flightArea, position, droneBase, backToBaseStrategy);
        }

        public override IDrone CreateDrone(int angle, IFlightArea flightArea, Coordinate position, IBackToBaseStrategy backToBaseStrategy)
        {
            return new Drone(angle, flightArea, position, backToBaseStrategy);
        }

        public override IDrone CreateDrone(int angle, IFlightArea flightArea, Coordinate position, Coordinate droneBase)
        {
            return new Drone(angle, flightArea, position, droneBase, new SimpleSameWayBackStrategy());
        }

        public override IDrone CreateDrone(int angle, IFlightArea flightArea, Coordinate position)
        {
            return new Drone(angle, flightArea, position, new SimpleSameWayBackStrategy());
        }
    }
}