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
