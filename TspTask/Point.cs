namespace TspTask;

public class Point
{
    private readonly double _x;
    private readonly double _y;
    private readonly double _z;
    public double X => _x;
    public double Y => _y;
    public double Z => _z;

    public override bool Equals(object? obj)
    {
        if (!(obj is Point point)) return false;
        return Math.Abs(point._x - _x) < double.Epsilon && Math.Abs(point._y - _y) < double.Epsilon && Math.Abs(point._z - _z) < double.Epsilon;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(_x, _y, _z);
    }

    public Point(double x = double.MaxValue, double y = int.MaxValue, double z = int.MaxValue)
    {
        _x = x;
        _y = y;
        _z = z;
    }

    public static double GetDistance(Point p1, Point p2) =>
        Math.Sqrt((p1._x - p2._x) * (p1._x - p2._x) + (p1._y - p2._y) * (p1._y - p2._y) +
                  (p1._z - p2._z) * (p1._z - p2._z));
}