namespace TspTask;

public class Point
{
    private readonly double _x;
    private readonly double _y;
    public double X => _x;
    public double Y => _y;

    public override bool Equals(object? obj)
    {
        if (!(obj is Point point)) return false;
        return Math.Abs(point._x - _x) < double.Epsilon && Math.Abs(point._y - _y) < double.Epsilon;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(_x, _y);
    }

    public Point(double x = double.MaxValue, double y = int.MaxValue, double z = int.MaxValue)
    {
        _x = x;
        _y = y;
    }

    public static double GetDistance(Point p1, Point p2)
    {
        if (p1.Equals(p2)) return 0;
        return Math.Sqrt((p1._x - p2._x) * (p1._x - p2._x) + (p1._y - p2._y) * (p1._y - p2._y));
    }
}