using System;

namespace DroneLibrary
{
    public static class Validation
    {
        public static void IsGteTo<T>(string paramName, T param, T minValue) where T : IComparable
        {
            if (param.CompareTo(minValue) < 0)
            {
                throw new ArgumentOutOfRangeException(paramName, $"{paramName} must be greater or equal to ${minValue}");
            }
        }

        public static void IsNotNull<T>(string paramName, T param)
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName, $"{paramName} cannot be null");
            }
        }
    }
}