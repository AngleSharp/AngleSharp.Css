namespace AngleSharp.Performance.Css
{
    using AngleSharp.Css.Parser;
    using BenchmarkDotNet.Attributes;
    using System;
    using System.Text;

    /// <summary>
    /// Parsing style sheets that nest deeply. The shapes stay within the default nesting limit
    /// (64) and within what a parser without the limit survives (on a 1 MB stack, before the
    /// JIT optimizes, about 580 nested @media rules), so builds with and without the limit do
    /// the same work.
    /// </summary>
    [MemoryDiagnoser]
    public class DeepStyleSheetBenchmarks
    {
        private static readonly CssParser Parser = new CssParser();

        private String _source = null!;

        [Params("media-10", "media-60", "style-10", "style-60")]
        public String Shape { get; set; } = null!;

        [GlobalSetup]
        public void Setup()
        {
            var dash = Shape.LastIndexOf('-');
            var depth = Int32.Parse(Shape.Substring(dash + 1));

            _source = Shape.Substring(0, dash) switch
            {
                "media" => Repeat("@media screen{", depth) + "a{color:red}" + Repeat("}", depth),
                "style" => "a{" + Repeat("& b{", depth) + "color:red" + Repeat("}", depth + 1),
                _ => throw new ArgumentException(Shape),
            };
        }

        [Benchmark]
        public Int32 Parse() => Parser.ParseStyleSheet(_source).Rules.Length;

        private static String Repeat(String value, Int32 count) =>
            new StringBuilder(value.Length * count).Insert(0, value, count).ToString();
    }
}
