namespace AngleSharp.Performance.Css
{
    using BenchmarkDotNet.Running;

    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<CssParserBenchmarks>(args: args);
        }
    }
}
