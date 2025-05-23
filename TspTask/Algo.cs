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
    
    public static List<Edge> GetMaxTsp(EGraph gr)
    {
        var pairs = EGraph.GetPairs(gr);
        var t = (int)Math.Ceiling(Math.Sqrt(gr.N));
      
        pairs.Sort((x,y) => x.Cost.CompareTo(y.Cost));
        Edge? temp = null;
        if (pairs[0].Cost == 0)
        {
            temp = pairs[0];
            pairs.RemoveAt(0);
        }
        var all = CreateOrder(pairs, gr.N, t);
        return CreateResult(all, temp);
    }

    private static List<Edge> CreateOrder(List<Edge> pairs, int n, int t)
    {
        var light = pairs.Take(t - 2).ToList();
        var heavy = pairs.Skip(t - 2).ToList();

        var hitches = heavy.Select(x => new Hitch(x)).ToList();
        var j = n / 2 - 2 + t;
        while (j > 1)
        {
            for (var i = 0; i < hitches.Count; ++i)
            {
                var done = false;
                for (var k = i + 1; k < hitches.Count; ++k)
                {
                    if (GetAngel(hitches[i].Last, hitches[k].First) > (t - 16) / t)
                    {
                        done = true;
                        hitches[i].Merge(hitches[k]);
                        hitches.RemoveAt(k);
                    }
                    if(done) break;
                }
                if (done) break;
            }
            j--;
        }

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

    private static List<Edge> CreateResult(List<Edge> all, Edge? temp)
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
            // жесткий ребилд задачи в поиск минимума
            // бтв, так-то вроде вообще плевать, какие в результе будут веса, сборка энивей идет по упорядоченным ранее весам
            var straight = Point.GetDistance(curr.First, next.First) + Point.GetDistance(curr.Second, next.Second);
            var cross = Point.GetDistance(curr.First, next.Second) + Point.GetDistance(curr.Second, next.First);
            if (straight > cross)
            {
                result.Add(new Edge(curr.First,next.First));
                result.Add(new Edge(curr.Second, next.Second));
            }
            else
            {
                result.Add(new Edge(curr.First, next.Second));
                result.Add(new Edge(curr.Second,next.First));
            }
        }
        result.Add(new Edge(all[^1].First, all[^1].Second));
        return result;
    }
}