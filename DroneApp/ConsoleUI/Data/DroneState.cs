namespace ConsoleUI.Data
{
    public readonly struct DroneState
    {
        public int PositionX { get; }
        public int PositionY { get; }
        public DroneOrientation Orientation { get; }

        public DroneState(int positionX, int positionY, DroneOrientation orientation)
        {
            PositionX = positionX;
            PositionY = positionY;
            Orientation = orientation;
        }
    }
}