namespace TspTask;

public static class Parser
{
    private static long[,] ChangeMatrix(double[,] matrix, double max, int n)
    {
        var res = new long[n, n];
        for(var i = 0; i < n; ++i)
        for (var j = 0; j < n; ++j)
            res[i, j] = (long)((max + 1 - matrix[i, j]) * 100);
        return res;
    }

    public static void ParseGraph(EGraph gr, string path, string name)
    {
        var matrix = ChangeMatrix(gr.Weights, gr.Max, gr.N);

        using StreamWriter writer = File.CreateText(path);
        writer.Write($"NAME: {name}\nTYPE: TSP\nDIMENSION: {gr.N}\nEDGE_WEIGHT_TYPE: EXPLICIT\nEDGE_WEIGHT_FORMAT: FULL_MATRIX\n");
        writer.Write("EDGE_WEIGHT_SECTION\n");
        for (var i = 0; i < gr.N; ++i)
        {
            writer.Write(" ");
            for (var j = 0; j < gr.N; ++j)
                writer.Write(matrix[i,j] + (j == gr.N - 1 ? "" : " "));
            writer.Write("\n");
        }
        writer.Write("EOF");
    }
}