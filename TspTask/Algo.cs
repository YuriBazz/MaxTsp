namespace TspTask;

public static class Algo
{
    
    
    private static double GetAngel(Edge e1, Edge e2)
    {
        var first = (e1.Second.X - e1.First.X, e1.Second.Y - e1.First.Y);
        var second = (e2.Second.X - e2.First.X, e2.Second.Y - e2.First.Y);
        var scal = first.Item1 * second.Item1 + first.Item2 * second.Item2;
        var l1 = Math.Sqrt(first.Item1 * first.Item1 + first.Item2 * first.Item2);
        var l2 = Math.Sqrt(second.Item1 * second.Item1 + second.Item2 * second.Item2);
        return scal / (l1 * l2);
    }
    
    public static List<Edge> GetMaxTsp(EGraph gr)
    {
        var pairs = EGraph.GetPairs(gr);
        var t = (int)Math.Ceiling(Math.Pow(gr.N, 1.0/3));
      
        pairs.Sort((x,y) => x.Cost.CompareTo(y.Cost));
        Edge? temp = null;
        if (Math.Abs(pairs[0].Cost - (-1)) < double.Epsilon)
        {
            temp = pairs[0];
            pairs.RemoveAt(0);
        }
        var all = CreateOrder(pairs, gr.N, t, gr);
        return CreateResult(all, gr, temp);
    }

    private static List<Edge> CreateOrder(List<Edge> pairs, int n, int t, EGraph gr)
    {
        var light = pairs.Take(t - 2).ToList();
        var heavy = pairs.Skip(t - 2).ToList();

        var hitches = heavy.Select(x => new Hitch(x)).ToList();
        var j = n / 2 + 2 - t;
        var currentAngle = 0.0;
        while (j > t-1)
        {
            var temp = new List<(int i , int k , double an)>();
            for (var i = 0; i < hitches.Count; ++i)
                for(var k = i + 1; k < hitches.Count; ++k)
                    temp.Add((i, k, Math.Acos(GetAngel(hitches[i].Last,hitches[k].First))));
            temp.Sort((x,y) => x.Item3.CompareTo(y.Item3));
            var res = temp[0];
            hitches[res.i].Merge(hitches[res.k]);
            hitches.RemoveAt(res.k);
            currentAngle = res.an;
            j--;
        }

        gr.Angle = currentAngle;
        
        var all = new List<Edge>();
        var lInd = 0;
        foreach (var hitch in hitches)
        {
            all.AddRange(hitch.Edges);
            if (light.Count != lInd)
            {
                all.Add(light[lInd]);
                lInd++;
            }
        }

        return all;
    }

    private static List<Edge> CreateResult(List<Edge> all, EGraph gr, Edge? temp)
    {
        var result = new List<Edge>();
        if(temp is null)
            result.Add(new Edge(all[0].First, all[0].Second));
        else
        {
            var p = temp.First;
            result.Add(new Edge(p, all[0].First));
            result.Add(new Edge(p, all[0].Second));
        }
        for (var i = 0; i < all.Count - 1; ++i)
        {
            var curr = all[i];
            var next = all[i + 1];
            var straight = Point.GetDistance(curr.First, next.First) + Point.GetDistance(curr.Second, next.Second);
            var cross = Point.GetDistance(curr.First, next.Second) + Point.GetDistance(curr.Second, next.First);
            if (straight > cross)
            {
                result.Add(new Edge(curr.First, next.First));
                result.Add(new Edge(curr.Second, next.Second));
            }
            else
            {
                result.Add(new Edge(curr.First, next.Second));
                result.Add(new Edge(curr.Second, next.First));
            }
        }
        result.Add(new Edge(all[^1].First, all[^1].Second));
        return result;
    }
}