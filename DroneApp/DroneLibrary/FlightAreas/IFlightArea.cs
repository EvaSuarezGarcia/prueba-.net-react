namespace DroneLibrary.FlightAreas
{
    public interface IFlightArea
    {
        public double Tolerance { get; set; }
        public bool IsValidPositionInArea(Coordinate coordinate);
    }
}