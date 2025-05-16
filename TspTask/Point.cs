namespace TspTask;

public class Point
{
    private readonly int _x;
    private readonly int _y;
    private readonly int _z;
    public int X => _x;
    public int Y => _y;
    public int Z => _z;

    public override bool Equals(object? obj)
    {
        if (!(obj is Point point)) return false;
        return point._x == _x && point._y == _y && point._z == _z;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(_x, _y, _z);
    }

    public Point(int x = int.MaxValue, int y = int.MaxValue, int z = int.MaxValue)
    {
        _x = x;
        _y = y;
        _z = z;
    }

    public static double GetDistance(Point p1, Point p2) =>
        Math.Sqrt((p1._x - p2._x) * (p1._x - p2._x) + (p1._y - p2._y) * (p1._y - p2._y) +
                  (p1._z - p2._z) * (p1._z - p2._z));
}