namespace AngleSharp.Performance.Css
{
    using AngleSharp;
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using BenchmarkDotNet.Attributes;
    using System;
    using System.Text;

    /// <summary>
    /// Computing a style with a rule whose selector nests :is(): the cascade indexes the rule by
    /// looking into the arguments. The depths stay within the 75 levels AngleSharp allows from
    /// 1.9.1 on (Firefox's limit).
    /// </summary>
    [MemoryDiagnoser]
    public class DeepSelectorBenchmarks
    {
        private IHtmlParser _parser = null!;
        private String _source = null!;

        [Params(10, 50, 75)]
        public Int32 Depth { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            var context = BrowsingContext.New(Configuration.Default.WithCss());
            _parser = context.GetService<IHtmlParser>()!;
            _source = "<style>" + Repeat(":is(", Depth) + ".x" + Repeat(")", Depth) + "{color:red}</style><div class=x>x</div>";
        }

        [Benchmark]
        public String ComputedStyle()
        {
            // a fresh document, so the rule is indexed again (the index is kept with the rule)
            var document = _parser.ParseDocument(_source);
            return document.DefaultView!.GetComputedStyle(document.QuerySelector("div")!).GetColor();
        }

        private static String Repeat(String value, Int32 count) =>
            new StringBuilder(value.Length * count).Insert(0, value, count).ToString();
    }
}
