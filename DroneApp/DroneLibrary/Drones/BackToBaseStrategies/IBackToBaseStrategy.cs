using System.Collections.Generic;
using DroneLibrary.Drones.Data;

namespace DroneLibrary.Drones.BackToBaseStrategies
{
    public interface IBackToBaseStrategy
    {
        public IEnumerable<DroneMovement> GoBackToBase(IDrone drone);
    }
}