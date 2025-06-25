using System.Globalization;
using NUnit.Framework;

namespace TspTask;

[TestFixture]
public class AlgoTests
{
    private const string AbsolutePath = @"D:\tspData\";

    [TestCase(10)]
    [TestCase(50)]
    [TestCase(100)]
    [TestCase(500)]
    [TestCase(1000)]
    public static void MainTest(int count)
    {
        var name = $"Test_For_{count}";
        var graph = new EGraph(Generator.Generate(count));
        Parser.ParseGraph(graph, AbsolutePath + name + ".tsp", name);
        var resultPathOfAlgo = Algo.GetMaxTsp(graph);
        using StreamWriter writer = File.CreateText(AbsolutePath + name + "-resultOfAlgo.txt");
        writer.WriteLine(resultPathOfAlgo.Select(edge => edge.Cost).Sum());
        writer.WriteLine(graph.Max);
        writer.WriteLine(graph.Angle);

        var t = (int)Math.Ceiling(Math.Pow(graph.N, 1.0 / 3));
        var mu = graph.N / 2;
        var n = graph.N;
        var angel = graph.Angle;
        Console.WriteLine($"Теоретическая оценка для n = {count}: " + (2.0 * mu / n - 2.0 * (t - 2) / n - 4.0 * (mu + 2) / n * Math.Sin(angel / 4) * Math.Sin(angel / 4)));
    }
}