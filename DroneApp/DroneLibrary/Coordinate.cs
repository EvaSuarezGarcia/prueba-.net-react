using System;

public readonly struct Coordinate
{
    public double X { get; }
    public double Y { get; }

    public Coordinate(double x, double y)
    {
        X = x;
        Y = y;
    }

    public bool AproxEqual(Coordinate other)
    {
        /* This precision should be good enough for the purposes of this app, 
        but in a real example we probably should not rely on floating point arithmetic */
        return (
            Math.Abs(X - other.X) <= 0.001 &&
            Math.Abs(Y - other.Y) <= 0.001
        );
    }
}