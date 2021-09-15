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
        [InlineData(5, 4, -0.001, 2)]  // Slight deviation to the left
        [InlineData(5, 4, 5.001, 2)]   // Slight deviation to the right
        [InlineData(5, 4, 2, 4.001)]   // Slight deviation upwards
        [InlineData(5, 4, 2, -0.001)]  // Slight deviation downwards
        public void IsValidPositionInArea_ShouldBeTrue(int areaMaxX, int areaMaxY, double coordX, double coordY)
        {
            var area = new RectangularFlightArea(areaMaxX, areaMaxY);
            var position = new Coordinate(coordX, coordY);
            Assert.True(area.IsValidPositionInArea(position));
        }

        [Theory]
        [InlineData(3, 4, 5, 7)]  // Random point outside the area
        [InlineData(3, 4, 2, 5)]  // Outside upper limit
        [InlineData(3, 4, 2, -1)] // Outside lower limit
        [InlineData(3, 4, 4, 2)]  // Outside right limit
        [InlineData(3, 4, -1, 2)] // Outside left limit
        [InlineData(3, 4, -0.01, 2)]  // Too much deviation to the left
        [InlineData(3, 4, 3.01, 2)]   // Too much deviation to the right
        [InlineData(3, 4, 2, 4.01)]   // Too much deviation upwards
        [InlineData(3, 4, 2, -0.01)]  // Too much deviation downwards
        public void IsValidPositionInArea_ShouldBeFalse(int areaMaxX, int areaMaxY, double coordX, double coordY)
        {
            var area = new RectangularFlightArea(areaMaxX, areaMaxY);
            var position = new Coordinate(coordX, coordY);
            Assert.False(area.IsValidPositionInArea(position));
        }

        #endregion
    }
}