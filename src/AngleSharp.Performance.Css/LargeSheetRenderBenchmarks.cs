namespace AngleSharp.Performance.Css
{
    using AngleSharp;
    using AngleSharp.Css;
    using AngleSharp.Dom;
    using BenchmarkDotNet.Attributes;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Builds the render tree of a document styled by a large real-world sheet (one of the
    /// samples). The markup is generated from the class and id names the sheet uses, so that
    /// a good share of its rules actually match and the cascade has real work to do.
    /// </summary>
    [MemoryDiagnoser]
    public class LargeSheetRenderBenchmarks
    {
        private static readonly String[] Tags = { "div", "section", "article", "ul", "li", "p", "span", "a", "button", "nav" };

        private String _html = null!;
        private IWindow _window = null!;

        [Params("maxcdn.bootstrapcdn", "florian-rappl", "s.yimg")]
        public String CssFileName { get; set; } = null!;

        [GlobalSetup]
        public void Setup()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Samples", CssFileName + ".css");
            var source = File.ReadAllText(path);
            _html = BuildDocument(source);
            _window = Load(_html).DefaultView!;
        }

        /// <summary>
        /// Rendering a document that is already loaded (sheet parsed, rule index warm).
        /// </summary>
        [Benchmark]
        public Object RenderTree() => _window.Render();

        /// <summary>
        /// Loading the document (including the sheet) and rendering it once.
        /// </summary>
        [Benchmark]
        public Object LoadAndRenderTree() => Load(_html).DefaultView!.Render();

        private static IDocument Load(String html)
        {
            var config = Configuration.Default
                .WithRenderDevice(new DefaultRenderDevice
                {
                    ViewPortWidth = 1280,
                    ViewPortHeight = 800,
                })
                .WithCss();
            var context = BrowsingContext.New(config);
            return context.OpenAsync(req => req.Content(html)).GetAwaiter().GetResult();
        }

        private static String BuildDocument(String css)
        {
            var classes = Regex.Matches(css, @"\.(-?[_a-zA-Z][_a-zA-Z0-9-]*)")
                .Cast<Match>()
                .Select(m => m.Groups[1].Value)
                .Distinct()
                .ToArray();
            var ids = Regex.Matches(css, @"#(-?[_a-zA-Z][_a-zA-Z0-9-]*)\s*[{,.:\[ >+~]")
                .Cast<Match>()
                .Select(m => m.Groups[1].Value)
                .Distinct()
                .ToArray();
            var random = new Random(42);
            var sb = new StringBuilder();
            sb.Append("<!doctype html><html><head><style>").Append(css).Append("</style></head><body>");
            var count = 0;
            AppendChildren(sb, random, classes, ids, 0, ref count);
            sb.Append("</body></html>");
            return sb.ToString();
        }

        private static void AppendChildren(StringBuilder sb, Random random, String[] classes, String[] ids, Int32 depth, ref Int32 count)
        {
            var children = depth == 0 ? 30 : random.Next(1, 5);

            for (var i = 0; i < children && count < 3000; i++)
            {
                var tag = Tags[random.Next(Tags.Length)];
                count++;
                sb.Append('<').Append(tag);

                if (classes.Length > 0)
                {
                    sb.Append(" class='");
                    var n = random.Next(1, 4);

                    for (var k = 0; k < n; k++)
                    {
                        sb.Append(classes[random.Next(classes.Length)]).Append(' ');
                    }

                    sb.Append('\'');
                }

                if (ids.Length > 0 && random.Next(20) == 0)
                {
                    sb.Append(" id='").Append(ids[random.Next(ids.Length)]).Append('\'');
                }

                sb.Append('>');

                if (depth < 8)
                {
                    AppendChildren(sb, random, classes, ids, depth + 1, ref count);
                }

                sb.Append("Text ").Append(count);
                sb.Append("</").Append(tag).Append('>');
            }
        }
    }
}
