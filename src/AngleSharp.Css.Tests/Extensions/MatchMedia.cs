#nullable disable
namespace AngleSharp.Css.Tests.Extensions
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;
    using System.Linq;

    [TestFixture]
    public class MatchMediaTests
    {
        [TestCase("selector(div)", true)]
        [TestCase("selector(:hover)", true)]
        [TestCase("selector(:not-a-real-pseudo-class)", false)]
        [TestCase("not selector(:not-a-real-pseudo-class)", true)]
        [TestCase("selector(div) and (color: red)", true)]
        [TestCase("(color: red)", true)]
        [TestCase("color: red", true)]
        [TestCase("(color: notacolor)", false)]
        [TestCase("this is ( not parseable", false)]
        [TestCase("", false)]
        public void SupportsAnswersWithoutThrowing(System.String condition, System.Boolean expected)
        {
            var window = CreateWindow(new DefaultRenderDevice { ViewPortWidth = 1000, ViewPortHeight = 800 });
            Assert.AreEqual(expected, window.Supports(condition));
        }

        private static (IWindow Window, IElement Element) CreateStyled(System.String style)
        {
            var document = new HtmlParser(new HtmlParserOptions(), BrowsingContext.New(Configuration.Default.WithCss())).ParseDocument($"<!doctype html><div id=d style=\"{style}\"></div>");
            return (document.DefaultView, document.GetElementById("d"));
        }

        [TestCase("position", "static")]
        [TestCase("visibility", "visible")]
        [TestCase("overflow-x", "visible")]
        [TestCase("overflow-y", "visible")]
        [TestCase("overflow", "visible")]
        [TestCase("opacity", "1")]
        [TestCase("flex-direction", "row")]
        public void ScriptSeesInitialValuesOfWhatWasNotDeclared(System.String property, System.String expected)
        {
            var (window, element) = CreateStyled("");
            Assert.AreEqual(expected, window.GetComputedStyleOfScript(element).GetPropertyValue(property));
        }

        [Test]
        public void ScriptSeesWhatWasDeclared()
        {
            var (window, element) = CreateStyled("position: absolute; overflow: hidden");
            var style = window.GetComputedStyleOfScript(element);

            Assert.AreEqual("absolute", style.GetPropertyValue("position"));
            Assert.AreEqual("hidden", style.GetPropertyValue("overflow"));
            Assert.AreEqual("visible", style.GetPropertyValue("visibility"));
        }

        [Test]
        public void TheComputedStyleForDotNetStillTellsWhatWasNotDeclared()
        {
            var (window, element) = CreateStyled("");
            window.GetComputedStyleOfScript(element);

            Assert.AreEqual(System.String.Empty, window.GetComputedStyle(element).GetPropertyValue("position"));
        }

        private static readonly System.Collections.Generic.HashSet<System.String> NotForScript = new System.Collections.Generic.HashSet<System.String>
        {
            // pages test for these to rely on them, but nothing applies them
            "anchor-name", "anchor-scope", "position-anchor", "position-area", "position-try-fallbacks", "position-try-order", "position-visibility",
            "animation-timeline", "animation-range", "animation-range-start", "animation-range-end", "view-transition-name", "view-transition-class",
            // these are not properties of a style
            "src", "unicode-range", "font-display", "size-adjust", "ascent-override", "descent-override", "line-gap-override",
            "min-zoom", "max-zoom", "user-zoom", "orientation", "string-set", "running", "footnote-display", "footnote-policy",
            "bookmark-level", "bookmark-label", "bookmark-state", "background-repeat-x", "background-repeat-y", "scrollbar-base-color",
        };

        private static System.Collections.Generic.IEnumerable<System.String> AllProperties() =>
            typeof(ICssStyleDeclaration).Assembly.GetType("AngleSharp.Css.PropertyNames")
                .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                .Select(m => (System.String)m.GetValue(null));

        private static ILookup<System.String, System.Reflection.MethodInfo> ScriptMembers() =>
            typeof(StyleDeclarationExtensions).GetMethods()
                .SelectMany(m => m.GetCustomAttributes(typeof(AngleSharp.Attributes.DomNameAttribute), false)
                    .Cast<AngleSharp.Attributes.DomNameAttribute>()
                    .Select(n => new { n.OfficialName, Method = m }))
                .ToLookup(m => m.OfficialName, m => m.Method);

        private static System.String ToCamelCase(System.String name)
        {
            var parts = name.TrimStart('-').Split('-');
            return parts[0] + System.String.Concat(parts.Skip(1).Select(m => char.ToUpperInvariant(m[0]) + m.Substring(1)));
        }

        [Test]
        public void EveryPropertyThatCanBeAppliedHasAnAccessorAndAHyphenatedName()
        {
            var members = ScriptMembers();
            var missing = AllProperties()
                .Where(m => !NotForScript.Contains(m) && m != "float")
                .Where(m => members[ToCamelCase(m)].Count() != 2 || members[m].Count() != 2)
                .ToArray();

            Assert.IsEmpty(missing);
        }

        [Test]
        public void WhatCanNotBeAppliedHasNoAccessorSoThatPagesDoNotRelyOnIt()
        {
            var members = ScriptMembers();
            var exposed = NotForScript.Where(m => members[ToCamelCase(m)].Any() || members[m].Any()).ToArray();

            Assert.IsEmpty(exposed);
        }

        [Test]
        public void EveryMemberIsAnAccessor()
        {
            var plain = typeof(StyleDeclarationExtensions).GetMethods()
                .Where(m => m.IsDefined(typeof(AngleSharp.Attributes.DomNameAttribute), false) && !m.IsDefined(typeof(AngleSharp.Attributes.DomAccessorAttribute), false))
                .Select(m => m.Name)
                .ToArray();

            Assert.IsEmpty(plain);
        }

        [Test]
        public void FloatHasBothOfItsNames()
        {
            var members = ScriptMembers();

            Assert.AreEqual(2, members["cssFloat"].Count());
            Assert.AreEqual(2, members["float"].Count());
        }

        private sealed class FixedUsedValues : ICssUsedValueProvider
        {
            public System.String GetUsedValue(IElement element, System.String propertyName) => propertyName == "width" ? "123px" : null;
        }

        [Test]
        public void ScriptSeesWhatLayoutFound()
        {
            var config = Configuration.Default.WithCss();
            var context = BrowsingContext.New(config);
            context.SetUsedValueProvider(new FixedUsedValues());
            var document = new HtmlParser(new HtmlParserOptions(), context).ParseDocument("<!doctype html><div id=d style='height: 5px'></div>");
            var element = document.GetElementById("d");
            var style = document.DefaultView.GetComputedStyleOfScript(element);

            Assert.AreEqual("123px", style.GetPropertyValue("width"));
            Assert.AreEqual("5px", style.GetPropertyValue("height"));
            Assert.AreEqual(System.String.Empty, document.DefaultView.GetComputedStyle(element).GetPropertyValue("width"));
        }

        [Test]
        public void AddListenerListensToChangesUntilRemoved()
        {
            var window = CreateWindow(new DefaultRenderDevice { ViewPortWidth = 1000, ViewPortHeight = 800 });
            var list = window.MatchMedia("all");
            var calls = 0;
            DomEventHandler listener = (_, _) => calls++;

            list.AddListener(listener);
            list.Dispatch(new AngleSharp.Dom.Events.Event("change"));
            list.RemoveListener(listener);
            list.Dispatch(new AngleSharp.Dom.Events.Event("change"));

            Assert.AreEqual(1, calls);
        }

        [Test]
        public void MatchMediaWithoutAnyQueryIsMatched()
        {
            var window = CreateWindow(new DefaultRenderDevice { ViewPortWidth = 1000, ViewPortHeight = 800 });
            Assert.IsTrue(window.MatchMedia("").IsMatched);
        }

        [Test]
        public void MatchMediaAllIsMatched()
        {
            var window = CreateWindow(new DefaultRenderDevice { ViewPortWidth = 1000, ViewPortHeight = 800 });
            Assert.IsTrue(window.MatchMedia("all").IsMatched);
        }

        [Test]
        public void MatchMediaScreenIsMatchedOnScreenDevice()
        {
            var window = CreateWindow(new DefaultRenderDevice { Category = DeviceCategory.Screen });
            Assert.IsTrue(window.MatchMedia("screen").IsMatched);
        }

        [Test]
        public void MatchMediaPrintIsNotMatchedOnScreenDevice()
        {
            var window = CreateWindow(new DefaultRenderDevice { Category = DeviceCategory.Screen });
            Assert.IsFalse(window.MatchMedia("print").IsMatched);
        }

        [Test]
        public void MatchMediaPrintIsMatchedOnPrinterDevice()
        {
            var window = CreateWindow(new DefaultRenderDevice { Category = DeviceCategory.Printer });
            Assert.IsTrue(window.MatchMedia("print").IsMatched);
        }

        [Test]
        public void MatchMediaWithCommaSeparatedQueriesIsMatchedWhenOneQueryMatches()
        {
            var window = CreateWindow(new DefaultRenderDevice { Category = DeviceCategory.Screen });
            Assert.IsTrue(window.MatchMedia("screen, print").IsMatched);
        }

        [Test]
        public void MatchMediaScreenIsNotMatchedOnPrinterDevice()
        {
            var window = CreateWindow(new DefaultRenderDevice { Category = DeviceCategory.Printer });
            Assert.IsFalse(window.MatchMedia("screen").IsMatched);
        }

        [Test]
        public void MatchMediaMinWidthIsMatchedForWideViewPort()
        {
            var window = CreateWindow(new DefaultRenderDevice { ViewPortWidth = 1000, ViewPortHeight = 800 });
            Assert.IsTrue(window.MatchMedia("(min-width: 600px)").IsMatched);
        }

        [Test]
        public void MatchMediaMinWidthIsNotMatchedForNarrowViewPort()
        {
            var window = CreateWindow(new DefaultRenderDevice { ViewPortWidth = 320, ViewPortHeight = 480 });
            Assert.IsFalse(window.MatchMedia("(min-width: 600px)").IsMatched);
        }

        [Test]
        public void MatchMediaMaxWidthIsMatchedForNarrowViewPort()
        {
            var window = CreateWindow(new DefaultRenderDevice { ViewPortWidth = 320, ViewPortHeight = 480 });
            Assert.IsTrue(window.MatchMedia("(max-width: 600px)").IsMatched);
        }

        [Test]
        public void MatchMediaMaxWidthIsNotMatchedForWideViewPort()
        {
            var window = CreateWindow(new DefaultRenderDevice { ViewPortWidth = 1000, ViewPortHeight = 800 });
            Assert.IsFalse(window.MatchMedia("(max-width: 600px)").IsMatched);
        }

        [Test]
        public void MatchMediaCombinedWidthRangeIsMatchedInBetween()
        {
            var window = CreateWindow(new DefaultRenderDevice { ViewPortWidth = 1000, ViewPortHeight = 800 });
            Assert.IsTrue(window.MatchMedia("(min-width: 600px) and (max-width: 1200px)").IsMatched);
        }

        [Test]
        public void MatchMediaOnlyScreenWithMinWidthIsMatchedForWideViewPort()
        {
            var window = CreateWindow(new DefaultRenderDevice { ViewPortWidth = 1000, ViewPortHeight = 800 });
            Assert.IsTrue(window.MatchMedia("only screen and (min-width: 600px)").IsMatched);
        }

        [Test]
        public void MatchMediaOnlyScreenWithMinWidthIsNotMatchedForNarrowViewPort()
        {
            var window = CreateWindow(new DefaultRenderDevice { ViewPortWidth = 320, ViewPortHeight = 480 });
            Assert.IsFalse(window.MatchMedia("only screen and (min-width: 600px)").IsMatched);
        }

        [Test]
        public void MatchMediaNotScreenIsNotMatchedOnScreenDevice()
        {
            var window = CreateWindow(new DefaultRenderDevice { Category = DeviceCategory.Screen });
            Assert.IsFalse(window.MatchMedia("not screen").IsMatched);
        }

        [Test]
        public void MatchMediaNotPrintIsMatchedOnScreenDevice()
        {
            var window = CreateWindow(new DefaultRenderDevice { Category = DeviceCategory.Screen });
            Assert.IsTrue(window.MatchMedia("not print").IsMatched);
        }

        [Test]
        public void MatchMediaNotAllIsNotMatched()
        {
            var window = CreateWindow(new DefaultRenderDevice { Category = DeviceCategory.Screen });
            Assert.IsFalse(window.MatchMedia("not all").IsMatched);
        }

        [Test]
        public void MatchMediaNotMinWidthIsMatchedForNarrowViewPort()
        {
            var window = CreateWindow(new DefaultRenderDevice { ViewPortWidth = 320, ViewPortHeight = 480 });
            Assert.IsTrue(window.MatchMedia("not (min-width: 600px)").IsMatched);
        }

        [Test]
        public void MatchMediaNotMinWidthIsNotMatchedForWideViewPort()
        {
            var window = CreateWindow(new DefaultRenderDevice { ViewPortWidth = 1000, ViewPortHeight = 800 });
            Assert.IsFalse(window.MatchMedia("not (min-width: 600px)").IsMatched);
        }

        [Test]
        public void MatchMediaUnknownFeatureIsNotMatched()
        {
            var window = CreateWindow(new DefaultRenderDevice { ViewPortWidth = 1000, ViewPortHeight = 800 });
            Assert.IsFalse(window.MatchMedia("(foo-bar: 3)").IsMatched);
        }

        [Test]
        public void MatchMediaMinHeightIsMatchedForTallViewPort()
        {
            var window = CreateWindow(new DefaultRenderDevice { ViewPortWidth = 1000, ViewPortHeight = 800 });
            Assert.IsTrue(window.MatchMedia("(min-height: 600px)").IsMatched);
        }

        [Test]
        public void MatchMediaWithoutRenderDeviceUsesTheDefaultDevice()
        {
            var context = BrowsingContext.New(Configuration.Default.WithCss());
            var window = CreateWindow(context);
            Assert.IsTrue(window.MatchMedia("screen").IsMatched);
            Assert.IsFalse(window.MatchMedia("print").IsMatched);
        }

        [Test]
        public void MatchMediaKeepsTheProvidedMediaText()
        {
            var window = CreateWindow(new DefaultRenderDevice { ViewPortWidth = 1000, ViewPortHeight = 800 });
            Assert.AreEqual("(min-width: 600px)", window.MatchMedia("(min-width: 600px)").MediaText);
        }

        // Media Queries 4 range syntax, e.g. used by GitHub for all of its breakpoints
        [TestCase("(width >= 1000px)", true)]
        [TestCase("(width >= 1001px)", false)]
        [TestCase("(width <= 1000px)", true)]
        [TestCase("(width <= 999.98px)", false)]
        [TestCase("(width > 1000px)", false)]
        [TestCase("(width > 999px)", true)]
        [TestCase("(width < 1000px)", false)]
        [TestCase("(width < 1001px)", true)]
        [TestCase("(width = 1000px)", true)]
        [TestCase("(width = 999px)", false)]
        [TestCase("(width>=1000px)", true)]
        [TestCase("(1000px <= width)", true)]
        [TestCase("(1001px <= width)", false)]
        [TestCase("(1001px > width)", true)]
        [TestCase("(1000px > width)", false)]
        [TestCase("(400px < width < 1200px)", true)]
        [TestCase("(400px < width <= 1000px)", true)]
        [TestCase("(400px < width < 1000px)", false)]
        [TestCase("(1200px > width > 400px)", true)]
        [TestCase("(1200px > width >= 1000px)", true)]
        [TestCase("(1200px > width > 1000px)", false)]
        [TestCase("(height > 600px)", true)]
        [TestCase("(height >= 801px)", false)]
        [TestCase("screen and (width >= 1000px)", true)]
        [TestCase("(width >= 768px) and (width < 1012px)", true)]
        [TestCase("(width >= 1012px) and (height > 600px)", false)]
        [TestCase("not (width >= 1012px)", true)]
        [TestCase("not (width < 1012px)", false)]
        // calc() in a range, e.g. used by MDN for its breakpoints (1rem = 16px here)
        [TestCase("(width < calc(1rem * 2 + 15rem + 2rem + 31rem))", false)]
        [TestCase("(width > calc(1rem * 2 + 15rem + 2rem + 31rem))", true)]
        [TestCase("(min-width: calc(500px + 1rem))", true)]
        [TestCase("(min-width: calc(1100px + 1rem))", false)]
        [TestCase("(max-width: min(800px, 50rem))", false)]
        [TestCase("(max-width: min(1200px, 100rem))", true)]
        public void MatchMediaEvaluatesRanges(string query, bool expected)
        {
            var window = CreateWindow(new DefaultRenderDevice { ViewPortWidth = 1000, ViewPortHeight = 800, FontSize = 16 });
            Assert.AreEqual(expected, window.MatchMedia(query).IsMatched);
        }

        // the viewport is 1000x800 here
        [TestCase("(min-width: 2000px) or (min-height: 600px)", true)]
        [TestCase("(min-width: 2000px) or (min-height: 900px)", false)]
        [TestCase("(width >= 2000px) or (width < 1012px)", true)]
        [TestCase("(min-width: 500px) and ((min-height: 900px) or (orientation: landscape))", true)]
        [TestCase("(min-width: 500px) and ((min-height: 900px) or (orientation: portrait))", false)]
        [TestCase("((min-width: 500px) and (min-height: 900px)) or (orientation: landscape)", true)]
        [TestCase("(min-width: 500px) and (not (min-height: 900px))", true)]
        [TestCase("not ((min-width: 500px) and (min-height: 900px))", true)]
        [TestCase("not ((min-width: 500px) and (min-height: 600px))", false)]
        [TestCase("not ((min-width: 2000px) or (min-height: 600px))", false)]
        [TestCase("not screen and (min-width: 2000px)", true)]
        [TestCase("not screen and (min-width: 500px)", false)]
        [TestCase("not print and (min-width: 500px)", true)]
        [TestCase("only screen and (min-width: 500px) and (max-width: 1200px)", true)]
        [TestCase("tv", false)]
        [TestCase("not tv", true)]
        [TestCase("not all", false)]
        public void MatchMediaCombinesConditions(string query, bool expected)
        {
            var window = CreateWindow(new DefaultRenderDevice { Category = DeviceCategory.Screen, ViewPortWidth = 1000, ViewPortHeight = 800 });
            Assert.AreEqual(expected, window.MatchMedia(query).IsMatched);
        }

        [TestCase(1000, 800, "landscape")]
        [TestCase(800, 1000, "portrait")]
        [TestCase(800, 800, "portrait")]
        public void MatchMediaOrientationFollowsTheViewport(int width, int height, string orientation)
        {
            // a landscape device may well show a portrait viewport
            var window = CreateWindow(new DefaultRenderDevice { ViewPortWidth = width, ViewPortHeight = height, DeviceWidth = 1920, DeviceHeight = 1080 });
            Assert.IsTrue(window.MatchMedia($"(orientation: {orientation})").IsMatched);
        }

        [TestCase("(min-width: 500px) and ((min-height: 900px) or (orientation: landscape))")]
        [TestCase("(min-width: 500px) and (not (min-height: 900px))")]
        [TestCase("not ((min-width: 500px) and (min-height: 900px))")]
        public void MatchMediaKeepsNestedConditions(string query)
        {
            var window = CreateWindow(new DefaultRenderDevice { ViewPortWidth = 1000, ViewPortHeight = 800 });
            Assert.AreEqual(query, window.MatchMedia(query).Media.MediaText);
        }

        [TestCase("(400px < width > 1200px)")]
        [TestCase("(min-width: )")]
        [TestCase("screen and")]
        public void MatchMediaOfAnInvalidQueryIsNotAll(string query)
        {
            var window = CreateWindow(new DefaultRenderDevice { ViewPortWidth = 1000, ViewPortHeight = 800 });
            var list = window.MatchMedia(query);

            Assert.IsFalse(list.IsMatched);
            Assert.AreEqual("not all", list.Media.MediaText);
        }

        [Test]
        public void RangesPointingInDifferentDirectionsAreInvalid()
        {
            var context = BrowsingContext.New(Configuration.Default.WithCss());
            var sheet = (ICssStyleSheet)context.GetService<IHtmlParser>()
                .ParseDocument("<style>@media (400px < width > 1200px) { p { color: red } }</style>")
                .StyleSheets[0];

            Assert.AreEqual("not all", sheet.Rules.OfType<ICssMediaRule>().Single().ConditionText);
        }

        [TestCase("(width >= 1012px)")]
        [TestCase("(1012px <= width)")]
        [TestCase("(400px < width <= 1200px)")]
        [TestCase("(1200px > width > 400px)")]
        public void MatchMediaKeepsTheRangeText(string query)
        {
            var window = CreateWindow(new DefaultRenderDevice { ViewPortWidth = 1000, ViewPortHeight = 800 });
            Assert.AreEqual(query, window.MatchMedia(query).Media.MediaText);
        }

        private static IWindow CreateWindow(IRenderDevice device)
        {
            var config = Configuration.Default.WithCss().WithRenderDevice(device);
            return CreateWindow(BrowsingContext.New(config));
        }

        private static IWindow CreateWindow(IBrowsingContext context)
        {
            var parser = context.GetService<IHtmlParser>();
            var document = parser.ParseDocument("<!doctype html><title>Example</title>");
            return document.DefaultView;
        }
    }
}
