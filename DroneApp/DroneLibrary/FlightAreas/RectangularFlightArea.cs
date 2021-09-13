using System;

namespace DroneLibrary.FlightAreas
{
    public class RectangularFlightArea : IRectangularFlightArea
    {
        public int MaxX { get; }

        public int MaxY { get; }

        public RectangularFlightArea(int maxX, int maxY)
        {
            Validation.IsGteTo<int>(nameof(maxX), maxX, 0);
            Validation.IsGteTo<int>(nameof(maxY), maxY, 0);
            MaxX = maxX;
            MaxY = maxY;
        }

        public bool IsValidPositionInArea(Coordinate coordinate)
        {
            return 0 <= coordinate.X && coordinate.X <= MaxX
                && 0 <= coordinate.Y && coordinate.Y <= MaxY;
        }
    }
}