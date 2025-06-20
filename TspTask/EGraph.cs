using static TspTask.Point;

namespace TspTask;

public class EGraph
{
    private readonly double[,] _weights;
    private readonly Point[] _vertexes;
    private readonly double _max;

    public double Max => _max;
    public int N => _weights.GetLength(0);
    public double[,] Weights => _weights;
    public Point[] Vertexes => _vertexes;
    public EGraph(IEnumerable<Point> enumerable)
    {
        _vertexes = enumerable.ToArray();
        _weights = new double[_vertexes.Length, _vertexes.Length];
        _weights.Initialize();
        for(var i = 0; i < _vertexes.Length; ++i)
        for (var j = i + 1; j < _vertexes.Length; ++j)
            _weights[i, j] = _weights[j,i] = GetDistance(_vertexes[i], _vertexes[j]);
        _max = _weights.Cast<double>().Prepend(double.MinValue).Max();
    }

    public static List<Edge> GetPairs(EGraph gr)
    {
        var mwm = new MaxWeightMatching(gr);
        return mwm.Solve();
    }
}