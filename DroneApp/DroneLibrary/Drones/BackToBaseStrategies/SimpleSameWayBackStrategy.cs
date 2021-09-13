using System.Collections.Generic;
using System.Linq;
using DroneLibrary.Drones.Data;

namespace DroneLibrary.Drones.BackToBaseStrategies
{
    public class SimpleSameWayBackStrategy : IBackToBaseStrategy
    {
        public IDrone Drone { get; }

        public IEnumerable<DroneMovement> GoBackToBase(IDrone drone)
        {
            Validation.IsNotNull<IDrone>(nameof(drone), drone);

            IList<DroneMovement> resultMovements = new List<DroneMovement>();

            // Turn around and undo movements
            resultMovements.Add(new DroneMovement(180, 0));
            IEnumerable<DroneState> movementHistory = drone.MovementHistory;
            for (int i = movementHistory.Count() - 1; i > 0; i--)
            {
                var currentMovement = movementHistory.ElementAt(i);
                resultMovements.Add(new DroneMovement(-currentMovement.Movement.Angle, currentMovement.Movement.Speed));
            }

            return resultMovements;
        }
    }
}