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
    private static void Hitches(List<Edge> pairs, int n, int t)
    {
        var gamma = 16.0; // Посчитано
        pairs.Sort((x,y) => x.Cost.CompareTo(y.Cost));
        var light = new List<Edge>();
        var i = pairs[0].Cost == 0 ? 1 : 0;
        var border = pairs[0].Cost == 0 ? t - 3 : t - 2;
        var hitches = new List<List<Edge>>();
        foreach (var edge in pairs)
        {
            if(i < border) light.Add(edge);
            else hitches.Add(new List<Edge>{edge});
            ++i;
        }

        var alpha = Math.Acos(1 - 2 * gamma / t);
        var j = n / 2 - t + 2;
        while (j > 0)
        {
            for (var k = 0; k < hitches.Count - 1; ++k)
            {
                if (GetAngel(hitches[i][^1], hitches[i + 1][0]) < (t - 32.0) / t)
                {
                    hitches[i].AddRange(hitches[i+1]);
                    hitches.RemoveAt(i+1);
                    break;
                }
            }

            j--;
        }
        
        
    }
}