using System;
using System.Collections.Generic;
using System.IO;
using ConsoleUI.Data;
using ConsoleUI.DroneClients;
using ConsoleUI.Readers;
using ConsoleUI.Readers.ReaderStates;
using Moq;
using Xunit;

namespace ConsoleUI.Tests.Readers.ReaderStates
{
    public class ParseActionsStateTests
    {
        private Mock<IDroneClient> _mockClient;
        private Mock<Reader> _mockReader;
        private DroneState _droneState = new DroneState(0, 0, Data.DroneOrientation.East);
        private ParseActionsState _state;

        public ParseActionsStateTests()
        {
            _mockClient = new Mock<IDroneClient>();
            _mockReader = new Mock<Reader>(new Mock<StreamReader>("test.txt").Object, _mockClient.Object);
            _state = new ParseActionsState(_mockReader.Object, _mockClient.Object, _droneState);
        }

        public static IEnumerable<object[]> ParseValidData => new List<object[]>
        {
            new object[] { "L", new List<DroneAction>{ DroneAction.TurnLeft } },
            new object[] { "R", new List<DroneAction>{ DroneAction.TurnRight } },
            new object[] { "M", new List<DroneAction>{ DroneAction.Move } },
            new object[] { "LLRMRML",
                new List<DroneAction>
                {
                    DroneAction.TurnLeft,
                    DroneAction.TurnLeft,
                    DroneAction.TurnRight,
                    DroneAction.Move,
                    DroneAction.TurnRight,
                    DroneAction.Move,
                    DroneAction.TurnLeft
                }
            }
        };

        [Theory]
        [MemberData(nameof(ParseValidData))]
        public void Parse_ShouldWork(string text, IEnumerable<DroneAction> expectedActions)
        {
            _state.Parse(text);

            _mockClient.Verify(m => m.FlyDrone(_droneState, expectedActions), Times.Once);
            Assert.IsType<ParseDroneState>(_mockReader.Object.State);
        }

        [Theory]
        [InlineData("")]
        [InlineData("invalid")]
        [InlineData("lda M")]
        [InlineData("L M R")]
        [InlineData("01293")]
        public void Parse_ShouldFail(string text)
        {
            Assert.Throws<ArgumentException>(() => _state.Parse(text));
        }
    }
}