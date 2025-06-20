using System.Globalization;
using NUnit.Framework;

namespace TspTask;

[TestFixture]
public class AlgoTests
{
    private const string AbsolutePath = @"E:\tspData\";

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
    }
}