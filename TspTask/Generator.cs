namespace TspTask;

public static class Generator
{
    private const int min = -10;//int.MinValue / 2;
    private const int max = 10;//int.MaxValue / 2;
    
    public static List<Point> Generate(int count)
    {
        var res = new List<Point>();
        var rnd = new Random();
        for (var k = 0; k < count; ++k)
            res.Add(new Point(rnd.Next(min,max), rnd.Next(min,max)));
        return res;
    }
}