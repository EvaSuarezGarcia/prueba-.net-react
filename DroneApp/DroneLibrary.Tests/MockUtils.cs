using DroneLibrary.FlightAreas;
using Moq;

namespace DroneLibrary.Tests
{
    public static class MockUtils
    {
        public static void SetAsInfiniteFlightArea(Mock<IFlightArea> mock)
        {
            mock.Setup(m => m.IsValidPositionInArea(It.IsAny<Coordinate>())).Returns(true);
        }

        public static void SetAsSinglePointArea(Mock<IFlightArea> mock, int coordX = 0, int coordY = 0)
        {
            mock.Setup(m => m.IsValidPositionInArea(It.IsAny<Coordinate>()))
                .Returns((Coordinate c) => c.X == coordX && c.Y == coordY);
        }
    }
}