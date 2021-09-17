using System;
using System.Collections.Generic;
using ConsoleUI.Data;
using ConsoleUI.DroneClients;
using ConsoleUI.Writers;
using Moq;
using Xunit;

namespace ConsoleUI.Tests.DroneClients
{
    public class LibraryDroneClientTests
    {
        private Mock<Writer> writer;
        private LibraryDroneClient client;

        public LibraryDroneClientTests()
        {
            writer = new Mock<Writer>();
            client = new LibraryDroneClient(writer.Object);
        }

        #region CreateRectangularFlightArea Tests

        [Fact]
        public void CreateRectangularFlightArea_ShouldWork()
        {
            client.CreateRectangularFlightArea(5, 5);

            Assert.NotNull(client._flightAreaId);
        }

        [Fact]
        public void CreateRectangularFlightArea_CanOnlyBeCalledOnce()
        {
            client.CreateRectangularFlightArea(1, 2);

            Assert.Throws<InvalidOperationException>(() => client.CreateRectangularFlightArea(3, 4));
        }

        [Fact]
        public void CreateRectangularFlightArea_WritesErrorWithInvalidArguments()
        {
            client.CreateRectangularFlightArea(-1, 0);

            writer.Verify(m => m.WriteAreaCreationError(It.IsAny<string>()), Times.Once);
        }

        #endregion

        #region FlyDrone Tests

        public static IEnumerable<object[]> FlyDroneValidPathData => new List<object[]>
        {
            new object[] {
                new DroneState(1, 1, DroneOrientation.East),
                new DroneState(2, 1, DroneOrientation.East),
                new List<DroneAction> { DroneAction.Move }
            },
            new object[] {
                new DroneState(1, 1, DroneOrientation.North),
                new DroneState(1, 2, DroneOrientation.North),
                new List<DroneAction> { DroneAction.Move }
            },
            new object[] {
                new DroneState(1, 1, DroneOrientation.South),
                new DroneState(1, 1, DroneOrientation.West),
                new List<DroneAction> { DroneAction.TurnRight }
            },
            new object[] {
                new DroneState(1, 1, DroneOrientation.West),
                new DroneState(1, 1, DroneOrientation.South),
                new List<DroneAction> { DroneAction.TurnLeft }
            }
        };

        [Theory]
        [MemberData(nameof(FlyDroneValidPathData))]
        public void FlyDrone_ShouldWork(DroneState initialState, DroneState expectedState,
            IEnumerable<DroneAction> actions)
        {
            client.CreateRectangularFlightArea(2, 2);
            client.FlyDrone(initialState, actions);

            writer.Verify(m => m.WriteFinalDroneState(expectedState), Times.Once);
            writer.Verify(m => m.WriteDronePathError(), Times.Never);
            writer.Verify(m => m.WriteDroneBackToBaseError(It.IsAny<DroneState>()), Times.Never);
        }

        public static IEnumerable<object[]> FlyDroneNotValidPathData => new List<object[]>
        {
            new object[] {
                new DroneState(2, 1, DroneOrientation.East),
                new DroneState(2, 1, DroneOrientation.East),
                new List<DroneAction> { DroneAction.Move }
            },
            new object[] {
                new DroneState(1, 1, DroneOrientation.North),
                new DroneState(1, 2, DroneOrientation.North),
                new List<DroneAction> { DroneAction.Move, DroneAction.Move }
            },
            new object[] {
                new DroneState(1, 0, DroneOrientation.South),
                new DroneState(1, 0, DroneOrientation.South),
                new List<DroneAction> { DroneAction.Move }
            },
            new object[] {
                new DroneState(1, 1, DroneOrientation.West),
                new DroneState(0, 1, DroneOrientation.West),
                new List<DroneAction> { DroneAction.Move, DroneAction.Move }
            }
        };

        [Theory]
        [MemberData(nameof(FlyDroneNotValidPathData))]
        public void FlyDrone_ShouldWorkWithNotValidPath(DroneState initialState, DroneState expectedState,
            IEnumerable<DroneAction> actions)
        {
            client.CreateRectangularFlightArea(2, 2);
            client.FlyDrone(initialState, actions);

            writer.Verify(m => m.WriteFinalDroneState(expectedState), Times.Once);
            writer.Verify(m => m.WriteDronePathError(), Times.Once);
            writer.Verify(m => m.WriteDroneBackToBaseError(It.IsAny<DroneState>()), Times.Never);
        }

        [Fact]
        public void FlyDrone_ShouldFailWithoutArea()
        {
            Assert.Throws<InvalidOperationException>(() =>
                client.FlyDrone(new DroneState(1, 2, DroneOrientation.East), new List<DroneAction> { }));

        }

        [Fact]
        public void FlyDrone_WritesErrorWithInvalidDroneArguments()
        {
            client.CreateRectangularFlightArea(5, 5);

            client.FlyDrone(new DroneState(-1, 0, DroneOrientation.North), new List<DroneAction> { });

            writer.Verify(m => m.WriteDroneCreationError(It.IsAny<string>()), Times.Once);
        }

        #endregion
    }
}