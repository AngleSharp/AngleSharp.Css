namespace AngleSharp.Performance.Css
{
    using AngleSharp.Css.Parser;
    using BenchmarkDotNet.Attributes;
    using System;
    using System.Text;

    /// <summary>
    /// Parsing style sheets that nest deeply. The shapes stay within the default nesting limits
    /// (64 nested blocks, 63 parentheses inside calc()) and within what a parser without them
    /// survives (on a 1 MB stack, before the JIT optimizes, about 580 nested @media rules and
    /// 1900 calc() parentheses), so builds with and without the limits do the same work.
    /// </summary>
    [MemoryDiagnoser]
    public class DeepStyleSheetBenchmarks
    {
        private static readonly CssParser Parser = new CssParser();

        private String _source = null!;

        [Params("media-10", "media-60", "style-10", "style-60", "calc-10", "calc-60", "supports-10", "supports-60")]
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
                "calc" => "a{width:calc(" + Repeat("(1px + ", depth) + "1px" + Repeat(")", depth) + ")}",
                "supports" => "@supports " + Repeat("(", depth) + "display:block" + Repeat(")", depth) + "{a{color:red}}",
                _ => throw new ArgumentException(Shape),
            };
        }

        [Benchmark]
        public Int32 Parse() => Parser.ParseStyleSheet(_source).Rules.Length;

        private static String Repeat(String value, Int32 count) =>
            new StringBuilder(value.Length * count).Insert(0, value, count).ToString();
    }
}
