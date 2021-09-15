namespace DroneLibrary.FlightAreas
{
    public interface IRectangularFlightArea : IFlightArea
    {
        public int MaxX { get; }
        public int MaxY { get; }
    }
}