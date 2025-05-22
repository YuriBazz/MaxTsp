namespace TspTask;

public class Hitch
{
    private readonly List<Edge> _data;
    public List<Edge> Edges => _data;

    public void Add(Edge edge) => _data.Add(edge);
    public void AddRange(IEnumerable<Edge> edges) => _data.AddRange(edges);

    public Hitch(Edge edge) => _data = new List<Edge> { edge };

    public Edge First => _data[0];
    public Edge Last => _data[^1];


    public void Merge(Hitch hitch) => _data.AddRange(hitch.Edges);
    public override bool Equals(object? obj)
    {
        return obj is Hitch hitch && _data.SequenceEqual(hitch._data);
    }

    protected bool Equals(Hitch other)
    {
        return _data.Equals(other._data);
    }

    public override int GetHashCode()
    {
        return _data.GetHashCode();
    }
}