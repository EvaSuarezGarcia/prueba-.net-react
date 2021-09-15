using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.DictionaryAdapter;
using DroneLibrary.Drones;
using DroneLibrary.Drones.BackToBaseStrategies;
using DroneLibrary.Drones.Data;
using DroneLibrary.FlightAreas;
using Moq;
using Xunit;

namespace DroneLibrary.Tests.Drones
{
    public class DroneTests
    {
        #region Ctor Tests

        [Fact]
        public void Ctor_ShouldWork()
        {
            var mockArea = new Mock<IFlightArea>();
            MockUtils.SetAsInfiniteFlightArea(mockArea);
            var expectedBase = new Coordinate(1, 2);
            var expectedMovementHistory = new List<DroneState> { new DroneState(expectedBase, new DroneMovement(90, 0)) };

            var drone = new Drone(90, mockArea.Object, new Coordinate(1, 2), new Mock<IBackToBaseStrategy>().Object);

            Assert.Equal(expectedBase, drone.DroneBase);
            Assert.Equal(expectedMovementHistory, drone.MovementHistory);
        }

        [Fact]
        public void Ctor_ShouldFailIfStartingPointIsOutsideArea()
        {
            var mockArea = new Mock<IFlightArea>();
            mockArea.Setup(m => m.IsValidPositionInArea(It.IsAny<Coordinate>())).Returns(false);
            var backToBaseStrategy = new Mock<IBackToBaseStrategy>();

            Assert.Throws<ArgumentException>("position", () => new Drone(90, mockArea.Object, new Coordinate(0, 0), backToBaseStrategy.Object));
            mockArea.Verify(m => m.IsValidPositionInArea(It.IsAny<Coordinate>()), Times.Once());
        }

        public static IEnumerable<object[]> NullRequiredCtorArgumentsData => new List<object[]>
        {
            new object[] { "backToBaseStrategy", null, new Mock<IFlightArea>().Object },
            new object[] { "flightArea", new Mock<IBackToBaseStrategy>().Object, null }
        };

        [Theory]
        [MemberData(nameof(NullRequiredCtorArgumentsData))]
        public void Ctor_ShouldFailWithNullRequiredArguments(string nullParamName, IBackToBaseStrategy backToBaseStrategy, IFlightArea flightArea)
        {
            Assert.Throws<ArgumentNullException>(nullParamName, () => new Drone(90, flightArea, new Coordinate(0, 0), backToBaseStrategy));
        }

        #endregion

        #region Move Tests

        public static IEnumerable<object[]> ExpectedMovementData => new List<object[]>
        {
            new object[] { new Coordinate(0, 0), 0, 2, new Coordinate(2, 0) },
            new object[] { new Coordinate(0, 0), 90, 1, new Coordinate(0, 1) },
            new object[] { new Coordinate(0, 0), 180, 1.5, new Coordinate(-1.5, 0) },
            new object[] { new Coordinate(0, 0), 270, 0.5, new Coordinate(0, -0.5) },
            new object[] { new Coordinate(0, 0), 45, 1, new Coordinate(0.707, 0.707) },
            new object[] { new Coordinate(0, 0), 0, -1, new Coordinate(-1, 0) },
            new object[] { new Coordinate(1, 2), 0, 1, new Coordinate(2, 2) }
        };

        [Theory]
        [MemberData(nameof(ExpectedMovementData))]
        public void Move_ShouldWork(Coordinate startPosition, int angle, double speed, Coordinate expectedPosition)
        {
            var mockArea = new Mock<IFlightArea>();
            MockUtils.SetAsInfiniteFlightArea(mockArea);
            var mockBackToBaseStrategy = new Mock<IBackToBaseStrategy>();

            var drone = new Drone(angle, mockArea.Object, startPosition, mockBackToBaseStrategy.Object);
            var moveOk = drone.Move(speed);

            Assert.True(moveOk);
            Assert.True(drone.Position.AproxEqual(expectedPosition));
            Assert.Equal(2, drone.MovementHistory.Count());
            mockArea.Verify(m => m.IsValidPositionInArea(It.IsAny<Coordinate>()), Times.Exactly(2));
        }

        [Fact]
        public void Move_NoSpeed()
        {
            var mockArea = new Mock<IFlightArea>();
            MockUtils.SetAsInfiniteFlightArea(mockArea);
            var mockBackToBaseStrategy = new Mock<IBackToBaseStrategy>();
            var initialPosition = new Coordinate(0, 0);

            var drone = new Drone(0, mockArea.Object, initialPosition, mockBackToBaseStrategy.Object);
            var moveOk = drone.Move(0);

            Assert.True(moveOk);
            Assert.Equal(initialPosition, drone.Position);
            Assert.Single(drone.MovementHistory);
            mockArea.Verify(m => m.IsValidPositionInArea(It.IsAny<Coordinate>()), Times.Once());
        }

        [Fact]
        public void Move_ShouldFailOutsideArea()
        {
            var mockArea = new Mock<IFlightArea>();
            MockUtils.SetAsSinglePointArea(mockArea);
            var mockBackToBaseStrategy = new Mock<IBackToBaseStrategy>();
            var startPosition = new Coordinate(0, 0);

            var drone = new Drone(0, mockArea.Object, startPosition, mockBackToBaseStrategy.Object);
            var moveOk = drone.Move(1);

            Assert.False(moveOk);
            Assert.Equal(startPosition, drone.Position);
            Assert.Single(drone.MovementHistory);
            mockArea.Verify(m => m.IsValidPositionInArea(It.IsAny<Coordinate>()), Times.Exactly(2));
        }

        #endregion

        #region Turn Tests

        [Theory]
        [InlineData(0, 90, 90)]
        [InlineData(180, -45, 135)]
        [InlineData(0, 370, 10)]
        [InlineData(270, -380, 250)]
        public void Turn_ShouldWork(int startAngle, int anglesToTurn, int expectedAngle)
        {
            var mockArea = new Mock<IFlightArea>();
            MockUtils.SetAsInfiniteFlightArea(mockArea);
            var mockBackToBaseStrategy = new Mock<IBackToBaseStrategy>();
            var drone = new Drone(startAngle, mockArea.Object, new Coordinate(0, 0), mockBackToBaseStrategy.Object);

            drone.Turn(anglesToTurn);

            Assert.Equal(expectedAngle, drone.Angle);
            Assert.Equal(2, drone.MovementHistory.Count());
        }

        [Fact]
        public void Turn_NoAngle()
        {
            var mockArea = new Mock<IFlightArea>();
            MockUtils.SetAsInfiniteFlightArea(mockArea);
            var mockBackToBaseStrategy = new Mock<IBackToBaseStrategy>();
            var initialAngle = 90;
            var drone = new Drone(initialAngle, mockArea.Object, new Coordinate(0, 0), mockBackToBaseStrategy.Object);

            drone.Turn(0);

            Assert.Equal(initialAngle, drone.Angle);
            Assert.Single(drone.MovementHistory);
        }

        #endregion

        #region GoBackToBase Tests

        [Fact]
        public void GoBackToBase_ShouldWork()
        {
            var mockArea = new Mock<IFlightArea>();
            MockUtils.SetAsInfiniteFlightArea(mockArea);
            var mockBackToBaseStrategy = new Mock<IBackToBaseStrategy>();
            var backToBaseMovements = new List<DroneMovement> { new DroneMovement(0, -1) };
            mockBackToBaseStrategy.Setup(m => m.GoBackToBase(It.IsAny<IDrone>())).Returns(backToBaseMovements);
            var expectedMovementHistory = new List<DroneState>
            {
                new DroneState(new Coordinate(1, 2), new DroneMovement(0, 0)),
                new DroneState(new Coordinate(2, 2), new DroneMovement(0, 1)),
                new DroneState(new Coordinate(1, 2), new DroneMovement(0, -1))
            };

            var drone = new Drone(0, mockArea.Object, new Coordinate(1, 2), mockBackToBaseStrategy.Object);
            drone.Move(1);

            var wentBackToBase = drone.GoBackToBase();

            Assert.True(wentBackToBase);
            Assert.Equal(expectedMovementHistory, drone.MovementHistory);
            mockBackToBaseStrategy.Verify(m => m.GoBackToBase(It.IsAny<IDrone>()), Times.Once());
        }

        [Fact]
        public void GoBackToBase_ShouldFail()
        {
            var mockArea = new Mock<IFlightArea>();
            MockUtils.SetAsInfiniteFlightArea(mockArea);
            var mockBackToBaseStrategy = new Mock<IBackToBaseStrategy>();
            mockBackToBaseStrategy.Setup(m => m.GoBackToBase(It.IsAny<IDrone>()))
                .Returns(new List<DroneMovement> { new DroneMovement(0, 1) });
            var drone = new Drone(0, mockArea.Object, new Coordinate(1, 2), mockBackToBaseStrategy.Object);

            var wentBackToBase = drone.GoBackToBase();

            Assert.False(wentBackToBase);
            Assert.Equal(2, drone.MovementHistory.Count());
            mockBackToBaseStrategy.Verify(m => m.GoBackToBase(It.IsAny<IDrone>()), Times.Once());
        }

        [Fact]
        public void GoBackToBase_ShouldFailIfGoingOutsideArea()
        {
            var mockArea = new Mock<IFlightArea>();
            MockUtils.SetAsSinglePointArea(mockArea, 1, 2);
            var mockBackToBaseStrategy = new Mock<IBackToBaseStrategy>();
            mockBackToBaseStrategy.Setup(m => m.GoBackToBase(It.IsAny<IDrone>()))
                .Returns(new List<DroneMovement> { new DroneMovement(0, 1), new DroneMovement(0, -1) });
            var drone = new Drone(0, mockArea.Object, new Coordinate(1, 2), mockBackToBaseStrategy.Object);

            var wentBackToBase = drone.GoBackToBase();

            Assert.False(wentBackToBase);
            Assert.Single(drone.MovementHistory);
            mockBackToBaseStrategy.Verify(m => m.GoBackToBase(It.IsAny<IDrone>()), Times.Once());
        }

        #endregion
    }
}