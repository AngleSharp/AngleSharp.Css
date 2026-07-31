namespace AngleSharp.Performance.Css
{
    using AngleSharp;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom;
    using BenchmarkDotNet.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Covers the styling side of the library: cascade resolution via
    /// getComputedStyle, full render tree construction and the parsing of
    /// inline style attributes.
    /// </summary>
    [MemoryDiagnoser]
    public class CssCascadeBenchmarks
    {
        private static readonly CssParser DeclarationParser = new CssParser();

        private IDocument _document = null!;
        private IWindow _window = null!;
        private IElement[] _elements = null!;
        private String[] _inlineStyles = null!;

        [GlobalSetup]
        public void Setup()
        {
            var config = Configuration.Default.WithCss();
            var context = BrowsingContext.New(config);
            _document = context.OpenAsync(req => req.Content(BuildDocument())).GetAwaiter().GetResult();
            _window = _document.DefaultView!;

            // A representative sample instead of every element - keeps a single
            // benchmark iteration in a sane range while still walking the cascade
            // for elements at different depths.
            _elements = _document.QuerySelectorAll("#root *").Where((_, i) => i % 12 == 0).ToArray();
            _inlineStyles = BuildInlineStyles();
        }

        [Benchmark]
        public Int32 ComputedStyle()
        {
            var total = 0;

            foreach (var element in _elements)
            {
                total += _window.GetComputedStyle(element).Length;
            }

            return total;
        }

        [Benchmark]
        public Object RenderTree()
        {
            return _window.Render();
        }

        [Benchmark]
        public Int32 ParseInlineDeclarations()
        {
            var total = 0;

            foreach (var style in _inlineStyles)
            {
                total += DeclarationParser.ParseDeclaration(style)?.Length ?? 0;
            }

            return total;
        }

        private static String[] BuildInlineStyles()
        {
            var templates = new[]
            {
                "color:#333;background:#fff",
                "display:none",
                "margin:0;padding:0",
                "width:100%;height:auto",
                "font-family:Arial,Helvetica,sans-serif;font-size:12px",
                "border:1px solid #ccc;border-radius:4px",
                "position:absolute;top:0;left:0;z-index:10",
                "background-image:url(https://example.com/i.png);background-repeat:no-repeat",
                "text-align:center;line-height:1.5",
                "float:left;clear:both;overflow:hidden",
                "background:linear-gradient(to right,#fff 0%,#000 100%)",
                "transform:translate(10px,20px) rotate(45deg)",
                "box-shadow:0 1px 2px rgba(0,0,0,.2)",
                "flex:1 1 auto;align-items:center;justify-content:space-between",
                "padding:10px 15px 10px 15px;margin:0 auto",
                "visibility:hidden;opacity:0.5",
                "color:rgb(51,51,51);background-color:rgba(255,255,255,0.9)",
                "font:bold 14px/1.2 'Segoe UI',sans-serif",
                "grid-template-columns:repeat(3,1fr);gap:10px",
                "transition:all .3s ease-in-out",
            };

            var list = new List<String>();

            for (var i = 0; i < 10; i++)
            {
                list.AddRange(templates);
            }

            return list.ToArray();
        }

        private static String BuildDocument()
        {
            var sb = new StringBuilder();
            sb.Append("<!doctype html><html><head><style>");

            // Roughly the shape and size of a component library sheet: plain
            // class selectors, descendant combinators and a selector list.
            for (var i = 0; i < 400; i++)
            {
                sb.Append(".c-").Append(i % 40).Append(" .child-").Append(i % 20)
                  .Append("{color:#").Append((i % 9) + 1).Append("33;margin:").Append(i % 12)
                  .Append("px;padding:2px;display:block;font-size:1").Append(i % 6).Append("px;}");
            }

            sb.Append("h1,h2,h3,p.big,a:hover,.c-1 span{font-weight:bold;letter-spacing:0.02em;}");
            sb.Append(".hidden{display:none}.big{font-size:24px}a{color:blue}");
            sb.Append("</style></head><body><main id='root'>");

            for (var s = 0; s < 25; s++)
            {
                sb.Append("<section class='c-").Append(s % 40).Append("'>");

                for (var c = 0; c < 12; c++)
                {
                    sb.Append("<div class='child-").Append(c % 20).Append("' style='color:#").Append(c).Append("11'>");
                    sb.Append("<p class='big'>Text ").Append(s).Append('-').Append(c).Append("</p>");
                    sb.Append("<a href='#'>link</a>");
                    sb.Append("</div>");
                }

                sb.Append("</section>");
            }

            sb.Append("</main></body></html>");
            return sb.ToString();
        }
    }
}
