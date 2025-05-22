namespace TspTask;

class Program
{
    static void Main(string[] args)
    {
        var list = new List<Point> {new Point(0,0,0), new Point(2,0,0), new Point(1,5,0)};
        var gr = new EGraph(list);
        var test = Algo.GetMaxTsp(gr);
        Console.WriteLine(-1);
    }
}