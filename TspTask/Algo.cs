namespace TspTask;

public class Algo
{
    private static double GetAngel(Edge e1, Edge e2)
    {
        var first = (e1.Second.X - e1.First.X, e1.Second.Y - e1.First.Y, e1.Second.Z - e1.First.Z);
        var second = (e2.Second.X - e2.First.X, e2.Second.Y - e2.First.Y, e2.Second.Z - e2.First.Z);
        var scal = first.Item1 * second.Item1 + first.Item2 * second.Item2 + first.Item3 * second.Item3;
        var l1 = Math.Sqrt(first.Item1 * first.Item1 + first.Item2 * first.Item2 + first.Item3 * first.Item3);
        var l2 = Math.Sqrt(second.Item1 * second.Item1 + second.Item2 * second.Item2 + second.Item3 * second.Item3);
        return scal / (l1 * l2);
    }
    private static List<Edge> Create(List<Edge> pairs, int n, int t)
    {
        var gamma = 16.0; // Посчитано
        pairs.Sort((x,y) => x.Cost.CompareTo(y.Cost));
        var light = new List<Edge>();
        var temp = pairs[0].Cost == 0 ? pairs[0] : null;
        if(temp is not null) pairs.RemoveAt(0); 
        var i =  0;
        var border =  t - 2;
        var hitches = new List<List<Edge>>();
        foreach (var edge in pairs)
        {
            if(i < border) light.Add(edge);
            else hitches.Add(new List<Edge>{edge});
            ++i;
        }

        
        var j = n / 2 - t + 2;
        while (j > 0)
        {
            for (var k = 0; k < hitches.Count - 1; ++k)
            {
                if (GetAngel(hitches[k][^1], hitches[k + 1][0]) < (t - 32.0) / t)
                {
                    hitches[k].AddRange(hitches[k+1]);
                    hitches.RemoveAt(k+1);
                    break;
                }
            }

            j--;
        }

        var E = new List<Edge>();
        var all = new List<Edge>();
        var lInd = 0;
        foreach (var t1 in hitches)
        {
            all.AddRange(t1);
            if (lInd < light.Count)
            {
                all.Add(light[lInd]);
                lInd++;
            }
        }
        // TODO : ДВАЖДЫ УЧИТЫВАЕТ РЕБРО ДЛЯ КВАДРАТА
        foreach (var edge in all)
        {
            if(E.Count == 0) {E.Add(edge); continue;}
            var str = Point.GetDistance(E[^1].First, edge.First) + Point.GetDistance(E[^1].Second, edge.Second);
            var cross = Point.GetDistance(E[^1].First, edge.Second) + Point.GetDistance(E[^1].Second, edge.First);
            if (str > cross)
            {
                E.Add(new Edge(E[^1].First, edge.First, Point.GetDistance(E[^1].First, edge.First)));
                E.Add(new Edge(E[^1].Second, edge.Second,Point.GetDistance(E[^1].Second, edge.Second) ));
            }
            else
            {
                E.Add(new Edge(E[^1].First, edge.Second, Point.GetDistance(E[^1].First, edge.Second)));
                E.Add(new Edge(E[^1].Second, edge.First,Point.GetDistance(E[^1].Second, edge.First) ));
            }
        }
        E.Add(all[^1]);
        if (temp is not null)
        {
            E.RemoveAt(0);
            E.Add(new Edge(temp.First, all[0].First, Point.GetDistance(temp.First, all[0].First)));
            E.Add(new Edge(temp.First, all[0].Second, Point.GetDistance(temp.First, all[0].Second)));
        }

        return E;
    }

    public static List<Edge> GetMaxTsp(EGraph gr)
    {
        var pairs = EGraph.GetPairs(gr);
        var t = (int)Math.Ceiling(Math.Sqrt(gr.N));
        return Create(pairs, gr.N, t);
    }
}