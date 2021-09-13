using System;
using DroneLibrary.FlightAreas;
using Xunit;

namespace DroneLibrary.Tests.FlightAreas
{
    public class RectangularFlightAreaTests
    {
        #region Ctor Tests

        [Theory]
        [InlineData(-3, 4)]
        [InlineData(0, -1)]
        [InlineData(-4, -5)]
        public void Ctor_ShouldFailWithNegativeParams(int maxX, int maxY)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new RectangularFlightArea(maxX, maxY));
        }

        #endregion

        #region IsValidPositionInArea Tests
        [Theory]
        [InlineData(5, 4, 2, 3)]  // Random point inside the area
        [InlineData(5, 4, 0, 0)]  // Lower left corner
        [InlineData(5, 4, 0, 4)]  // Upper left corner
        [InlineData(5, 4, 5, 4)]  // Upper right corner
        [InlineData(5, 4, 5, 0)]  // Lower right corner
        public void IsValidPositionInArea_ShouldBeTrue(int areaMaxX, int areaMaxY, int coordX, int coordY)
        {
            RectangularFlightArea area = new RectangularFlightArea(areaMaxX, areaMaxY);
            Coordinate position = new Coordinate(coordX, coordY);
            Assert.True(area.IsValidPositionInArea(position));
        }

        [Theory]
        [InlineData(3, 4, 5, 7)]  // Random point outside the area
        [InlineData(3, 4, 2, 5)]  // Outside upper limit
        [InlineData(3, 4, 2, -1)] // Outside lower limit
        [InlineData(3, 4, 4, 2)]  // Outside right limit
        [InlineData(3, 4, -1, 2)] // Outside left limit
        public void IsValidPositionInArea_ShouldBeFalse(int areaMaxX, int areaMaxY, int coordX, int coordY)
        {
            RectangularFlightArea area = new RectangularFlightArea(areaMaxX, areaMaxY);
            Coordinate position = new Coordinate(coordX, coordY);
            Assert.False(area.IsValidPositionInArea(position));
        }

        #endregion
    }
}