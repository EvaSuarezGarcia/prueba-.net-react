using System;
using System.Collections.Generic;
using DroneLibrary.Facades;
using DroneLibrary.Facades.SimpleData;
using DroneLibrary.FlightAreas;
using Xunit;

namespace DroneLibrary.Tests
{
    public class SimpleFacadeTests
    {
        private SimpleFacade facade;

        public SimpleFacadeTests()
        {
            facade = new SimpleFacade();
        }

        #region CreateRectangularFlightArea Tests

        [Fact]
        public void CreateRectangularFlightArea_ShouldWork()
        {
            CreateRectangularAreaAndCheck(facade, 5, 4, 0, 1);
            CreateRectangularAreaAndCheck(facade, 3, 6, 1, 2);
        }

        private void CreateRectangularAreaAndCheck(SimpleFacade facade, int maxX, int maxY, int expectedId, int expectedAreas)
        {
            int areaId = facade.CreateRectangularFlightArea(maxX, maxY);

            Assert.Equal(expectedId, areaId);
            Assert.Equal(expectedAreas, facade._flightAreas.Count);

            var rectangularArea = (IRectangularFlightArea)facade._flightAreas[areaId];
            Assert.Equal(maxX, rectangularArea.MaxX);
            Assert.Equal(maxY, rectangularArea.MaxY);
        }

        #endregion

        #region AddDrone Tests

        // Probar a añadir a áreas que no existen
        [Fact]
        public void AddDrone_ShouldWork()
        {
            int areaId = facade.CreateRectangularFlightArea(5, 4);
            var expectedDroneId = 0;
            var expectedX = 1;
            var expectedY = 2;

            int droneId = facade.AddDrone(SimpleDroneOrientation.East, areaId, expectedX, expectedY);

            Assert.Single(facade._drones);
            Assert.Equal(expectedDroneId, droneId);

            var drone = facade._drones[droneId];
            Assert.Equal(facade._flightAreas[areaId], drone.FlightArea);
            Assert.Equal((int)SimpleDroneOrientation.East, drone.Angle);
            Assert.Equal(expectedX, drone.Position.X);
            Assert.Equal(expectedY, drone.Position.Y);
            Assert.Equal(drone.Position, drone.DroneBase);
        }

        [Fact]
        public void AddDrone_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>("flightAreaId",
                () => facade.AddDrone(SimpleDroneOrientation.North, 0, 0, 0));

            facade.CreateRectangularFlightArea(3, 3);

            Assert.Throws<ArgumentException>("flightAreaId",
                () => facade.AddDrone(SimpleDroneOrientation.North, 1, 0, 0));
        }

        #endregion

        #region TurnDroneLeft Tests

        [Theory]
        [InlineData(SimpleDroneOrientation.North, SimpleDroneOrientation.West)]
        [InlineData(SimpleDroneOrientation.West, SimpleDroneOrientation.South)]
        [InlineData(SimpleDroneOrientation.South, SimpleDroneOrientation.East)]
        [InlineData(SimpleDroneOrientation.East, SimpleDroneOrientation.North)]
        public void TurnDroneLeft_ShouldWork(SimpleDroneOrientation initialOrientation, SimpleDroneOrientation expectedOrientation)
        {
            var areaId = facade.CreateRectangularFlightArea(5, 5);
            var droneId = facade.AddDrone(initialOrientation, areaId, 1, 1);

            facade.TurnDroneLeft(droneId);

            Assert.Equal((int)expectedOrientation, facade._drones[droneId].Angle);
        }

        [Fact]
        public void TurnDroneLeft_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>("droneId", () => facade.TurnDroneLeft(0));
        }

        #endregion

        #region TurnDroneRight Tests

        [Theory]
        [InlineData(SimpleDroneOrientation.North, SimpleDroneOrientation.East)]
        [InlineData(SimpleDroneOrientation.East, SimpleDroneOrientation.South)]
        [InlineData(SimpleDroneOrientation.South, SimpleDroneOrientation.West)]
        [InlineData(SimpleDroneOrientation.West, SimpleDroneOrientation.North)]
        public void TurnDroneRight_ShouldWork(SimpleDroneOrientation initialOrientation, SimpleDroneOrientation expectedOrientation)
        {
            var areaId = facade.CreateRectangularFlightArea(5, 5);
            var droneId = facade.AddDrone(initialOrientation, areaId, 1, 1);

            facade.TurnDroneRight(droneId);

            Assert.Equal((int)expectedOrientation, facade._drones[droneId].Angle);
        }

        [Fact]
        public void TurnDroneRight_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>("droneId", () => facade.TurnDroneRight(0));
        }


        #endregion

        #region MoveDrone Tests

        [Theory]
        [InlineData(1, 2, SimpleDroneOrientation.East, 2, 2)]
        [InlineData(1, 2, SimpleDroneOrientation.North, 1, 3)]
        [InlineData(1, 2, SimpleDroneOrientation.West, 0, 2)]
        [InlineData(1, 2, SimpleDroneOrientation.South, 1, 1)]
        public void MoveDrone_ShouldWork(int initialX, int initialY, SimpleDroneOrientation orientation,
            int expectedX, int expectedY)
        {
            var areaId = facade.CreateRectangularFlightArea(5, 5);
            var droneId = facade.AddDrone(orientation, areaId, initialX, initialY);
            var drone = facade._drones[droneId];

            Assert.True(facade.MoveDrone(droneId));
            Assert.Equal(expectedX, (int)Math.Round(drone.Position.X));
            Assert.Equal(expectedY, (int)Math.Round(drone.Position.Y));
        }

        [Theory]
        [InlineData(5, 2, SimpleDroneOrientation.East)]
        [InlineData(1, 5, SimpleDroneOrientation.North)]
        [InlineData(0, 2, SimpleDroneOrientation.West)]
        [InlineData(1, 0, SimpleDroneOrientation.South)]
        public void MoveDrone_InvalidMove(int initialX, int initialY, SimpleDroneOrientation orientation)
        {
            var areaId = facade.CreateRectangularFlightArea(5, 5);
            var droneId = facade.AddDrone(orientation, areaId, initialX, initialY);
            var drone = facade._drones[droneId];

            Assert.False(facade.MoveDrone(droneId));
            Assert.Equal(initialX, (int)Math.Round(drone.Position.X));
            Assert.Equal(initialY, (int)Math.Round(drone.Position.Y));
        }

        [Fact]
        public void MoveDrone_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>("droneId", () => facade.MoveDrone(0));
        }

        #endregion

        #region GetDroneInfo Tests

        [Theory]
        [InlineData(SimpleDroneOrientation.East, 1, 2)]
        [InlineData(SimpleDroneOrientation.North, 4, 3)]
        [InlineData(SimpleDroneOrientation.West, 5, 5)]
        [InlineData(SimpleDroneOrientation.West, 0, 0)]
        public void GetDroneInfo_ShouldWork(SimpleDroneOrientation orientation, int positionX, int positionY)
        {
            var areaId = facade.CreateRectangularFlightArea(5, 5);
            var droneId = facade.AddDrone(orientation, areaId, positionX, positionY);
            var expectedInfo = new SimpleDroneInfo(positionX, positionY, orientation);

            Assert.Equal(expectedInfo, facade.GetDroneInfo(droneId));
        }

        [Fact]
        public void GetDroneInfo_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>("droneId", () => facade.GetDroneInfo(1));
        }

        #endregion

        #region SendDroneBack Tests

        [Fact]
        public void SendDroneBack_ShouldWork()
        {
            var areaId = facade.CreateRectangularFlightArea(5, 5);
            var initialX = 1;
            var initialY = 1;
            var droneId = facade.AddDrone(SimpleDroneOrientation.North, areaId, initialX, initialY);
            var drone = facade._drones[droneId];

            facade.MoveDrone(droneId);
            facade.TurnDroneLeft(droneId);
            facade.MoveDrone(droneId);
            facade.TurnDroneRight(droneId);
            facade.MoveDrone(droneId);

            Assert.True(facade.SendDroneBack(droneId));
            Assert.Equal(initialX, (int)Math.Round(drone.Position.X));
            Assert.Equal(initialY, (int)Math.Round(drone.Position.Y));
            Assert.Equal((int)SimpleDroneOrientation.South, drone.Angle);
        }

        [Fact]
        public void SendDroneBack_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>("droneId", () => facade.SendDroneBack(2));
        }

        #endregion

        #region FlyDrone Tests

        public static IEnumerable<object[]> FlyDroneData => new List<object[]>
        {
            // Valid path
            new object[]
            {
                SimpleDroneOrientation.East, 1, 1,
                new List<SimpleDroneAction> {SimpleDroneAction.Move, SimpleDroneAction.TurnLeft,
                    SimpleDroneAction.Move},
                new SimpleDroneResult(true, new SimpleDroneInfo(2, 2, SimpleDroneOrientation.North),
                    true, new SimpleDroneInfo(1, 1, SimpleDroneOrientation.West))
            },
            // Invalid path
            new object[]
            {
                SimpleDroneOrientation.West, 1, 1,
                new List<SimpleDroneAction> {SimpleDroneAction.Move, SimpleDroneAction.Move},
                new SimpleDroneResult(false, new SimpleDroneInfo(0, 1, SimpleDroneOrientation.West),
                    true, new SimpleDroneInfo(1, 1, SimpleDroneOrientation.East))
            }
        };

        [Theory]
        [MemberData(nameof(FlyDroneData))]
        public void FlyDrone_ShouldWork(SimpleDroneOrientation initialOrientation, int initialX, int initialY,
            IEnumerable<SimpleDroneAction> actions, SimpleDroneResult expectedResult)
        {
            var areaId = facade.CreateRectangularFlightArea(5, 5);
            var actualResult = facade.FlyDrone(initialOrientation, areaId, initialX, initialY, actions);
            Assert.Equal(expectedResult, actualResult);
        }

        #endregion
    }
}