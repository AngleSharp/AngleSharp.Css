namespace AngleSharp.Performance.Css
{
    using AngleSharp.Css.Parser;
    using BenchmarkDotNet.Attributes;
    using ExCSS;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    [MemoryDiagnoser]
    public class CssParserBenchmarks
    {
        private static readonly CssParserOptions AngleSharpOptions = new CssParserOptions
        {
            IsIncludingUnknownDeclarations = true,
            IsIncludingUnknownRules = true,
            IsToleratingInvalidSelectors = true,
        };

        private static readonly CssParser AngleSharpParser = new CssParser(AngleSharpOptions);

        private string _source = null!;

        [ParamsSource(nameof(CssFileNames))]
        public string CssFileName { get; set; } = null!;

        public static IEnumerable<string> CssFileNames()
        {
            var dir = Path.Combine(AppContext.BaseDirectory, "Samples");

            return Directory.GetFiles(dir, "*.css")
                .OrderBy(f => f)
                .Select(f => Path.GetFileNameWithoutExtension(f));
        }

        [GlobalSetup]
        public void Setup()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Samples", CssFileName + ".css");
            _source = File.ReadAllText(path);
        }

        [Benchmark(Baseline = true)]
        public object AngleSharp()
        {
            return AngleSharpParser.ParseStyleSheet(_source);
        }

        [Benchmark]
        public object ExCss()
        {
            var parser = new StylesheetParser();
            return parser.Parse(_source);
        }

#if NET472
        [Benchmark]
        public object CsCss()
        {
            var parser = new Alba.CsCss.Style.CssLoader
            {
                Compatibility = Alba.CsCss.BrowserCompatibility.FullStandards
            };

            try
            {
                return parser.ParseSheet(_source, new Uri("http://localhost/foo.css"), new Uri("http://localhost"));
            }
            catch
            {
                return null!;
            }
        }
#endif
    }
}
