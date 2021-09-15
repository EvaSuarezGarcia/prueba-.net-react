using DroneLibrary.Drones.BackToBaseStrategies;
using DroneLibrary.FlightAreas;

namespace DroneLibrary.Drones
{
    public class DefaultDroneFactory : DroneFactory
    {
        public override IDrone CreateDrone(IFlightArea flightArea, Coordinate position, int angle, Coordinate droneBase, IBackToBaseStrategy backToBaseStrategy)
        {
            return new Drone(flightArea, position, angle, droneBase, backToBaseStrategy);
        }

        public override IDrone CreateDrone(IFlightArea flightArea, Coordinate position, int angle, IBackToBaseStrategy backToBaseStrategy)
        {
            return new Drone(flightArea, position, angle, backToBaseStrategy);
        }

        public override IDrone CreateDrone(IFlightArea flightArea, Coordinate position, int angle, Coordinate droneBase)
        {
            return new Drone(flightArea, position, angle, droneBase, new SimpleSameWayBackStrategy());
        }

        public override IDrone CreateDrone(IFlightArea flightArea, Coordinate position, int angle)
        {
            return new Drone(flightArea, position, angle, new SimpleSameWayBackStrategy());
        }
    }
}