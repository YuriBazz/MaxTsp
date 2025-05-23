using System.Globalization;
using NUnit.Framework;

namespace TspTask;

[TestFixture]
public class AlgoTests
{
    [TestCase(@"100.txt", 7910)]
    [TestCase(@"400.txt", 15281)]
    public static void MainTest(string path, double optimal)
    {
        var list = new List<Point>();
        using (StreamReader reader = File.OpenText(path))
        {
            while (reader.ReadLine() is { } str)
            {
                var p = str.Split(" ").Select(x => double.Parse(x, CultureInfo.InvariantCulture)).ToArray();
                list.Add(new Point(p[0],p[1],p[2]));
            }
        }

        var gr = new EGraph(list);
        var result = Algo.GetMaxTsp(gr);
        var cost = result.Select(x => x.Cost).Sum();
        Console.WriteLine($"Кол-во точек: {list.Count} \n Оптималь: {optimal} \n Результат: {cost} \n Отношение: {optimal} / {cost} = {optimal / cost}");
    }
}