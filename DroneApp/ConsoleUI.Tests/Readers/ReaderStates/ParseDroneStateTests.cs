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
    public class ParseDroneStateTests
    {
        private Mock<IDroneClient> _mockClient;
        private Mock<Reader> _mockReader;
        private ParseDroneState _state;

        public ParseDroneStateTests()
        {
            _mockClient = new Mock<IDroneClient>();
            _mockReader = new Mock<Reader>(new Mock<StreamReader>("test.txt").Object, _mockClient.Object);
            _state = new ParseDroneState(_mockReader.Object, _mockClient.Object);
        }

        public static IEnumerable<object[]> ParseValidData => new List<object[]>
        {
            new object[] { "0 1 N", new DroneState(0, 1, DroneOrientation.North) },
            new object[] { "5 4 S", new DroneState(5, 4, DroneOrientation.South) },
            new object[] { "22 22 E", new DroneState(22, 22, DroneOrientation.East)},
            new object[] { "300 7 W", new DroneState(300, 7, DroneOrientation.West) }
        };

        [Theory]
        [MemberData(nameof(ParseValidData))]
        public void Parse_ShouldWork(string text, DroneState expectedState)
        {
            _state.Parse(text);

            Assert.IsType<ParseActionsState>(_mockReader.Object.State);
            Assert.Equal(expectedState,
                ((ParseActionsState)_mockReader.Object.State).InitialState);
        }

        [Theory]
        [InlineData("")]
        [InlineData("invalid")]
        [InlineData("-1 1 N")]
        [InlineData("20 2 F")]
        [InlineData("1 1")]
        [InlineData("0 W")]
        [InlineData("S")]
        public void Parse_ShouldFail(string text)
        {
            Assert.Throws<ArgumentException>(() => _state.Parse(text));
        }
    }
}