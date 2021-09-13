namespace DroneLibrary.Drones.Data
{
    public readonly struct DroneState
    {
        public DroneMovement Movement { get; }
        public Coordinate EndPosition { get; }

        public DroneState(Coordinate position, DroneMovement movement)
        {
            EndPosition = position;
            Movement = movement;
        }
    }
}