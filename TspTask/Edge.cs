namespace TspTask;

public class Edge
{
    private readonly Point _first;
    private readonly Point _second;

    public Point First => _first;
    public Point Second => _second;

    private readonly double _cost;
    public double Cost => _cost;

    public Edge(Point first, Point second, double cost)
    {
        _first = first;
        _second = second;
        _cost = cost;
    }
}