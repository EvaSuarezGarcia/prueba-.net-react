namespace DroneLibrary.FlightAreas
{
    public class RectangularFlightArea : IRectangularFlightArea
    {
        public int MaxX { get; }

        public int MaxY { get; }

        public double Tolerance { get; set; }

        public RectangularFlightArea(int maxX, int maxY, double tolerance = 0.001)
        {
            Validation.IsGteTo<int>(nameof(maxX), maxX, 0);
            Validation.IsGteTo<int>(nameof(maxY), maxY, 0);
            MaxX = maxX;
            MaxY = maxY;
            Tolerance = tolerance;
        }

        public bool IsValidPositionInArea(Coordinate coordinate)
        {
            // Allow small errors in movement
            return 0 - Tolerance <= coordinate.X &&
                coordinate.X <= MaxX + Tolerance &&
                0 - Tolerance <= coordinate.Y &&
                coordinate.Y <= MaxY + Tolerance;
        }
    }
}