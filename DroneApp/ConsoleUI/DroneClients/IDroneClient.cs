using System.Collections.Generic;
using ConsoleUI.Data;
using ConsoleUI.Writers;

namespace ConsoleUI.DroneClients
{
    public interface IDroneClient
    {
        public Writer Writer { get; }
        public void CreateRectangularFlightArea(int maxX, int maxY);
        public void FlyDrone(DroneState droneState, IEnumerable<DroneAction> actions);
    }
}