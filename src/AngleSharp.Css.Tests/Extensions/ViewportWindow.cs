#nullable disable
namespace AngleSharp.Css.Tests.Extensions
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;

    [TestFixture]
    public class ViewportWindowTests
    {
        [Test]
        public void InnerSizeIsTheViewportOfTheRenderDevice()
        {
            var window = CreateWindow(new DefaultRenderDevice { ViewPortWidth = 1280, ViewPortHeight = 800 });
            Assert.AreEqual(1280, window.InnerWidth());
            Assert.AreEqual(800, window.InnerHeight());
        }

        [Test]
        public void DevicePixelRatioFollowsTheResolutionOfTheRenderDevice()
        {
            Assert.AreEqual(1.0, CreateWindow(new DefaultRenderDevice()).DevicePixelRatio());
            Assert.AreEqual(2.0, CreateWindow(new DefaultRenderDevice { Resolution = 192 }).DevicePixelRatio());
        }

        private static IWindow CreateWindow(IRenderDevice device)
        {
            var config = Configuration.Default.WithCss().WithRenderDevice(device);
            var context = BrowsingContext.New(config);
            var parser = context.GetService<IHtmlParser>();
            var document = parser.ParseDocument("<!doctype html><title>Example</title>");
            return document.DefaultView;
        }
    }
}
