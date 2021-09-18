using System.Collections.Generic;
using System.IO;
using System.Text;
using ConsoleUI.Data;
using ConsoleUI.DroneClients;
using ConsoleUI.Readers;
using Moq;
using Xunit;

namespace ConsoleUI.Tests.Readers
{
    public class ReaderTests
    {
        [Fact]
        public void Read_ShouldWork()
        {
            var text = "5 5\n3 3 E\nL\n1 2 N\nLMLMR";
            var droneState1 = new DroneState(3, 3, DroneOrientation.East);
            var actions1 = new List<DroneAction> { DroneAction.TurnLeft };
            var droneState2 = new DroneState(1, 2, DroneOrientation.North);
            var actions2 = new List<DroneAction>
            {
                DroneAction.TurnLeft,
                DroneAction.Move,
                DroneAction.TurnLeft,
                DroneAction.Move,
                DroneAction.TurnRight
            };

            var textBytes = Encoding.UTF8.GetBytes(text);
            var textMemoryStream = new MemoryStream(textBytes);
            var streamReader = new StreamReader(textMemoryStream);
            var mockClient = new Mock<IDroneClient>();
            var reader = new DefaultReader(streamReader, mockClient.Object);

            reader.Read();

            mockClient.Verify(m => m.CreateRectangularFlightArea(5, 5), Times.Once);
            mockClient.Verify(m => m.FlyDrone(droneState1, actions1), Times.Once);
            mockClient.Verify(m => m.FlyDrone(droneState2, actions2), Times.Once);
            Assert.True(reader.State.IsFinalState);
        }

        [Theory]
        [InlineData("1 1")]
        [InlineData("3 2\n1 2 W")]
        public void Read_ShouldWorkButNotFinalState(string text)
        {
            var textBytes = Encoding.UTF8.GetBytes(text);
            var textMemoryStream = new MemoryStream(textBytes);
            var streamReader = new StreamReader(textMemoryStream);
            var mockClient = new Mock<IDroneClient>();
            var reader = new DefaultReader(streamReader, mockClient.Object);

            reader.Read();

            Assert.False(reader.State.IsFinalState);
        }
    }
}