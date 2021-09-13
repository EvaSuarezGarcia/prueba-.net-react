namespace DroneLibrary.Drones.Data
{
    public readonly struct DroneMovement
    {
        public int Angle { get; }
        public double Speed { get; }

        public DroneMovement(int angle, double speed)
        {
            Angle = angle;
            Speed = speed;
        }
    }
}