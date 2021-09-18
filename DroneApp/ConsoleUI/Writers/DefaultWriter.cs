using System.IO;
using ConsoleUI.Data;

namespace ConsoleUI.Writers
{
    public class DefaultWriter : Writer
    {
        public DefaultWriter(StreamWriter streamWriter) : base(streamWriter)
        {

        }

        public override void WriteFinalDroneState(DroneState state)
        {
            StreamWriter.WriteLine($"{state.PositionX} {state.PositionY} " +
                $"{FormatDroneOrientation(state.Orientation)}");
        }

        protected string FormatDroneOrientation(DroneOrientation orientation)
        {
            string result;
            switch (orientation)
            {
                case DroneOrientation.East:
                    result = "E";
                    break;
                case DroneOrientation.North:
                    result = "N";
                    break;
                case DroneOrientation.South:
                    result = "S";
                    break;
                case DroneOrientation.West:
                    result = "W";
                    break;
                default:
                    result = "?";
                    break;
            }

            return result;
        }
    }
}