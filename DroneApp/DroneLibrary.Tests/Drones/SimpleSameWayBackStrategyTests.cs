using System;
using System.Collections.Generic;
using DroneLibrary.Drones;
using DroneLibrary.Drones.BackToBaseStrategies;
using DroneLibrary.Drones.Data;
using Moq;
using Xunit;

namespace DroneLibrary.Tests.Drones
{
    public class SimpleSameWayBackStrategyTests
    {
        #region GoBackToBase Tests

        public static IEnumerable<object[]> ValidGoBackToBaseData = new List<object[]>
        {
            new object[]
            {
                new List<DroneState>
                {
                    new DroneState(new Coordinate(0, 0), new DroneMovement(90, 0)),
                    new DroneState(new Coordinate(0, 1), new DroneMovement(0, 1)),
                    new DroneState(new Coordinate(0, 1), new DroneMovement(-90, 0)),
                    new DroneState(new Coordinate(2, 1), new DroneMovement(0, 2)),
                    new DroneState(new Coordinate(2, 1), new DroneMovement(45, 0))
                },
                new List<DroneMovement>
                {
                    new DroneMovement(180, 0),
                    new DroneMovement(-45, 0),
                    new DroneMovement(0, 2),
                    new DroneMovement(90, 0),
                    new DroneMovement(0, 1)
                }
            },
            new object[]
            {
                new List<DroneState>
                {
                    new DroneState(new Coordinate(0, 0), new DroneMovement(0, 0))
                },
                new List<DroneMovement>
                {
                    new DroneMovement(180, 0)
                }
            }
        };

        [Theory]
        [MemberData(nameof(ValidGoBackToBaseData))]
        public void GoBackToBase_ShouldWork(IEnumerable<DroneState> droneHistory, IEnumerable<DroneMovement> expectedMovements)
        {
            var mockDrone = new Mock<IDrone>();
            mockDrone.Setup(m => m.MovementHistory).Returns(droneHistory);

            var strategy = new SimpleSameWayBackStrategy();
            var actualMovements = strategy.GoBackToBase(mockDrone.Object);

            Assert.Equal(expectedMovements, actualMovements);
        }

        [Fact]
        public void GoBackToBase_ShouldFailWithNullDrone()
        {
            var strategy = new SimpleSameWayBackStrategy();
            Assert.Throws<ArgumentNullException>("drone", () => strategy.GoBackToBase(null));
        }

        #endregion
    }
}