namespace AngleSharp.Performance.Css
{
    using BenchmarkDotNet.Configs;
    using BenchmarkDotNet.Running;

    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkSwitcher
                .FromAssembly(typeof(Program).Assembly)
                .Run(args, DefaultConfig.Instance);
        }
    }
}
