using System;
using System.IO;
using ConsoleUI.Data;

namespace ConsoleUI.Writers
{
    public abstract class Writer : IDisposable
    {
        protected StreamWriter StreamWriter { get; }

        public Writer(StreamWriter streamWriter)
        {
            StreamWriter = streamWriter;
        }

        public virtual void WriteAreaCreationError(string message)
        {
            StreamWriter.WriteLine($"Error creating area: {message}.");
        }

        public virtual void WriteDroneBackToBaseError(DroneState lastState)
        {
            StreamWriter.WriteLine("Error: Drone could not go back to base. " +
                $"Last known position was: ({lastState.PositionX}, {lastState.PositionY}).");
        }

        public virtual void WriteDroneCreationError(string message)
        {
            StreamWriter.WriteLine($"Error creating drone: {message}.");
        }

        public virtual void WriteDronePathError()
        {
            StreamWriter.WriteLine($"Error: Path for drone was not valid. Drone was sent back to its base.");
        }

        public abstract void WriteFinalDroneState(DroneState state);

        public void Dispose()
        {
            if (StreamWriter != null)
            {
                StreamWriter.Dispose();
            }
        }
    }
}