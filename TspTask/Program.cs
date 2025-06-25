namespace TspTask;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Abandon hope, all ye who enter here");
        Console.WriteLine(TheoreticalBound(10));
        Console.WriteLine(TheoreticalBound(50));
        Console.WriteLine(TheoreticalBound(100));
        Console.WriteLine(TheoreticalBound(500));
        Console.WriteLine(TheoreticalBound(1000));
        
    }

    private static double TheoreticalBound(int n)
    {
        var t = (int)Math.Ceiling(Math.Pow(n, 1.0 / 3));
        var cosLowerBound = 1 - 2 * Math.PI * Math.PI / (t * t);
        var angel =   Math.Acos(cosLowerBound);
        var mu = n / 2;
        return 2.0 * mu / n - 2.0 * (t - 2) / n - 4.0 * (mu + 2) / n * Math.Sin(angel / 4) * Math.Sin(angel / 4);

    }
}