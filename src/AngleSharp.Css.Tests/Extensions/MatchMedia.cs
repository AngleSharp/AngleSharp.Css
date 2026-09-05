#nullable disable
namespace AngleSharp.Css.Tests.Extensions
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;

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
