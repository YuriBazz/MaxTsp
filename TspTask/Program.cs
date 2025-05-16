namespace TspTask;

class Program
{
    static void Main(string[] args)
    {
        var list = new List<Point> {new Point(0,0,0), new Point(4,0,0), new Point(2,3,0)};
        var gr = new EGraph(list);
        var pairs = EGraph.GetPairs(gr);
        Console.WriteLine(-1);
    }
}