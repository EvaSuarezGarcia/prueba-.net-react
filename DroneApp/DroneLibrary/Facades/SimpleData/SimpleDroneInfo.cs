namespace DroneLibrary.Facades.SimpleData
{
    public readonly struct SimpleDroneInfo
    {
        public int PositionX { get; }
        public int PositionY { get; }
        public SimpleDroneOrientation? Orientation { get; }

        public SimpleDroneInfo(int positionX, int positionY, SimpleDroneOrientation? orientation)
        {
            PositionX = positionX;
            PositionY = positionY;
            Orientation = orientation;
        }
    }
}