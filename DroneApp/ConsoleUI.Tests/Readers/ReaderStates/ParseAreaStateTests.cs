using System;
using System.IO;
using ConsoleUI.DroneClients;
using ConsoleUI.Readers;
using ConsoleUI.Readers.ReaderStates;
using Moq;
using Xunit;

namespace ConsoleUI.Tests.Readers.ReaderStates
{
    public class ParseAreaStateTests
    {
        private Mock<IDroneClient> _mockClient;
        private Mock<Reader> _mockReader;
        private ParseAreaState _state;

        public ParseAreaStateTests()
        {
            _mockClient = new Mock<IDroneClient>();
            _mockReader = new Mock<Reader>(new Mock<StreamReader>("test.txt").Object, _mockClient.Object);
            _state = new ParseAreaState(_mockReader.Object, _mockClient.Object);
        }

        [Theory]
        [InlineData("5 5", 5, 5)]
        [InlineData("20 0", 20, 0)]
        [InlineData("1 300", 1, 300)]
        public void Parse_ShouldWork(string text, int expectedX, int expectedY)
        {
            _state.Parse(text);

            _mockClient.Verify(m => m.CreateRectangularFlightArea(expectedX, expectedY));
            Assert.IsType<ParseDroneState>(_mockReader.Object.State);
        }

        [Theory]
        [InlineData("")]
        [InlineData("-10 1")]
        [InlineData("1 -1")]
        [InlineData("22")]
        public void Parse_ShouldFail(string text)
        {
            Assert.Throws<ArgumentException>(() => _state.Parse(text));
        }
    }
}