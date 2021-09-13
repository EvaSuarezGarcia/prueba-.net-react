namespace DroneLibrary.FlightAreas
{
    public interface IFlightArea
    {
        public bool IsValidPositionInArea(Coordinate coordinate);
    }
}