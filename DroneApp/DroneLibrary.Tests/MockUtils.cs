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
    }
}