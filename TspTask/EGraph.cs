using static TspTask.Point;

namespace TspTask;

public class EGraph
{
    private readonly double[,] _weights;
    private readonly Point[] _vertexes;
    private readonly double _max;

    public double Max => _max;
    public int N => _weights.GetLength(0);
    
    public EGraph(IEnumerable<Point> enumerable)
    {
        _vertexes = enumerable.ToArray();
        _weights = new double[_vertexes.Length, _vertexes.Length];
        for(var i = 0; i < _vertexes.Length; ++i)
        for (var j = i + 1; j < _vertexes.Length; ++j)
            _weights[i, j] = _weights[j,i] = GetDistance(_vertexes[i], _vertexes[j]);
        _max = _weights.Cast<double>().Prepend(double.MinValue).Max();
        
        ChangeMatrix(); // Меняет задачу с максимума на минимум (наверное)
    }

    private void ChangeMatrix()
    {
       
        
        for (var i = 0; i < _weights.GetLength(0); i++)
        for (var j = 0; j < _weights.GetLength(1); ++j)
            _weights[i, j] = 2 * _max - _weights[i, j];
    }
   public static List<Edge> GetPairs(EGraph gr)
    {
        var n = gr.N;
        var match = new int[n];
        var parent = new int[n];
        var baseV = new int[n];
        var q = new int[n];
        var inQueue = new bool[n];
        var inBlossom = new bool[n];
        var y = new double[n];
        var z = new double[2*n];

        for (var i = 0; i < n; ++i)
        {
            match[i] = -1;
            var mx = double.MinValue;
            for (var j = 0; j < n; ++j)
                mx = Math.Max(mx, gr._weights[i, j]);
            y[i] = mx;
            baseV[i] = i;
        }

        int FindPath(int root)
        {
            Array.Fill(parent, -1);
            Array.Fill(inQueue, false);
            for (var i = 0; i < n; ++i) baseV[i] = i;
            var qh = 0; var qt = 0;
            q[qt++] = root;
            inQueue[root] = true;

            int lca(int a, int b)
            {
                var used = new bool[n];
                while (true)
                {
                    a = baseV[a];
                    used[a] = true;
                    if (match[a] < 0) break;
                    a = parent[match[a]];
                }
                while (true)
                {
                    b = baseV[b];
                    if (used[b]) return b;
                    b = parent[match[b]];
                }
            }

            void MarkPath(int v, int b, int x)
            {
                while (baseV[v] != b)
                {
                    inBlossom[baseV[v]] = inBlossom[baseV[match[v]]] = true;
                    parent[v] = x;
                    x = match[v];
                    v = parent[match[v]];
                }
            }

            void Contract(int v, int u)
            {
                var b = lca(v, u);
                Array.Fill(inBlossom, false);
                MarkPath(v, b, u);
                MarkPath(u, b, v);
                for (var i = 0; i < n; ++i)
                {
                    if (inBlossom[baseV[i]])
                    {
                        baseV[i] = b;
                        if (!inQueue[i])
                        {
                            q[qt++] = i;
                            inQueue[i] = true;
                        }
                    }
                }
            }

            while (qh < qt)
            {
                var v = q[qh++];
                for (var u = 0; u < n; ++u)
                {
                    if (gr._weights[v, u] <= 0) continue;
                    if (baseV[v] == baseV[u] || match[v] == u) continue;
                    var slack = y[v] + y[u] - 2*gr._weights[v,u] - z[baseV[v]] - z[baseV[u]];
                    if (slack > 1e-9) continue;

                    if (u == root || (match[u] >= 0 && parent[match[u]] >= 0))
                        Contract(v, u);
                    else if (parent[u] < 0)
                    {
                        parent[u] = v;
                        if (match[u] < 0) return u;
                        if (!inQueue[match[u]])
                        {
                            q[qt++] = match[u];
                            inQueue[match[u]] = true;
                        }
                    }
                }
            }
            return -1;
        }

        void Augment(int v)
        {
            while (v >= 0)
            {
                var pv = parent[v];
                var nv = match[pv];
                match[v] = pv;
                match[pv] = v;
                v = nv;
            }
        }

        for (var v = 0; v < n; ++v)
        {
            if (match[v] < 0)
            {
                var u = FindPath(v);
                if (u >= 0) Augment(u);
            }
        }

        var res = new List<Edge>();
        for (var i = 0; i < n; ++i)
        {
            if (match[i] >= 0 && i < match[i])
                res.Add(new Edge(gr._vertexes[i], gr._vertexes[match[i]], gr._weights[i, match[i]]));
            if(match[i] == -1) res.Add(new Edge(gr._vertexes[i], gr._vertexes[i], 0));
        }

        return res;
    }
}